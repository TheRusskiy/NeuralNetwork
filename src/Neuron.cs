using System;
using System.Collections.Generic;
using NeuralNetwork.src;

namespace NeuralNetwork
{
    class Neuron:INeuron
    {
        private List<INeuron> neurons;
        private List<double> weights;
        private double desired_answer;
        private bool cache_enabled;
        private bool cache_is_outdated = true;
        private double cached_activation;
        private bool is_last_layer;

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

        public bool IsCachingActivationResults
        {
            get { return cache_enabled; }
            set { cache_enabled = value; }
        }

        public Neuron(bool to_cache_activation_results=false)
        {
            cache_enabled = to_cache_activation_results;
            cache_is_outdated = true;
            neurons = new List<INeuron>();
            weights = new List<double>();
//            BiasNeuron bias = new BiasNeuron();
//            this.Connect(bias);
        }

        public void Connect(INeuron neuron, double weight=0)
        {
            SelfCheck(neuron);
            AlreadyConnectedCheck(neuron);
            ExtraBiasCheck(neuron);
            neurons.Add(neuron);
            weights.Add(weight);
        }


        public double Activation()
        {
            double result;
            if (!HasBias()) throw new BiasNotSetException();
            MakeSureHasNeuronsConnected();
            if (cache_enabled && !cache_is_outdated)
            {
                result = cached_activation;
            }
            else
            {
                result = Function();
                cache_is_outdated = false;
                cached_activation = result;
            }
            return result;
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

        private void SelfCheck(INeuron neuron)
        {
            if (this == neuron)
            {
                throw new CannotConnectToSelfException();
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

        public void SetAnswer(double desired_answer)
        {
            this.desired_answer = desired_answer;
            is_last_layer = true;
        }

        public double GetDelta()
        {
            double result=0;
            if (is_last_layer)
            {
                result = Activation() - desired_answer;
            }
            else
            {
                throw new NotImplementedException();
            }
            return result;
        }

        public void InvalidateActivationCache()
        {
            cache_is_outdated = true;
        }
    }

    internal class CannotConnectToSelfException : Exception
    {
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