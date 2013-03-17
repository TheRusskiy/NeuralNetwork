using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.src
{
    class DataNormalizer
    {
        public static DataNormalizer SigmoidNormalizer(double[] initial_values)
        {
            return new DataNormalizer(initial_values);
        }

        public static DataNormalizer HyperbolicNormalizer(double[] initial_values)
        {
            return new HyperbolicNormalizer(initial_values);
        }

        public const double SAFE_KOEFF = 1.1;
        protected double min;
        protected double max;
        private double[] initial_values;
        private double[] values;

        protected DataNormalizer(double[] initial_values)
        {
            if (initial_values==null || initial_values.Length == 0) throw new EmptyDataSetException();
            this.initial_values = initial_values;
            FindMaxAndMin();
            CreateNormalizedValues();
        }

        private void CreateNormalizedValues()
        {
            values = new double[initial_values.Length];
            for (int i = 0; i < initial_values.Length; i++)
            {
                values[i] = NormalizeValue(initial_values[i]);
            }
        }

        protected virtual double NormalizeValue(double value)
        {
            double result = (value - min)/(max - min);
            return result;
        }

        public double GetMin()
        {
            return min;
        }

        public double GetMax()
        {
            return max;
        }

        private void FindMaxAndMin()
        {
            min = initial_values[0];
            max = initial_values[0];
            foreach (double value in initial_values)
            {
                if (value > max)
                {
                    max = value;
                }
                if (value < min)
                {
                    min = value;
                }
            }
            min *= SAFE_KOEFF;
            max *= SAFE_KOEFF;
        }

        public double[] GetValues()
        {
            return values;
        }

        public virtual double Denormalize(double value)
        {
            if (value > 1 || value < 0) throw new ValueOutOfBoundsException(); 
            double result = min + value*(max - min);
            return result;
        }
    }
    internal class HyperbolicNormalizer : DataNormalizer
    {
        protected internal HyperbolicNormalizer(double[] initial_values) : base(initial_values){}

        protected override double NormalizeValue(double value)
        {
            double result = 2 * (value - min) / (max - min) - 1;
            return result;
        }

        public override double Denormalize(double value)
        {
            if (value > 1 || value < -1) throw new ValueOutOfBoundsException();
            double result = (value+1)*(max-min)/2+min;
            return result;
        }
    }

    internal class ValueOutOfBoundsException : Exception{}
    internal class EmptyDataSetException : Exception{}
}
