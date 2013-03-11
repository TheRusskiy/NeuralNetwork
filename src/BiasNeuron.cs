using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.src
{
    class BiasNeuron:INeuron
    {
        public void Connect(INeuron neuron, double weight)
        {
            throw new CannotConnectToBiasException();
        }

        public double Activation()
        {
            return 1.0;
        }

        public bool IsCachingActivationResults
        {
            get { throw new CachingIsNotSupportedException(); }
            set { }
        }
        public void InvalidateActivationCache()
        {
            //no cache here
        }
    }

    internal class CannotConnectToBiasException : Exception
    {
    }
}
