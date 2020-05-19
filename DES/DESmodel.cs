using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace DES
{
    /// <summary>
    /// Run One Event
    /// </summary>
    public class DESmodel : IDEScomponent
    {
        #region Datafield

        List<ServiceNode> nodes = new List<ServiceNode>();
        ClientGenerator clientGtor;
        List<Itinerary> itineraries = new List<Itinerary>();
        SortedList<double, DiscreteEvent> futureEventList = new SortedList<double, DiscreteEvent>();
        double clockTime;
        
        #endregion

        #region Property

        [Category("Simulation"), Description("當前事件處理時間"), Browsable(false)]
        public double ClockTime { get => clockTime; }

        [Category("Model"), Description("顧客產生器")]
        public ClientGenerator ClientGtor { get => clientGtor; set => clientGtor = value; }

        [Category("Simulation"), Description("模擬停止時間")]
        public double ClientCeaseTime { set; get; } = 400.0; // 停止條件

        [Editor(typeof(NodeListEditor), typeof(UITypeEditor))]
        [Category("Model"), Description("Dynamic list of serice nodes")]
        public List<ServiceNode> Nodes { get => nodes; set => nodes = value; }

        [Category("Model"), Description("Dynamic list of itinerary")]
        public List<Itinerary> Itineraries { get => itineraries; }
        #endregion

        /// <summary>
        /// Constructor of Discrete Event Simulation Model
        /// </summary>
        public DESmodel()
        {
            DiscreteEvent.TheDESmodel = this; // 創建 DESmodel 時，讓 DiscreteEvent 和 ServiceNode 認得此 DESmodel，建立關聯關係
            ServiceNode.TheDESmodel = this;
            ItineraryItemEditorForm.TheModel = this;
            ClientGroupListEditorForm.TheModel = this;
            // 創建 DESmodel 就要 new 一個 Client Generator
            clientGtor = new ClientGenerator(new ExponentialRVG(1.0));
        }

        public static DESmodel CreateSSQModel()
        {
            DESmodel model = new DESmodel();

            ServiceNode node = new SingleQueueServiceNode(1);
            model.nodes.Add(node);

            Itinerary it = new Itinerary();
            it.Title = "SSQ Client";
            it.ItineraryItems.Add(new ItineraryItem(node));

            model.itineraries.Add(it);

            ClientGroup group = new ClientGroup(it);
            model.clientGtor.ClientGroups.Add(group);

            return model;
        }

        public static DESmodel CreateBandAndMcDonaldModel()
        {
            DESmodel model = new DESmodel();

            // 創建兩個 Node，分別為 Bank 以及 McDonald
            SingleQueueServiceNode bank = new SingleQueueServiceNode(3);
            bank.Name = "Bank";
            bank.Servers[0].ServerName = "Teller 1";
            bank.Servers[1].ServerName = "Teller 2";
            bank.Servers[2].ServerName = "Teller 3";
            bank.Servers[0].TargetQueue.QueueName = "Queue For All Teller";
            model.nodes.Add(bank);

            MultiQueueServiceNode McDonald = new MultiQueueServiceNode(2);
            McDonald.Name = "McDonald";
            McDonald.Servers[0].TargetQueue = new TimedQueue();
            McDonald.Servers[0].TargetQueue.CapacityLimitEnabled = true;
            McDonald.Servers[0].TargetQueue.QueueCapacity = 5;
            McDonald.Servers[1].TargetQueue = new TimedQueue();
            McDonald.Servers[1].TargetQueue.CapacityLimitEnabled = true;
            McDonald.Servers[1].TargetQueue.QueueCapacity = 4;
            McDonald.Servers[0].ServerName = "Clerk 1";
            McDonald.Servers[1].ServerName = "Clerk 2";
            McDonald.Servers[0].TargetQueue.QueueName = "Queue for Clerk 1";
            McDonald.Servers[1].TargetQueue.QueueName = "Queue for Clerk 2";
            McDonald.Servers[0].ServiceTimeGenerator = new ExponentialRVG(2);
            McDonald.Servers[1].ServiceTimeGenerator = new ExponentialRVG(2.5);
            model.nodes.Add(McDonald);

            Itinerary it = new Itinerary();
            it.Title = "Bank then McDonald";
            // 只有一種行程，有兩站要走
            it.ItineraryItems.Add(new ItineraryItem(bank));
            it.ItineraryItems.Add(new ItineraryItem(McDonald));
            model.itineraries.Add(it);

            ClientGroup group = new ClientGroup(it);
            model.clientGtor.ClientGroups.Add(group);

            return model;
        }

        public static DESmodel CreateFactoryModel()
        {
            DESmodel model = new DESmodel();

            SingleQueueServiceNode washWorkstation = new SingleQueueServiceNode(1);
            washWorkstation.Name = "Washing WorkStation";
            washWorkstation.Servers[0].ServerName = "W";
            washWorkstation.Servers[0].TargetQueue = new TimedQueue();
            washWorkstation.Servers[0].TargetQueue.CapacityLimitEnabled = true;
            washWorkstation.Servers[0].TargetQueue.QueueCapacity = 10;
            washWorkstation.Servers[0].ServiceTimeGenerator = new ExponentialRVG(1.0);
            model.nodes.Add(washWorkstation);

            MultiQueueServiceNode pressWorkstaion = new MultiQueueServiceNode(3);
            pressWorkstaion.Name = "Press Workstaion";
            pressWorkstaion.Servers[0].TargetQueue = new TimedQueue();
            pressWorkstaion.Servers[0].TargetQueue.CapacityLimitEnabled = true;
            pressWorkstaion.Servers[0].TargetQueue.QueueCapacity = 8;
            pressWorkstaion.Servers[1].TargetQueue = new TimedQueue();
            pressWorkstaion.Servers[1].TargetQueue.CapacityLimitEnabled = true;
            pressWorkstaion.Servers[1].TargetQueue.QueueCapacity = 7;
            pressWorkstaion.Servers[2].TargetQueue = new TimedQueue();
            pressWorkstaion.Servers[2].TargetQueue.CapacityLimitEnabled = true;
            pressWorkstaion.Servers[2].TargetQueue.QueueCapacity = 6;
            pressWorkstaion.Servers[0].ServerName = "P1";
            pressWorkstaion.Servers[1].ServerName = "P2";
            pressWorkstaion.Servers[2].ServerName = "P3";
            pressWorkstaion.Servers[0].TargetQueue.QueueName = "QP1";
            pressWorkstaion.Servers[1].TargetQueue.QueueName = "QP2";
            pressWorkstaion.Servers[2].TargetQueue.QueueName = "QP3";
            pressWorkstaion.Servers[0].ServiceTimeGenerator = null;
            pressWorkstaion.Servers[1].ServiceTimeGenerator = null;
            pressWorkstaion.Servers[2].ServiceTimeGenerator = null;
            model.nodes.Add(pressWorkstaion);

            SingleQueueServiceNode assemblyWorkstation = new SingleQueueServiceNode(2);
            assemblyWorkstation.Name = "Assembly Workstation";
            assemblyWorkstation.Servers[0].ServerName = "Y1";
            assemblyWorkstation.Servers[1].ServerName = "Y2";
            assemblyWorkstation.Servers[0].TargetQueue = new TimedQueue();
            assemblyWorkstation.Servers[0].TargetQueue.CapacityLimitEnabled = true;
            assemblyWorkstation.Servers[0].TargetQueue.QueueCapacity = 10;
            assemblyWorkstation.Servers[1].TargetQueue = assemblyWorkstation.Servers[0].TargetQueue;
            assemblyWorkstation.Servers[0].ServiceTimeGenerator = null;
            assemblyWorkstation.Servers[1].ServiceTimeGenerator = null;
            model.nodes.Add(assemblyWorkstation);

            Itinerary itineraryA = new Itinerary();
            itineraryA.Title = "Product A";
            // 1: W
            itineraryA.ItineraryItems.Add(new ItineraryItem(washWorkstation));
            // 2: P
            ItineraryItem itItem1 = new ItineraryItem(pressWorkstaion);
            itItem1.ServiceTimeGenerator = new ExponentialRVG(4.0);
            itineraryA.ItineraryItems.Add(itItem1);
            // 3: Y
            itItem1 = new ItineraryItem(assemblyWorkstation);
            itItem1.ServiceTimeGenerator = new ExponentialRVG(3.0);
            itineraryA.ItineraryItems.Add(itItem1);

            model.itineraries.Add(itineraryA);

            Itinerary itineraryB = new Itinerary();
            itineraryB.Title = "Product B";
            // 1: W
            itineraryB.ItineraryItems.Add(new ItineraryItem(washWorkstation));
            // 2: P
            ItineraryItem itItem2 = new ItineraryItem(pressWorkstaion);
            itItem2.ServiceTimeGenerator = new ExponentialRVG(3.0);
            itineraryB.ItineraryItems.Add(itItem2);
            // 3: W
            itineraryB.ItineraryItems.Add(new ItineraryItem(washWorkstation));
            // 4: Y
            itItem2 = new ItineraryItem(assemblyWorkstation);
            itItem2.ServiceTimeGenerator = new ExponentialRVG(2.0);
            itineraryB.ItineraryItems.Add(itItem2);

            model.itineraries.Add(itineraryB);


            ClientGroup group = new ClientGroup(itineraryA);
            group.RelativeFrequency = 30;
            model.clientGtor.ClientGroups.Add(group);

            group = new ClientGroup(itineraryB);
            group.RelativeFrequency = 70;
            model.clientGtor.ClientGroups.Add(group);

            return model;
        }

        public static DESmodel CreateComputerModel()
        {
            DESmodel model = new DESmodel();
            model.clientGtor = new ClientGenerator(new ExponentialRVG(10));

            SingleQueueServiceNode Ask = new SingleQueueServiceNode(1);
            Ask.Name = "Ask";
            Ask.Servers[0].ServerName = "Ask1";
            Ask.Servers[0].TargetQueue.QueueName = "Queue For Ask";
            Ask.Servers[0].ServiceTimeGenerator = new ExponentialRVG(10);
            model.nodes.Add(Ask);

            MultiQueueServiceNode Select = new MultiQueueServiceNode(3);
            Select.Name = "Select Hardware";
            Select.Servers[0].TargetQueue = new TimedQueue();
            Select.Servers[0].TargetQueue.CapacityLimitEnabled = true;
            Select.Servers[0].TargetQueue.QueueCapacity = 8;
            Select.Servers[1].TargetQueue = new TimedQueue();
            Select.Servers[1].TargetQueue.CapacityLimitEnabled = true;
            Select.Servers[1].TargetQueue.QueueCapacity = 8;
            Select.Servers[2].TargetQueue = new TimedQueue();
            Select.Servers[2].TargetQueue.CapacityLimitEnabled = true;
            Select.Servers[2].TargetQueue.QueueCapacity = 8;
            Select.Servers[0].ServerName = "Select1";
            Select.Servers[1].ServerName = "Select2";
            Select.Servers[2].ServerName = "Select3";
            Select.Servers[0].TargetQueue.QueueName = "QS1";
            Select.Servers[1].TargetQueue.QueueName = "QS2";
            Select.Servers[2].TargetQueue.QueueName = "QS3";
            Select.Servers[0].ServiceTimeGenerator = new UniformRVG(15, 20);
            Select.Servers[1].ServiceTimeGenerator = new UniformRVG(15, 20);
            Select.Servers[2].ServiceTimeGenerator = new UniformRVG(15, 20);
            model.nodes.Add(Select);

            MultiQueueServiceNode assemblyWorkstation = new MultiQueueServiceNode(6);
            assemblyWorkstation.Name = "Assembly Workstation";
            assemblyWorkstation.Servers[0].ServerName = "Assembly1";
            assemblyWorkstation.Servers[1].ServerName = "Assembly2";
            assemblyWorkstation.Servers[2].ServerName = "Assembly3";
            assemblyWorkstation.Servers[3].ServerName = "Assembly4";
            assemblyWorkstation.Servers[4].ServerName = "Assembly5";
            assemblyWorkstation.Servers[5].ServerName = "Assembly6";
            assemblyWorkstation.Servers[0].TargetQueue = new TimedQueue();
            assemblyWorkstation.Servers[1].TargetQueue = new TimedQueue();
            assemblyWorkstation.Servers[2].TargetQueue = new TimedQueue();
            assemblyWorkstation.Servers[3].TargetQueue = new TimedQueue();
            assemblyWorkstation.Servers[4].TargetQueue = new TimedQueue();
            assemblyWorkstation.Servers[5].TargetQueue = new TimedQueue();
            assemblyWorkstation.Servers[0].TargetQueue.QueueName = "QA1";
            assemblyWorkstation.Servers[1].TargetQueue.QueueName = "QA2";
            assemblyWorkstation.Servers[2].TargetQueue.QueueName = "QA3";
            assemblyWorkstation.Servers[3].TargetQueue.QueueName = "QA4";
            assemblyWorkstation.Servers[4].TargetQueue.QueueName = "QA5";
            assemblyWorkstation.Servers[5].TargetQueue.QueueName = "QA6";
            assemblyWorkstation.Servers[0].ServiceTimeGenerator = new ExponentialRVG(45);
            assemblyWorkstation.Servers[1].ServiceTimeGenerator = new ExponentialRVG(60);
            assemblyWorkstation.Servers[2].ServiceTimeGenerator = new ExponentialRVG(30);
            assemblyWorkstation.Servers[3].ServiceTimeGenerator = new ExponentialRVG(35);
            assemblyWorkstation.Servers[4].ServiceTimeGenerator = new ExponentialRVG(40);
            assemblyWorkstation.Servers[4].ServiceTimeGenerator = new ExponentialRVG(40);
            model.nodes.Add(assemblyWorkstation);

            SingleQueueServiceNode testComputer = new SingleQueueServiceNode(2);
            testComputer.Name = "Test Computer";
            testComputer.Servers[0].ServerName = "Test1";
            testComputer.Servers[1].ServerName = "Test2";
            testComputer.Servers[0].TargetQueue.QueueName = "Queue for Test";
            testComputer.Servers[0].ServiceTimeGenerator = new ExponentialRVG(15);
            testComputer.Servers[1].ServiceTimeGenerator = new ExponentialRVG(15);
            model.nodes.Add(testComputer);

            Itinerary itineraryA = new Itinerary();
            itineraryA.Title = "Professional";
            itineraryA.ItineraryItems.Add(new ItineraryItem(Select));
            itineraryA.ItineraryItems.Add(new ItineraryItem(assemblyWorkstation));
            itineraryA.ItineraryItems.Add(new ItineraryItem(testComputer));
            model.itineraries.Add(itineraryA);

            Itinerary itineraryB = new Itinerary();
            itineraryB.Title = "Normal";
            itineraryB.ItineraryItems.Add(new ItineraryItem(Ask));
            itineraryB.ItineraryItems.Add(new ItineraryItem(Select));
            itineraryB.ItineraryItems.Add(new ItineraryItem(assemblyWorkstation));
            itineraryB.ItineraryItems.Add(new ItineraryItem(testComputer));
            model.itineraries.Add(itineraryB);


            ClientGroup group = new ClientGroup(itineraryA);
            group.RelativeFrequency = 30;
            model.clientGtor.ClientGroups.Add(group);

            group = new ClientGroup(itineraryB);
            group.RelativeFrequency = 70;
            model.clientGtor.ClientGroups.Add(group);

            return model;
        }

        /// <summary>
        /// 處理 futureEventList 的 head
        /// </summary>
        /// <returns></returns>
        public bool RunNextEvent()
        {
            if (futureEventList.Count <= 0)
                return false;
            DiscreteEvent headEvent = futureEventList.Values[0]; // 如果 futureEventList 是空的，不能處理
            clockTime = headEvent.EventTime; // 更新 clocktime
            List<DiscreteEvent> newEventList = headEvent.ProcessEvent();
            futureEventList.RemoveAt(0); // 處理完之後移除

            if (newEventList != null)
            {
                foreach (DiscreteEvent de in newEventList)
                {
                    InsertAnEvent(de);
                }
            }
            return true;
        }

        public void CreateAndInsertNextArrivalEvent()
        {
            if (clockTime > ClientCeaseTime) return;
            else
            {
                double arrivalTime;
                Client c = clientGtor.GenerateAClient(out arrivalTime);
                DiscreteEvent e = new ClientArrivalEvent(c, arrivalTime);
                InsertAnEvent(e);
            }
        }

        public bool InsertAnEvent(DiscreteEvent theEvent)
        {
            futureEventList.Add(theEvent.EventTime, theEvent);
            return true;
        }

        public void ResetSimulation()
        {
            int serverSeriesID = 1;
            clockTime = 0.0;
            futureEventList.Clear();

            foreach (ServiceNode sn in nodes)
            {
                sn.ResetSimulation(ref serverSeriesID);
            }
            clientGtor.ResetSimulation();
            CreateAndInsertNextArrivalEvent(); // add the first client
        }

        /// <summary>
        /// Show the content of Future Event List
        /// </summary>
        /// <returns></returns>
        public string FutureEventListString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (DiscreteEvent de in futureEventList.Values)
            {
                sb.AppendLine($"{de.EventTime:0.000} -> {de.ToString()}");
            }
            sb.AppendLine($"\nClock Time: {clockTime:0.000}");
            return sb.ToString();
        }

        public string DisplaySimulationResults()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--- DES Model ---");
            sb.AppendLine($"Client Arrival Cease Time: {ClientCeaseTime}");
            sb.AppendLine($"Simulation Clock ends at: {clockTime}");
            sb.AppendLine("--- Client Generator --- ");
            sb.AppendLine(clientGtor.DisplaySimulationResults());
            sb.AppendLine($"--- Service Nodes ---");
            foreach (ServiceNode sn in nodes)
            { 
                sb.AppendLine(sn.DisplaySimulationResults());
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return "DES";
        }

        public void SaveToFileStream(StreamWriter sw)
        {
            sw.WriteLine($"CeaseTime: {ClientCeaseTime}");
            sw.WriteLine($"NumberOfNodes: {Nodes.Count}");
            foreach (ServiceNode sn in nodes)
            {
                sw.WriteLine(sn.GetType().Name);
                sn.SaveToFileStream(sw);
            }
            sw.WriteLine($"NumberOfItineraries: {itineraries.Count}");
            foreach (Itinerary it in itineraries)
            {
                it.SaveToFileStream(sw);
            }
            clientGtor.SaveToFileStream(sw);
        }

        public void ReadFromFileStream(StreamReader sr)
        {
            string str;
            string[] data;
            str = sr.ReadLine();
            ClientCeaseTime = Convert.ToDouble(str.Substring(str.IndexOf(":") + 1).Trim());
            str = sr.ReadLine();
            int num = Convert.ToInt32(str.Substring(str.IndexOf(":") + 1).Trim());
            nodes.Clear();
            for (int i = 0; i < num; i++)
            {
                str = sr.ReadLine(); // type name
                ServiceNode sn = null;
                switch (str)
                {
                    case "SingleQueueServiceNode":
                        sn = new SingleQueueServiceNode();
                        break;
                    case "MultiQueueServiceNode":
                        sn = new MultiQueueServiceNode();
                        break;
                }
                nodes.Add(sn);
                sn.ReadFromFileStream(sr);
            }
            str = sr.ReadLine();
            num = Convert.ToInt32(str.Substring(str.IndexOf(":") + 1).Trim());
            itineraries.Clear();
            for (int i = 0; i < num; i++)
            {
                Itinerary it = new Itinerary();
                it.ReadFromFileStream(sr, nodes);
                itineraries.Add(it);
            }
            //Convert itinerary names to itinerary references
            foreach (ServiceNode sn in nodes)
            {
                sn.ConvertItineraryNamesToReferences(itineraries);
            }
            clientGtor = new ClientGenerator();
            clientGtor.ReadFromFileStream(sr, itineraries);
        }
    }
}
