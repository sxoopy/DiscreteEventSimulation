using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    public class UniformRVG : RandomVariateGenerator
    {

        double lowerBound;
        double upperBound;

        public double LowerBound
        {
            get => lowerBound;
            set
            {
                if (value > upperBound) return;
                lowerBound = value;
                theoreticMean = (lowerBound + upperBound) / 2;
            }
        }
        public double UpperBound
        {
            get => upperBound;
            set
            {
                if (value < lowerBound) return;
                upperBound = value;
                theoreticMean = (lowerBound + upperBound) / 2;
            }
        }

        public UniformRVG(int seed, double lb, double ub)
        {
            lowerBound = lb;
            upperBound = ub;
            SetSeed(seed);
        }

        public UniformRVG(double lb, double ub)
        {
            lowerBound = lb;
            upperBound = ub;
            theoreticMean = (lowerBound + upperBound) / 2;
            rnd = new Random();
        }
        public override double NextDouble()
        {
            return lowerBound + (upperBound - lowerBound) * rnd.NextDouble();
        }

        public override string ToString()
        {
            return $"Uniform(lb = {lowerBound}, ub = {upperBound})";
        }

        public override void SaveToFileStream(StreamWriter sw)
        {
            sw.WriteLine($"LowerBound: {lowerBound}");
            sw.WriteLine($"UpperBound: {upperBound}");
        }

        public override void ReadFromFileStream(StreamReader sr)
        {
            string str = sr.ReadLine();
            lowerBound = Convert.ToDouble(str.Substring(str.IndexOf(":") + 1));
            str = sr.ReadLine();
            upperBound = Convert.ToDouble(str.Substring(str.IndexOf(":") + 1));
        }
    }
}
