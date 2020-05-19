using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    public class DiscreteEvent
    {
        protected static DESmodel theDESmodel;
        protected double eventTime; // event 發生的時間

        public static DESmodel TheDESmodel { get => theDESmodel; set => theDESmodel = value; }
        public double EventTime { get => eventTime; }

        /// <summary>
        /// 有新的離散事件產生的話，就回傳一個 DiscreteEvent 的 list
        /// </summary>
        /// <returns></returns>
        public virtual List<DiscreteEvent> ProcessEvent()
        {
            throw new Exception("No implementation");
        }
    }


    public class ClientArrivalEvent : DiscreteEvent
    {
        protected Client client;

        public ClientArrivalEvent(Client c, double time)
        {
            client = c;
            eventTime = time;
        }

        /// <summary>
        /// 有新的離散事件產生的話，就回傳一個 Discrete Event 的 list
        /// </summary>
        /// <returns></returns>
        public override List<DiscreteEvent> ProcessEvent()
        {
            // ask model tp generate arrival event
            theDESmodel.CreateAndInsertNextArrivalEvent();
            List<DiscreteEvent> events = null;
            //List<DiscreteEvent> newEvents = client.EnterNextServiceNode(eventTime);
            bool isOK = client.TryEnterNextServiceNode(eventTime, out events);
            return events;
        }

        public override string ToString()
        {
            return "Arrival Event";
        }
    }


    public class ServiceCompleteEvent : DiscreteEvent
    {
        Server theServer;
        public ServiceCompleteEvent(Server server, double eTime)
        {
            theServer = server;
            eventTime = eTime;
        }

        /// <summary>
        /// 有新的離散事件產生的話，就回傳一個 DiscreteEvent 的 list
        /// </summary>
        /// <returns></returns>
        public override List<DiscreteEvent> ProcessEvent()
        {
            return theServer.CompleteCurrentService(eventTime);
        }

        public override string ToString()
        {
            return "Completion Event";
        }
    }
}
