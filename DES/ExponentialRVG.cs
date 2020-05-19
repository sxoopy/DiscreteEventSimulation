using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    public class ExponentialRVG : RandomVariateGenerator
    {      
        public ExponentialRVG(int seed,double mean)
        {
            theoreticMean = mean;
            //lowerBound = lb;
            //upperBound = ub;
            SetSeed(seed);
        }
        public ExponentialRVG(double mean)
        {
            theoreticMean = mean;
            rnd = new Random();
        }
        public double Average
        {
            get => theoreticMean;
            set
            {
                if (value < 0) return;
                theoreticMean = value;
            }
        }

        public override double NextDouble()
        {
            return -theoreticMean * Math.Log(1 - rnd.NextDouble());
        }

        public override string ToString()
        {
            return $"Exponential(avg = {Average})";
        }

        public override void SaveToFileStream(StreamWriter sw)
        {
            base.SaveToFileStream(sw);
            sw.WriteLine($"AverageParameter: {theoreticMean}");
        }

        public override void ReadFromFileStream(StreamReader sr)
        {
            base.ReadFromFileStream(sr);
            string str = sr.ReadLine();
            theoreticMean = Convert.ToDouble(str.Substring(str.IndexOf(":") + 1));
        }
    }
}
