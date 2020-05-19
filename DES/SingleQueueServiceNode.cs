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
    /// One Queue and One Server
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SingleQueueServiceNode : ServiceNode
    {

        // Single Queue Service Node consists of several servers and several timed queues
        TimedQueue queue = new TimedQueue();
        public TimedQueue Queue { get => queue; set => queue = value; }


        [Category("Model"), Description("Dynamic list of servers installed in the node")]
        [Editor(typeof(ServerListEditor), typeof(UITypeEditor))]
        public override List<Server> Servers { get => servers; }

        /// <summary>
        /// Constructor of One Queue Service Node (method 1)
        /// </summary>
        public SingleQueueServiceNode()
        {
            Name = $"Single Queue Node {count++}";
        }

        /// <summary>
        /// Constructor of One Queue Service Node (method2)
        /// </summary>
        /// <param name="numServers"></param>
        public SingleQueueServiceNode(int numServers)
        {
            Name = $"Single Queue Node {count++}";

            // subscribe list add event
            servers.ItemAdded += Servers_ItemAdded;

            Server s;
            for (int i = 0; i < numServers; i++)
            {
                s = new Server(this);
                s.TargetQueue = queue;
                //s.TargetQueueChanged += S_TargetQueueChanged;
                servers.Add(s);
            }            
        }

        private void Servers_ItemAdded(object sender, EventArgs e)
        {
            Server addedServer = (Server)sender;
            addedServer.UpdateTargetQueue(queue);
            addedServer.TargetQueueChanged += S_TargetQueueChanged;
        }

        private void S_TargetQueueChanged(object sender, EventArgs e)
        {
            Server targeServer = (Server)sender;
            for (int i = 0; i < servers.Count; i++)
            {
                if (servers[i] == targeServer) continue;
                servers[i].UpdateTargetQueue(targeServer.TargetQueue);
                //servers[i].TargetQueue = targeServer.TargetQueue;
            }
            queue = targeServer.TargetQueue;
        }

        /// <summary>
        /// Service node: 接收到一個 Client，決定這個 Client 是否要進入queue
        /// </summary>
        /// <param name="time"></param>
        /// <param name="client"></param>
        /// <param name="server"></param>
        /// <param name="generatedEvents"></param>
        /// <returns></returns>
        public override bool ReceiveAClient(double time, Client client, out List<DiscreteEvent> generatedEvents)
        {
            Server aFreeServer = null;
            foreach (Server s in servers)
            {
                if (s.CurrentState == ServerState.Free) // 如果 servers 裡有空的
                {
                    aFreeServer = s; // 把第一個找到的 server 設為 free server，之後能讓 client 排進去剩下不用找，break
                    break;
                }
            }
            if (aFreeServer != null) // 有 free server，client 進入 server，不進入 queue
            {
                client.EnterNodeDirectlyGetService(time);
                DiscreteEvent ev = aFreeServer.ServeAClient(time, client);
                if (ev == null) // 如果接收到的是 null，防呆
                    generatedEvents = null;
                else
                {
                    // 有接收到新的 Discrete Event，加入 list
                    generatedEvents = new List<DiscreteEvent>();
                    generatedEvents.Add(ev);
                }
                clientEntered++;
                return true;
            }
            else // 沒有 free server，client 進入 queue
            {
                generatedEvents = null;
                // client 進入 queue ，不會產生新的 Discrete Event
                if (queue.IsCapable)
                {
                    queue.EnQueue(client, time);
                    client.EnterQueueToWait(time); // client update itinerary index
                    clientEntered++;
                    return true;
                }
                else // not capable
                {
                    // keep the blocked server in the blocked list
                    // FIFO
                    if (client.CurrentServer != null)
                        blockedServers.Add(client.CurrentServer);
                    return false;
                }
            }
        }

        ///// <summary>
        ///// Set the chart of Server Gantt and Queue Length
        ///// </summary>
        ///// <param name="cht"></param>
        //public override void SetServerAndQueueChart(Chart cht)
        //{
        //    foreach (Server s in servers)
        //    {
        //        cht.Series.Add(s.ServerGanttSeries);
        //        s.ServerGanttSeries.ChartArea = cht.ChartAreas[0].Name;
        //    }
        //    cht.Series.Add(queue.QueueLengthSeries);
        //    queue.QueueLengthSeries.ChartArea = cht.ChartAreas[1].Name;
        //}

        public override void ResetSimulation(ref int serverSerialID)
        {
            clientEntered = 0;
            foreach (Server s in servers)
            {
                s.TargetQueue = queue;
                s.ResetSimulation(this, ref serverSerialID);
            }
            queue.Reset();
        }

        public override void ConvertItineraryNamesToReferences(List<Itinerary> itineraries)
        {
            if (queue is PriorityQueue)
            {
                ((PriorityQueue)queue).ConvertInineraryNamesToReferences(itineraries);
            }
        }

        public override string DisplaySimulationResults()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Title: {Name}");
            sb.AppendLine($"Clients Visited: {clientEntered}   Clients Exited: {clientExited}   Clients in node: {ClientInNode}");
            foreach (Server s in servers)
            {
                sb.Append(s.DisplaySimulationResults());
            }
            sb.Append(queue.DisplaySimulationResults());
            return sb.ToString();
        }

        public override void SaveToFileStream(StreamWriter sw)
        {
            sw.WriteLine($"Title: {Name}");
            sw.WriteLine($"NumberOfServers: {servers.Count}");
            foreach (Server s in servers)
            {
                s.SaveToFileStream(sw);
            }
            sw.WriteLine(queue.GetType().Name);
            queue.SaveToFileStream(sw);
        }
        public override string ToString()
        {
            return Name;
        }

        public override void ReadFromFileStream(StreamReader sr)
        {
            string str = sr.ReadLine();
            Name = str.Substring(str.IndexOf(":") + 1).Trim();
            str = sr.ReadLine();
            int num = Convert.ToInt32(str.Substring(str.IndexOf(":") + 1).Trim());
            servers.Clear();
            for (int i = 0; i < num; i++)
            {
                Server s = new Server(this);
                s.ReadFromFileStream(sr);
                servers.Add(s);
            }
            str = sr.ReadLine(); // queue type
            switch (str)
            {
                case "TimedQueue":
                    queue = new TimedQueue();
                    break;
                case "PriorityQueue":
                    queue = new PriorityQueue();
                    break;
            }
            queue.ReadFromFileStream(sr);
        }
    }
}
