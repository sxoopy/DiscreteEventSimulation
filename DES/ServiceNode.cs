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
    public enum ServiceNodeType
    {
        None, SingleQueueNode, MultipleQueueNode
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ServiceNode : IDEScomponent
    {
        #region Datafield

        // A service node consists of several servers and a timed queue
        // 最上層的 service node 只會放 servers，沒有 queue，因為下層有個別不同的 queue 種類
        //protected List<Server> servers = new List<Server>();
        protected EventHookList<Server> servers = new EventHookList<Server>();
        protected int clientEntered = 0; // 總共進來 sevice node 的 client 個數
        protected int clientExited = 0; // 進不來的 client 個數
        protected static int count = 1;
        public static DESmodel TheDESmodel;       
        protected List<Server> blockedServers = new List<Server>(); // keep server references who are blocked by clients intending to enter this node
        #endregion

        #region Property

        public virtual List<Server> Servers { get => servers; }

        [Category("Model"), Description("Service Node 顯示名稱")]
        public string Name { get; set; }

        [Category("Simulation"), Description("Number of clients currently in the node")]
        public int ClientInNode { get => clientEntered - clientExited; }

        #endregion

        /// <summary>
        /// 接收顧客
        /// </summary>
        /// <param name="time"></param>
        /// <param name="client"></param>
        /// <param name="generatedEvents"></param>
        /// <returns></returns>
        public virtual bool ReceiveAClient(double time, Client client, out List<DiscreteEvent> generatedEvents)
        {
            throw new Exception("No implementation");
        }

        /// <summary>
        /// 服務完顧客，送出顧客
        /// </summary>
        /// <param name="client"></param>
        /// <param name="server"></param>
        /// <param name="exitTime"></param>
        /// <returns></returns>
        public bool ExitAClient(Client client, Server server, double exitTime)
        {
            List<DiscreteEvent> newEvents;
            bool isOK = client.TryEnterNextServiceNode(exitTime, out newEvents);

            if (newEvents != null)
            {
                foreach (DiscreteEvent de in newEvents)
                    TheDESmodel.InsertAnEvent(de);
            }
            if (isOK) clientExited++;
            return isOK;
        }

        public virtual void ResetSimulation(ref int sid)
        {
            throw new Exception("No implementation");
        }

        public virtual string DisplaySimulationResults()
        {
            throw new Exception("No implementation");
        }

        public virtual void SaveToFileStream(StreamWriter sw)
        {
        }


        public void ReceiveBlockedClient(double currentTime)
        {
            if (blockedServers.Count > 0)
            {
                Server headServer = blockedServers[0];
                blockedServers.Remove(headServer);
                List<DiscreteEvent> newEvents = headServer.CompleteCurrentService(currentTime);
                if (newEvents != null)
                {
                    foreach (DiscreteEvent de in newEvents)
                    {
                        TheDESmodel.InsertAnEvent(de);
                    }
                }
            }
        }

        public virtual void ReadFromFileStream(StreamReader sr)
        {
        }

        public virtual void ConvertItineraryNamesToReferences(List<Itinerary> itineraries)
        {
            throw new NotImplementedException();
        }
    }
}
