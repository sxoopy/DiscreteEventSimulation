using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    public class ClientGroup
    {
        static int count = 1;
        public Itinerary theItinerary;

        public string GroupTitle {get; set;}
        
        public double RelativeFrequency { get; set; } = 100;

        public ClientGroup(Itinerary it)
        {
            theItinerary = it;
            GroupTitle = $"Client {it.Title} {count++}";
        }

        public override string ToString()
        {
            return GroupTitle;
        }
    }
}
