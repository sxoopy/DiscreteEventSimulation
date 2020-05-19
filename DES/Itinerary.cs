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
    public class Itinerary
    {
        static int count = 1;

        public string Title { get; set; }

        [Editor(typeof(ItineraryItemListEditor), typeof(UITypeEditor))]
        public List<ItineraryItem> ItineraryItems { get; set; } = new List<ItineraryItem>();

        public Itinerary()
        {
            Title = $"Itinerary {count++}";
        }

        public override string ToString()
        {
            return Title;
        }

        public void SaveToFileStream(StreamWriter sw)
        {
            sw.WriteLine($"Title: {Title}");
            sw.WriteLine($"NumberOfItems: {ItineraryItems.Count}");
            foreach (ItineraryItem item in ItineraryItems)
            {
                sw.WriteLine($"NodeName: {item.TheNode.Name}");
                if (item.ServiceTimeGenerator == null)
                    sw.WriteLine($"ServiceTimeGenerator: null");
                else
                {
                    sw.WriteLine($"ServiceTimeGenerator: {item.ServiceTimeGenerator.GetType().Name}");
                    item.ServiceTimeGenerator.SaveToFileStream(sw);
                }
                
            }
        }

        public void ReadFromFileStream(StreamReader sr, List<ServiceNode> nodeList)
        {
            string str = sr.ReadLine();
            Title = str.Substring(str.IndexOf(":") + 1).Trim();
            str = sr.ReadLine();
            int num = Convert.ToInt32(str.Substring(str.IndexOf(":") + 1).Trim());
            ItineraryItems.Clear();
            for (int i = 0; i < num; i++)
            {
                str = sr.ReadLine();
                string nodeName = str.Substring(str.IndexOf(":") + 1).Trim();
                ServiceNode node = null;
                foreach (ServiceNode sn in nodeList)
                {                   
                    if (sn.Name == nodeName)
                    {
                        node = sn;
                        break;
                    }
                }
                if (node == null)
                    throw new Exception($"Node with name {nodeName} is not found");
                ItineraryItem item = new ItineraryItem(node);
                str = sr.ReadLine();
                str = str.Substring(str.IndexOf(":") + 1).Trim(); // get service time generator type
                if (str != "Null")
                {
                    item.ServiceTimeGenerator = RandomVariateGenerator.CreateRVGbyType(str);
                    if (item.ServiceTimeGenerator != null)
                        item.ServiceTimeGenerator.ReadFromFileStream(sr);
                }
                ItineraryItems.Add(item);
            }
        }
    }

    public class ItineraryItem
    {
        static int count = 1;

        public string Name { get; set; }
        public ServiceNode TheNode { get; set; } = null;
        public RandomVariateGenerator ServiceTimeGenerator { get; set; } = null;

        public ItineraryItem()
        {
            Name = $"Item {count++}";
        }

        public ItineraryItem(ServiceNode sn)
        {
            Name = $"Item {count++}";
            TheNode = sn;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
