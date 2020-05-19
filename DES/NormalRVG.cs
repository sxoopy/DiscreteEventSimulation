using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    public class NormalRVG : RandomVariateGenerator
    {
        double theoreticSTD;
        public NormalRVG(int seed, double mean, double std)
        {
            theoreticMean = mean;
            theoreticSTD = std;
            //lowerBound = lb;
            //upperBound = ub;
            SetSeed(seed);
        }
        public NormalRVG(double mean, double std)
        {
            theoreticMean = mean;
            theoreticSTD = std;
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

        public double STD
        {
            get => theoreticSTD;
            set
            {
                if (value < 0) return;
                theoreticSTD = value;
            }
        }

        public override double NextDouble()
        {
            return -theoreticMean * 3 * rnd.NextDouble() * theoreticSTD;
        }

        public override string ToString()
        {
            return $"Normal(avg = {Average}, std = {STD})";
        }

        public override void SaveToFileStream(StreamWriter sw)
        {
            base.SaveToFileStream(sw);
            sw.WriteLine($"AverageParameter: {theoreticMean}");
            sw.WriteLine($"STDParameter: {theoreticSTD}");
        }

        public override void ReadFromFileStream(StreamReader sr)
        {
            base.ReadFromFileStream(sr);
            string str = sr.ReadLine();
            theoreticMean = Convert.ToDouble(str.Substring(str.IndexOf(":") + 1));
            str = sr.ReadLine();
            theoreticSTD = Convert.ToDouble(str.Substring(str.IndexOf(":") + 1));
        }
    }
}
