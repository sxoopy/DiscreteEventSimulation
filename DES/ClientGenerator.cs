using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DES
{
    /// <summary>
    /// Client 產生器
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ClientGenerator
    {
        #region DataField

        static Random rnd = new Random();
        RandomVariateGenerator interArrivalTimeGenerator;
        double lastTime;
        int clientGenerated;

        #endregion

        #region Property
        [Category("Generator")]
        public RandomVariateGenerator InterArrivalTimeGenerator { get => interArrivalTimeGenerator; set => interArrivalTimeGenerator = value; }

        [Editor(typeof(ClientGroupListEditor), typeof(UITypeEditor))]
        public List<ClientGroup> ClientGroups { get; set; } = new List<ClientGroup>();

        public string Title { set; get; } = "ClientGenerator";
        #endregion

        public ClientGenerator()
        {

        }

        /// <summary>
        /// Constructor of Client Generator
        /// </summary>
        /// <param name="gtor"></param>
        public ClientGenerator(RandomVariateGenerator gtor)
        {
            interArrivalTimeGenerator = gtor;
            clientGenerated = 0;
            lastTime = 0;
        }      

        public virtual Client GenerateAClient(out double time)
        {
            clientGenerated++;
            lastTime += interArrivalTimeGenerator.NextDouble();
            time = lastTime;
            Itinerary clientItinerary = null;
            double total = 0;
            double sum = 0;
            foreach (ClientGroup group in ClientGroups)
            {
                total += group.RelativeFrequency;
            }
            total = rnd.NextDouble() * total;
            foreach (ClientGroup group in ClientGroups)
            {
                sum += group.RelativeFrequency;
                if (total < sum)
                {
                    clientItinerary = group.theItinerary;
                    break;
                }
            }
            return new Client(lastTime, clientItinerary);
        }

        public override string ToString()
        {
            return Title;
        }

        public void ResetSimulation()
        {
            clientGenerated = 0;
            lastTime = 0;
        }

        public string DisplaySimulationResults()
        {
            return $"Client Generated: {clientGenerated}";
        }

        public void SaveToFileStream(StreamWriter sw)
        {
            sw.WriteLine($"ClientGeneratorTitle: {Title}");

            sw.WriteLine($"InterArrivalTimeGeneratorType: {interArrivalTimeGenerator.GetType().Name}");
            interArrivalTimeGenerator.SaveToFileStream(sw);

            sw.WriteLine($"NumberOfClientGroup: {ClientGroups.Count}");
            foreach (ClientGroup cg in ClientGroups)
            {
                sw.WriteLine($"Name: {cg.GroupTitle}");
                sw.WriteLine($"ItineraryTitle: {cg.theItinerary.Title}");
                sw.WriteLine($"RelativeFrequency: {cg.RelativeFrequency}");
            }
        }

        public void ReadFromFileStream(StreamReader sr, List<Itinerary> itineraryList)
        {
            string str;
            str = sr.ReadLine();
            Title = str.Substring(str.IndexOf(":") + 1).Trim();
            str = sr.ReadLine();
            str = str.Substring(str.IndexOf(":") + 1).Trim();
            interArrivalTimeGenerator = RandomVariateGenerator.CreateRVGbyType(str);
            if (interArrivalTimeGenerator == null)
                throw new Exception("Client Generator doesn't equipped with an interarrival time generator");
            interArrivalTimeGenerator.ReadFromFileStream(sr);
            ClientGroups.Clear();
            str = sr.ReadLine();
            int num = Convert.ToInt32(str.Substring(str.IndexOf(":") + 1));
            for (int i = 0; i < num; i++)
            {
                ClientGroup cg;
                str = sr.ReadLine();
                string name = str.Substring(str.IndexOf(":") + 1).Trim();
                str = sr.ReadLine();
                string itineraryName = str.Substring(str.IndexOf(":") + 1).Trim();
                Itinerary it = null;
                foreach (Itinerary iti in itineraryList)
                {
                    if (iti.Title == itineraryName)
                    {
                        it = iti;
                        break;
                    }
                }
                if (it == null)
                    throw new Exception($"Itinerary Title {itineraryName} is not found!");
                cg = new ClientGroup(it);
                cg.GroupTitle = name;
                str = sr.ReadLine();
                cg.RelativeFrequency = Convert.ToDouble(str.Substring(str.IndexOf(":") + 1));
                ClientGroups.Add(cg);
            }
        }
    }
}
