using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    public interface IDEScomponent
    {
        /// <summary>
        /// function declaration
        /// </summary>
        /// <returns></returns>
        string DisplaySimulationResults();
        void SaveToFileStream(StreamWriter sw);
        void ReadFromFileStream(StreamReader sr);
    }
}
