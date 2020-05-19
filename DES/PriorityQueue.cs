using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    public class PriorityQueue : TimedQueue
    {
        #region DataField

        string[] itineraryNames;

        #endregion

        #region Property

        public List<Itinerary> Priorities { set; get; } = new List<Itinerary>();

        #endregion

        public override Client DeQueue(double time)
        {
            Client targetClient = null;
            foreach (Itinerary i in Priorities)
            {
                foreach (Client c in clientQueue)
                {
                    if (c.TheItinerary == i)
                    {
                        targetClient = c;
                        break;
                    }
                }
                if (targetClient != null)
                    break;
            }
            BeforeDeQueueAClient(time);

            if (targetClient == null)
                targetClient = clientQueue[0];

            clientQueue.Remove(targetClient); // remove target client
            return targetClient;
        }

        public void ConvertInineraryNamesToReferences(List<Itinerary> itineraryList)
        {
            if (itineraryNames == null)
                return;
            Priorities.Clear();
            for (int i = 0; i < itineraryNames.Length; i++)
            {
                bool isFound = false;
                foreach (Itinerary it in itineraryList)
                {
                    if (itineraryNames[i] == it.Title)
                    {
                        Priorities.Add(it);
                        isFound = true;
                        break;
                    }
                    if (!isFound)
                        throw new Exception("Itinerary is not found!");
                }
            }
        }

        public override void SaveToFileStream(StreamWriter sw)
        {
            base.SaveToFileStream(sw);
            sw.WriteLine($"NumberOfPriorities: {Priorities.Count}");
            foreach (Itinerary it in Priorities)
            {
                sw.WriteLine(it.Title);
            }
        }

        public override void ReadFromFileStream(StreamReader sr)
        {
            base.ReadFromFileStream(sr);
            string str = sr.ReadLine();
            int num = Convert.ToInt32(str.Substring(str.IndexOf(":") + 1).Trim());
            Priorities.Clear();
            itineraryNames = new string[num];
            for (int i = 0; i < num; i++)
            {
                itineraryNames[i] = sr.ReadLine();
            }
        }
    }
}
