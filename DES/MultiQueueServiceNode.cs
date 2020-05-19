using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace DES
{
    /// <summary>
    /// Multiple Queue and Multiple Server (1 server to 1 queue)
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MultiQueueServiceNode : ServiceNode
    {
        [Category("Model"), Description("Dynamic list of servers installed in the node")]
        public override List<Server> Servers { get => servers; }

        public MultiQueueServiceNode(int numServers = 2)
        {
            Name = $"Multiple Queue Node {count++}";
            Server s;
            TimedQueue q;
            for (int i = 0; i < numServers; i++)
            {
                s = new Server(this);
                q = new TimedQueue();
                s.TargetQueue = q; // 建立 server 與 queue 的關聯關係
                servers.Add(s);
            }
        }
        public override void ResetSimulation(ref int serverSerialID)
        {
            clientEntered = 0;
            clientExited = 0;

            foreach (Server s in servers)
            {
                s.ResetSimulation(this, ref serverSerialID);
                s.TargetQueue.Reset();
            }
        }

        public override string ToString()
        {
            return $"{Name} ({servers.Count} Servers)";
        }

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
                client.EnterNodeDirectlyGetService(time); // client update itinerary index
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
                // 進入 queue，不會產生新的 Discrete Event
                // Find the shortest queue to queue the client.
                int minLength = int.MaxValue;
                TimedQueue minCapableQueue = null;

                // check whether all queues are not capable
                // queue 綁定在 server 上
                bool allIncapable = true;
                foreach (Server s in servers)
                {
                    if (s.TargetQueue.IsCapable)
                    {
                        allIncapable = false;
                        break;
                    }
                } 

                if (allIncapable)
                {
                    // keep the blocked server in the blocked list
                    // FIFO
                    if (client.CurrentServer != null)
                        blockedServers.Add(client.CurrentServer);
                    return false;
                }
                else
                {
                    foreach (Server s in servers)
                    {
                        if (s.TargetQueue.IsCapable)
                        {
                            if (s.TargetQueue.CurrentClientCount < minLength)
                            {
                                minLength = s.TargetQueue.CurrentClientCount;
                                minCapableQueue = s.TargetQueue;
                            }
                        }
                    }
                    if (minCapableQueue == null)
                        throw new Exception("Wrong Logic!");                    
                    else // client 進入 minQueue
                    {
                        minCapableQueue.EnQueue(client, time);
                        client.EnterQueueToWait(time); // client update itinerary index
                        clientEntered++;
                        return true;
                    }
                }
            }
        }

        public override void ConvertItineraryNamesToReferences(List<Itinerary> itineraries)
        {
            foreach (Server s in servers)
            {
                if (s.TargetQueue is PriorityQueue)
                    ((PriorityQueue)s.TargetQueue).ConvertInineraryNamesToReferences(itineraries);
            }
        }

        public override string DisplaySimulationResults()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Title: {Name}");
            sb.AppendLine($"Clients Visited: {clientEntered}   Clients Exited: {clientExited}   Clients in node: {ClientInNode}");
            for (int i = 0; i < servers.Count; i++)
            {
                sb.Append(servers[i].DisplaySimulationResults());
                sb.Append(servers[i].TargetQueue.DisplaySimulationResults());
            }
            return sb.ToString();
        }

        public override void SaveToFileStream(StreamWriter sw)
        {
            sw.WriteLine($"Title: {Name}");
            sw.WriteLine($"NumberOfServers: {servers.Count}");
            for (int i = 0; i < servers.Count; i++)
            {
                servers[i].SaveToFileStream(sw);
                sw.WriteLine(servers[i].TargetQueue.GetType().Name);
                servers[i].TargetQueue.SaveToFileStream(sw);
            }
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
                str = sr.ReadLine(); // queue type
                switch (str)
                {
                    case "TimedQueue":
                        s.TargetQueue = new TimedQueue();
                        break;
                    case "PriorityQueue":
                        s.TargetQueue = new PriorityQueue();
                        break;
                }
                s.TargetQueue.ReadFromFileStream(sr);
                servers.Add(s);
            }
        }
    }
}
