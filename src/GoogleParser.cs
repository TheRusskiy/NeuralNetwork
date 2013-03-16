using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.src
{
    class GoogleParser
    {
        public double[] ParseFile(string path)
        {
            //First line of file includes junk!
            String[] string_entries = System.IO.File.ReadAllLines(path);
            double[] entries = new double[string_entries.Length-1];
            for (int i = 0; i < entries.Length; i++)
            {
                entries[i] = ParseLine(string_entries[i+1]);
            }
            return entries;
        }

        public double ParseLine(string line)
        {
            line = DeleteBeforeSymbol(line, ",");
            line = DeleteBeforeSymbol(line, ",");
            line = DeleteBeforeSymbol(line, ",");
            line = DeleteBeforeSymbol(line, ",");
            line = DeleteAfterSymbol(line, ",");
            double result;
            if (!Double.TryParse(line, out result))
            {
                line = line.Replace('.', ',');
                result = Double.Parse(line);
            }
            return result;
        }

        private static string DeleteAfterSymbol(string from, string symbol)
        {
            int index = from.IndexOf(symbol);
//            int length = from.Length - index;
            return from.Substring(0, index);
        }

        private static String DeleteBeforeSymbol(string from, string symbol)
        {
            int index = from.IndexOf(symbol) + symbol.Length;
            int length = from.Length - index;
            return from.Substring(index, length);
        }

        public double[] GetDeltas(double[] absolutes)
        {
            double[] deltas = new double[absolutes.Length-1];
            for (int i = 0; i < absolutes.Length-1; i++)
            {
                deltas[i] = absolutes[i + 1] - absolutes[i];
            }
            return deltas;
        }
    }
}
