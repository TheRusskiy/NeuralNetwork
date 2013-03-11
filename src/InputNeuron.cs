using System;

namespace NeuralNetwork
{
    class InputNeuron:INeuron
    {
        private double input;
        private bool isSet = false;

        public double Input
        {
            get
            {
                if (!isSet)
                {
                    throw new NotConfiguredException();
                }
                return input;
            }
            set
            {
                isSet = true;
                input = value;
            }
        }

        public double Activation()
        {
            return Input;
        }

        public bool IsCachingActivationResults
        {
            get { throw new CachingIsNotSupportedException();}
            set { }
        }

        public void Connect(INeuron neuron, double weight)
        {
            throw new CannotConnectToInputNeuronException();
        }

        public void InvalidateActivationCache()
        {
            //no cache here
        }
    }

    internal class NotConfiguredException : Exception
    {
    }

    internal class CannotConnectToInputNeuronException : Exception
    {
    }
    
    internal class CachingIsNotSupportedException : Exception
    {
    }
}
