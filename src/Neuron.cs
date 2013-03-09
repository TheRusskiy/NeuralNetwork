using System;
using System.Collections.Generic;
using NeuralNetwork.src;

namespace NeuralNetwork
{
    class Neuron:INeuron
    {
        private List<INeuron> neurons;
        private List<double> weights;

        public INeuron[] Neurons
        {
            get { return neurons.ToArray(); }
        }
        public double[] Weights
        {
            get { return weights.ToArray(); }
        }

        public int Length
        {
            get { return neurons.Count; }           
        }

        public Neuron()
        {
            neurons = new List<INeuron>();
            weights = new List<double>();
            BiasNeuron bias = new BiasNeuron();
            this.Connect(bias);
        }

        public void Connect(INeuron neuron, double weight=0)
        {
            AlreadyConnectedCheck(neuron);
            ExtraBiasCheck(neuron);
            neurons.Add(neuron);
            weights.Add(weight);
        }

        public double Activation()
        {
            if (!HasBias()) throw new BiasNotSetException();
            MakeSureHasNeuronsConnected();
            return Function();
        }

        public void SetWeight(int index, double value)
        {
            weights[index] = value;
        }

        private double Function()
        {
            double z = WeightsOnInputs();
            double value = 1/(1 + Math.Pow(Math.E, -z));
            return value;
        }

        private void ExtraBiasCheck(INeuron neuron)
        {
            if (IsBias(neuron)&&HasBias())
            {
                throw new MoreThanOneBiasException();
            }
        }

        private void AlreadyConnectedCheck(INeuron neuron)
        {
            if (neurons.Contains(neuron))
            {
                throw new AlreadyConnectedException();
            }
        }

        private bool IsBias(INeuron neuron)
        {
            return neuron.GetType() == typeof (BiasNeuron);
        }

        private void MakeSureHasNeuronsConnected()
        {
            if (neurons.Count < 2)
            {
                throw new NotConfiguredException();
            }
        }

        private bool HasBias()
        {
            bool has_bias = false;
            foreach (INeuron neuron in neurons)
            {
                if (IsBias(neuron))
                {
                    has_bias = true;
                }
            }
            return has_bias;
        }

        private double WeightsOnInputs()
        {
            double acc = 0;
            for (int i = 0; i < weights.Count; i++)
            {
                acc += neurons[i].Activation()*weights[i];
            }
            return acc;
        }
    }

    internal class BiasNotSetException : Exception
    {
    }
    internal class MoreThanOneBiasException : Exception
    {
    }
    internal class AlreadyConnectedException : Exception
    {
    }
}