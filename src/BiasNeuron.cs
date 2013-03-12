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

        public void SetAnswer(double desired)
        {
            throw new OperationNotPossibleForBiasException();
        }

        public double GetDelta()
        {
            //Bias activation == 1
            //  =>  a(1-a) == 0 
            return 0;
        }

        public void PropagateBackwards()
        {
            //do nothing
        }

        public void AddWeightOnDelta(double weight_x_delta)
        {
            //do nothing
        }
    }

    internal class OperationNotPossibleForBiasException : Exception
    {
    }

    internal class CannotConnectToBiasException : Exception
    {
    }

}
