using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace DES
{
    /// <summary>
    /// Responsibility: 紀錄時間變化
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter)), Editor(typeof(TimedQueueEditor), typeof(UITypeEditor))]
    public class TimedQueue : IDEScomponent
    {
        #region DataField
        // 需要記錄歷史資料
        protected static int serialCount = 1;
        static Chart queueChart = null;
        static ChartArea queueChartArea = null;
        protected int maxLength;
        protected double timeAvgLength;
        protected List<Client> clientQueue = new List<Client>(); // 需要隨時變化，所以使用 Queue 這種資料型態
        Series queueLengthSeries = new Series("Clients Count in Queue");
        int clientPassed; // 經果這條 queue 的人數
        int queueCapacity = 10;

        // 用 lastTime, lastLength 來記錄 queue 的資料，不再使用之前的 List<double> times, List<int> lengths
        protected double lastTime;
        protected int lastLength;

        #endregion

        #region Property

        public int CurrentClientCount { get => clientQueue.Count; }
        public Series QueueLengthSeries { get => queueLengthSeries; }
        public bool IsCapable
        {
            get
            {
                if (CapacityLimitEnabled)
                {
                    return CurrentClientCount >= queueCapacity ? false : true;
                }
                else return true;
                
            }
        }

        [Category("Queue Settings"), Description("Queue Series 顯示名稱")]
        public string QueueName
        {
            set => queueLengthSeries.Name = value;
            get => queueLengthSeries.Name;
        }

        [Category("Model"), Description("The limit of number of clients can be queued.")]
        public int QueueCapacity
        {
            get => queueCapacity;
            set
            {
                if (value <= 0) return;
                queueCapacity = value;
            }
        }

        [Category("Model"), Description("Enable the capacity limit to make a capacity queue.")]
        public bool CapacityLimitEnabled { set; get; } = false;        
        #endregion

        /// <summary>
        /// Constructor of Timed Queue
        /// </summary>
        public TimedQueue()
        {
            queueLengthSeries.Name = $"Queue {serialCount++}";
            queueLengthSeries.ChartType = SeriesChartType.StepLine;
            queueLengthSeries.BorderWidth = 2;
        }

        public static void SetQueueChartAndArea(Chart cht, ChartArea area)
        {
            queueChart = cht;
            queueChartArea = area;
        }
        public virtual void Reset()
        {
            lastTime = 0;
            lastLength = 0;
            clientPassed = 0;
            clientQueue.Clear();
            maxLength = 0;
            timeAvgLength = 0;
            queueLengthSeries.Points.Clear();
            queueLengthSeries.Points.AddXY(lastTime, lastLength);

            if (queueChart != null)
            {
                if (!queueChart.Series.Contains(queueLengthSeries))
                    queueChart.Series.Add(queueLengthSeries);
                if (queueChartArea != null)
                    queueLengthSeries.ChartArea = queueChartArea.Name;
            }
            
        }

        /// <summary>
        /// insert a client into the queue at rear
        /// </summary>
        /// <param name="theClient"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public virtual bool EnQueue(Client theClient, double time)
        {
            if (CapacityLimitEnabled && CurrentClientCount >= queueCapacity)
            {
                return false;
            }
            else
            {
                clientQueue.Add(theClient);
                // 畫圖加入這個點
                queueLengthSeries.Points.AddXY(time, lastLength + 1);
                // 更新 maxLength, TimeAvgLength
                timeAvgLength += lastLength * (time - lastTime);

                lastLength += 1;
                lastTime = time;

                if (lastLength > maxLength) maxLength = lastLength;
                return true;
            }
        }

        /// <summary>
        /// delete a client from the queue at front
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public virtual Client DeQueue(double time)
        {
            BeforeDeQueueAClient(time);
            Client target = clientQueue[0];
            clientQueue.Remove(target);
            return target;
        }

        protected void BeforeDeQueueAClient(double time)
        {
            queueLengthSeries.Points.AddXY(time, lastLength - 1);
            timeAvgLength += lastLength * (time - lastTime);
            lastLength--;
            lastTime = time;
            clientPassed++;
        }

        public string DisplaySimulationResults()
        {
            StringBuilder sb = new StringBuilder();
            if (CapacityLimitEnabled)
                sb.AppendLine($"--- Capacity Queue: {QueueName} ---");
            else
                sb.AppendLine($"--- Infinite Queue: {QueueName} ---");

            sb.AppendLine($"Number of Clients Passed: {clientPassed}");
            sb.AppendLine($"Maximal Length: {maxLength}");
            sb.AppendLine($"Time-averaged Length: {Math.Round(timeAvgLength / lastTime, 4)}");
            return sb.ToString();
        }

        public override string ToString()
        {
            if (CapacityLimitEnabled)
                return $"Capacitied {QueueName}";
            else
                return $"Infinite {QueueName}";
        }

        public virtual void SaveToFileStream(StreamWriter sw)
        {
            sw.WriteLine($"Name: {QueueName}");
            sw.WriteLine($"CapacityLimit: {queueCapacity}");
            sw.WriteLine($"CapacityLimitEnabled: {CapacityLimitEnabled}");
        }

        public virtual void ReadFromFileStream(StreamReader sr)
        {
            string str = sr.ReadLine();
            QueueName = str.Substring(str.IndexOf(":") + 1).Trim();
            str = sr.ReadLine();
            queueCapacity = Convert.ToInt32(str.Substring(str.IndexOf(":") + 1)); // convert 會處理掉空格，可以不用 trim
            str = sr.ReadLine();
            CapacityLimitEnabled = Convert.ToBoolean(str.Substring(str.IndexOf(":") + 1)); // convert 會處理掉空格，可以不用 trim
        }
    }
}
