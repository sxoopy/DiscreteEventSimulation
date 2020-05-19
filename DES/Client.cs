using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace DES
{
    public enum ClientState
    {
        None, WaitInQueue, BeingServed, Dwell
    }
    public class Client
    {
        #region Datafield
        protected static Random rnd = new Random();
        protected static int count = 1;
        //protected static List<ServiceNode> itinerary; // 此模型設定一串 service node，讓 client 知道走的順序(ex. node 1 -> node 2)，也就是行程表
        protected static Chart clientChart = null;
        protected static ChartArea clientChartArea = null;        
        double birthTime; // client 產生的時間
        double lastTime;
        Itinerary theItinerary;
        int currentItineraryItemID = -1;
        Series clientGanttSeries = new Series();
        int ganttID;
        Color clr;
        #endregion

        #region Datafield

        public Server CurrentServer { set; get; } = null;
        public double BirthTime { get => birthTime; }
        public string ClientName
        {
            get => clientGanttSeries.Name;
            set => clientGanttSeries.Name = value;            
        }
        [Browsable(false)]
        public ClientState CurrentState { set; get; } = ClientState.None;
        [Browsable(false)]
        public RandomVariateGenerator CurrentServiceTimeGenerator
        {
            get
            {
                if (currentItineraryItemID < 0 || currentItineraryItemID >= theItinerary.ItineraryItems.Count)
                    return null;
                else
                    return theItinerary.ItineraryItems[currentItineraryItemID].ServiceTimeGenerator;
            }
        }

        public Itinerary TheItinerary { get => theItinerary; }
        #endregion

        // constructor 1
        public Client(double time, Itinerary it)
        {
            theItinerary = it;
            clr = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            ClientName = $"Client{count}";
            birthTime = time;
            ganttID = count++; // count 會先 assign 給 ganttID，之後才 +1

            clientGanttSeries.ChartType = SeriesChartType.RangeBar;
            clientGanttSeries["DrawSideBySide"] = "False";
            clientGanttSeries["PointWidth"] = "0.5"; // 厚度
            //clientGanttSeries.Name = $"{theItinerary.ItineraryItems[currentItineraryItemID].TheNode}-Client {ganttID}";
            if (clientChart != null) clientChart.Series.Add(clientGanttSeries); // 加入 series 到 chart 裡
            if (clientChartArea != null) clientGanttSeries.ChartArea = clientChartArea.Name;

            lastTime = birthTime;
            //DataPoint dp = new DataPoint(ganttID, new double[] { birthTime, birthTime });
            //dp.AxisLabel = clientGanttSeries.Name;
            //clientGanttSeries.Points.Add(dp);
        }

        // constructor 2
        public Client(Itinerary it) : this(0.0, it)
        {
            // 讓空的建構函式，會跑到 public Client(double 0.0)
        }

        /// <summary>
        /// 顧客進入排隊等待
        /// </summary>
        /// <param name="time"></param>
        public void EnterQueueToWait(double time)
        {
            CurrentServer = null;
            CurrentState = ClientState.WaitInQueue;
            currentItineraryItemID++;
        }

        public void EnterNodeDirectlyGetService(double time)
        {
            currentItineraryItemID++;
        }

        /// <summary>
        /// 顧客接受服務
        /// </summary>
        /// <param name="time"></param>
        /// <param name="server"></param>
        public void ReceiveService(double time, Server server)
        {
            CurrentState = ClientState.BeingServed;
            CurrentServer = server;
            if (lastTime != time)
            {
                DataPoint dp = new DataPoint(ganttID, new double[] { lastTime, time });
                dp.AxisLabel = ClientName;
                dp.Color = Color.Gray; // End wait in queue state
                dp.BorderColor = Color.Black;
                clientGanttSeries.Points.Add(dp);
                lastTime = time;
            }
        }

        /// <summary>
        /// 顧客前往下一站 (next service node)，成功 or 失敗
        /// </summary>
        /// <param name="time"></param>
        /// <param name="events"></param>
        /// <returns></returns>
        public bool TryEnterNextServiceNode(double time, out List<DiscreteEvent> events)
        {
            if (currentItineraryItemID < 0) // 進入第一個 Service Node
            {
                // 請 serviceNode[0] 在時間點 time 迎接這個 client(this)，看看是否成功
                bool isEntered = theItinerary.ItineraryItems[0].TheNode.ReceiveAClient(time, this, out events);
                return isEntered; 
            }
            else // 若 current node 不是 null，要先看 current node 目前是在哪個 service node
            {
                // endup last service in previous service node
                // add service gantt
                DataPoint dp = new DataPoint(ganttID, new double[] { lastTime, time });
                dp.AxisLabel = ClientName;
                clientGanttSeries.Points.Add(dp);
                dp.BorderColor = Color.Black;
                if (CurrentState == ClientState.BeingServed)
                    dp.Color = Color.LightPink; // end service
                else if (CurrentState == ClientState.Dwell)
                    dp.Color = Color.Gold;

                lastTime = time; // update last time

                int nextID = currentItineraryItemID + 1;
                if (nextID < theItinerary.ItineraryItems.Count) // 還有下一站
                {
                    // Move to next node
                    bool isEntered = theItinerary.ItineraryItems[nextID].TheNode.ReceiveAClient(time, this, out events);
                    if (isEntered)
                    {
                        // successfully entered
                        //currentItineraryItemID = nextID; // 移動到下一個 servie node，index +1
                    }
                    else
                    {
                        CurrentState = ClientState.Dwell;
                    }
                    return isEntered;
                }
                else // current node 已經是 itinerary 裡最後一個
                {
                    // Exit the system
                }
            }
            events = null;
            return true; // complete the itinerary
        }

        /// <summary>
        /// 設定 Client 的圖形位置
        /// </summary>
        /// <param name="cht"></param>
        /// <param name="area"></param>
        public static void SetClientChartAndArea(Chart cht, ChartArea area)
        {
            clientChart = cht;
            clientChartArea = area;
        }

        /// <summary>
        /// Reset Client Count to zero
        /// </summary>
        public static void ResetClientCounter()
        {
            count = 1;
        }

        public void EscapeFromDwellState(double time)
        {
            DataPoint dp = new DataPoint(ganttID, new double[] { lastTime, time });
            dp.AxisLabel = ClientName;
            dp.Color = Color.Gold; // under servicing
            clientGanttSeries.Points.Add(dp);

            lastTime = time;
        }
    }
}
