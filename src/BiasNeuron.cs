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
    }

    internal class CannotConnectToBiasException : Exception
    {
    }
}
