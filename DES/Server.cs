using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace DES
{
    public enum ContinousRVGType
    {
        Exp, Uni
    }
    public enum ServerState
    {
        Free, Busy, Blocked
    }

    /// <summary>
    /// 1. Serve a Client
    /// 2. Complete a service
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]   
    public class Server : IDEScomponent
    {
        #region Datafield

        static protected int count = 1;
        int id; // gantt id 用來區分不同的 server
        double lastTime;
        int clientServed;

        RandomVariateGenerator serviceTimeGtor;
        ServiceNode theNode;
        TimedQueue targetQueue;
        Series serverGanttSeries = new Series();
        Series pieSeries = new Series();
        Client theClientUnderService = null;
        protected static Chart serverChart = null;
        protected static ChartArea serverChartArea = null;
        protected static Chart pieChart = null;
        protected static ChartArea pieChartArea = null;
        public event EventHandler TargetQueueChanged;

        #endregion

        #region Property

        [Category("Model"), Description("Server 對應的 Queue")]
        public TimedQueue TargetQueue
        {
            get => targetQueue;
            set
            {
                if (value == targetQueue) return;
                else
                {
                    targetQueue = value;
                    if (TargetQueueChanged != null)
                    {
                        TargetQueueChanged(this, null);
                    }
                }
            }
        }

        [Category("Simulation"), Description("Server 閒置時間"), Browsable(false)]
        public double FreeTime { get; set; } = 0.0;

        [Category("Simulation"), Description("Server 忙碌時間"), Browsable(false)]
        public double BusyTime { get; set; } = 0.0;

        [Category("Simulation"), Description("Server Blocked 時間"), Browsable(false)]
        public double BlockedTime { get; set; } = 0.0;

        [Category("Chart"), Description("Server 甘特圖"), Browsable(false)]
        public Series ServerGanttSeries { get => serverGanttSeries; } // read only

        [Category("Model"), Description("Server 顯示名稱")]
        public string ServerName
        {
            get => serverGanttSeries.Name;
            set => serverGanttSeries.Name = value;
        }

        [Category("Generator"), Description("Service Time 產生器")]
        public RandomVariateGenerator ServiceTimeGenerator { get => serviceTimeGtor; set => serviceTimeGtor = value; }

        [Browsable(false)]
        public ServerState CurrentState { set; get; } = ServerState.Free;       

        #endregion


        /// <summary>
        /// Constructor of Server: Server 都要認得所屬的 Service Node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="ganttID"></param>
        public Server(ServiceNode node)
        {
            theNode = node; // 讓 server 指向 service node
            id = count;

            serverGanttSeries.Name = $"Server {id}";            
            serverGanttSeries.ChartType = SeriesChartType.RangeBar;
            serverGanttSeries["DrawSideBySide"] = "False";
            serverGanttSeries["PointWidth"] = "0.5";
            serverGanttSeries.IsVisibleInLegend = false; // server Series 不要顯示在 legends 裡

            pieSeries.Name = serverGanttSeries.Name;
            pieSeries.ChartType = SeriesChartType.Doughnut;
            
            // 只有三個點
            pieSeries.Points.AddXY("Free", 0.0);
            pieSeries.Points.AddXY("Busy", 0.0);
            pieSeries.Points.AddXY("Blocked", 0.0);

            pieSeries.Points[0].Color = Color.FromArgb(200, Color.Lime); // 不透明度 200
            pieSeries.Points[1].Color = Color.FromArgb(200, Color.Red);
            pieSeries.Points[2].Color = Color.FromArgb(200, Color.Gold);

            //timeGeneratorType = ContinousRVGType.Exp;
            serviceTimeGtor = new ExponentialRVG(0.8);

            count++;
        }

        public Server() : this(null)
        {
            TimedQueue queue = new TimedQueue();
            targetQueue = queue;
        }

        public void ResetID(int serverSerialID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reset Server Simulation
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name=""></param>
        public void ResetSimulation(ServiceNode parentNode, ref int serialID)
        {
            id = serialID++;
            CurrentState = ServerState.Free;
            clientServed = 0;
            FreeTime = 0.0;
            BusyTime = 0.0;
            BlockedTime = 0.0;
            lastTime = 0.0;
            serverGanttSeries.Points.Clear();

            theNode = parentNode;

            if (serverChart != null)
            {
                if (!serverChart.Series.Contains(serverGanttSeries))
                    serverChart.Series.Add(serverGanttSeries);
                if (serverChartArea != null)
                    serverGanttSeries.ChartArea = serverChartArea.Name;
            }

            if (pieChart != null)
            {
                if (!pieChart.Series.Contains(pieSeries))
                {
                    // ChartArea Title 要分開處理
                    pieChart.Series.Add(pieSeries);
                    ChartArea area = new ChartArea();
                    pieChart.ChartAreas.Add(area);
                    area.Area3DStyle.Enable3D = true;
                    area.Area3DStyle.Inclination = 65;
                    area.Area3DStyle.PointDepth = 40;
                    pieSeries.ChartArea = area.Name;
                    Title title = new Title(pieSeries.Name);
                    pieChart.Titles.Add(title);
                    title.DockedToChartArea = area.Name;
                    title.IsDockedInsideChartArea = false;
                    title.Docking = Docking.Bottom;
                    title.Alignment = ContentAlignment.MiddleCenter;
                }
            }
        }

        /// <summary>
        /// Server: 接收到 Client，服務 Client，並回傳一個 Discrete Event
        /// </summary>
        /// <param name="time"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public DiscreteEvent ServeAClient(double time, Client client)
        {
            theClientUnderService = client;
            client.CurrentServer = this; // client 會自帶一個當下正在服務他的 server
            FreeTime += time - lastTime; // 累加 free time

            // 更新 pie
            pieSeries.Points[0].YValues[0] = FreeTime;

            // 更新 server 甘特圖(x: server id編號，y: 上次時間 & 當前時間)
            DataPoint dp = new DataPoint(id, new double[] { lastTime, time });
            dp.Color = Color.Lime;
            dp.BorderColor = Color.Black;
            dp.AxisLabel = ServerName;
            serverGanttSeries.Points.Add(dp);

            CurrentState = ServerState.Busy; // 服務顧客，Server 狀態變成忙碌中
            lastTime = time; // 更新 last time

            // ask Client update its gantt
            client.ReceiveService(time, this);

            // get service time generator from client
            RandomVariateGenerator generator = client.CurrentServiceTimeGenerator;
            // 產生下個 Discrete Event(Service Complete Event)
            double serviceTime = generator == null ? serviceTimeGtor.NextDouble() : generator.NextDouble();
            DiscreteEvent ev = new ServiceCompleteEvent(this, time + serviceTime); // event time = time + service time
            return ev;
        }

        /// <summary>
        /// Server: 服務完 Client，Service Completion
        /// </summary>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public List<DiscreteEvent> CompleteCurrentService(double currentTime)
        {
            bool isOK = theNode.ExitAClient(theClientUnderService, this, currentTime);

            // 更新 server 甘特圖(x: server id編號，y: 上次時間 & 當前時間)
            DataPoint dp = new DataPoint(id, new double[] { lastTime, currentTime });
            dp.AxisLabel = ServerName;
            serverGanttSeries.Points.Add(dp);

            if (CurrentState == ServerState.Busy)
            {
                BusyTime += currentTime - lastTime; // 累加 busy time           
                pieSeries.Points[1].YValues[0] = BusyTime; // 更新 pie           
                dp.Color = Color.Red;
                dp.BorderColor = Color.Black;
            }
            else if (CurrentState == ServerState.Blocked)
            {
                BlockedTime += currentTime - lastTime; // 累加 blocked time           
                pieSeries.Points[2].YValues[0] = BlockedTime; // 更新 pie           
                dp.Color = Color.Gold;
                dp.BorderColor = Color.Black;
                // Add a gantt block to the client
                //theClientUnderService.EscapeFromDwellState(currentTime);
            }

            lastTime = currentTime;

            List<DiscreteEvent> newEvents = null;
            if (isOK)
            {
                clientServed++;
                CurrentState = ServerState.Free; // 服務完顧客，Server 狀態變成閒置

                // 檢查 queue 裡面有沒有 client
                if (targetQueue.CurrentClientCount > 0)
                {
                    Client head = targetQueue.DeQueue(currentTime); // 在 currentTime 時間點，DeQueue 並送出那個 Client
                    DiscreteEvent de = ServeAClient(currentTime, head); // 請 Server 處理 head 這個 Client，並接收 head 的 Service Completion Event
                    newEvents = new List<DiscreteEvent>();
                    newEvents.Add(de);
                }
                theNode.ReceiveBlockedClient(currentTime);
            }
            else // blocked
            {
                CurrentState = ServerState.Blocked; // 服務完顧客，Server 送不出顧客，狀態變成 Blocked
            }
            return newEvents;
        }

        public string DisplaySimulationResults()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"--- Server: {ServerName} ---");
            sb.AppendLine($"Clients Served: {clientServed}");
            sb.AppendLine($"Busy Ratio: {BusyTime / (FreeTime + BusyTime + BlockedTime):0.00}");
            sb.AppendLine($"Free Ratio: {FreeTime / (FreeTime + BusyTime + BlockedTime):0.00}");
            sb.AppendLine($"Blocked Ratio: {BlockedTime / (FreeTime + BusyTime + BlockedTime):0.00}");
            return sb.ToString();
        }        

        public void UpdateTargetQueue(TimedQueue queue)
        {
            targetQueue = queue;
        }
        /// <summary>
        /// 設定 Server 的圖形位置
        /// </summary>
        /// <param name="chtServer"></param>
        /// <param name="area"></param>
        public static void SetServerChartAndPieChart(Chart chtServer, ChartArea area, Chart chtPie)
        {
            serverChart = chtServer;
            serverChartArea = area;
            pieChart = chtPie;
        }

        public override string ToString()
        {
            return ServerName;
        }

        public void SaveToFileStream(StreamWriter sw)
        {
            sw.WriteLine($"Name: {ServerName}");
            sw.WriteLine($"GanttID: {id}");
            sw.WriteLine($"TimeGenerator: {serviceTimeGtor.GetType().Name}");
            serviceTimeGtor.SaveToFileStream(sw);
        }

        public void ReadFromFileStream(StreamReader sr)
        {
            string str = sr.ReadLine();
            ServerName = str.Substring(str.IndexOf(":") + 1).Trim();
            str = sr.ReadLine();
            id = Convert.ToInt32(str.Substring(str.IndexOf(":") + 1).Trim());
            str = sr.ReadLine();
            str = str.Substring(str.IndexOf(":") + 1).Trim();
            serviceTimeGtor = RandomVariateGenerator.CreateRVGbyType(str);
            if (serviceTimeGtor != null)
                serviceTimeGtor.ReadFromFileStream(sr);
        }
    }
}
