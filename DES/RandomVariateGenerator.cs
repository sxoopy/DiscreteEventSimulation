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
    [Editor(typeof(RandomRVGEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RandomVariateGenerator
    {
        protected int seed;
        protected Random rnd;
        protected double theoreticMean;

        public double TheoreticMean { get => theoreticMean; }

        /// <summary>
        /// 設定亂數種子
        /// </summary>
        /// <param name="seed">客製化的種子值</param>
        /// <returns></returns>       
        public void SetSeed(int seed)
        {
            this.seed = seed;
            rnd = new Random(seed);
        }

        public virtual double NextDouble()
        {
            throw new Exception("No Implementation");
        }

        public virtual int NextInt()
        {
            throw new Exception("No Implementation");
        }

        public virtual void SaveToFileStream(StreamWriter sw)
        {
            
        }

        public virtual void ReadFromFileStream(StreamReader sr)
        {
            
        }

        public static RandomVariateGenerator CreateRVGbyType(string className)
        {
            switch (className)
            {
                case "ExponentialRVG":
                    return new ExponentialRVG(1.0);
                case "UniformRVG":
                    return new UniformRVG(1.0, 3.0);
            }
            return null;
        }
    }
}
