using System;
using System.Collections.Generic;
using NeuralNetwork.src;

namespace NeuralNetwork
{
    class Neuron:INeuron
    {
        private List<INeuron> neurons;
        private List<double> weights;
        private double delta = 0;
        private double desired_answer;
        private bool cache_enabled;
        private bool cache_is_outdated = true;
        private double cached_activation;
        private bool propagate_like_last_layer;
        private double weight_x_delta_acc;
        private bool acc_empty = true;

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
            propagate_like_last_layer = true;
            acc_empty = false;
        }

        public double GetDelta()
        {
            if (!acc_empty)
            {
                throw new CannotAccessDeltaBeforeBackpropHasBeenDone();
            }
            return delta;
        }

        private double CalculateLastLayerDelta()
        {
            return Activation() - desired_answer;
        }

        private double CalculateMidLayerDelta()
        {
            double a = Activation();
            return weight_x_delta_acc*a*(1 - a);
        }

        public void InvalidateActivationCache()
        {
            cache_is_outdated = true;
        }

        /// <summary>
        /// Computes and stores delta.
        /// Add weights multiplied on self-delta to appropriate
        /// connected neurons.
        /// Clears self weight_x_delta accumulator.
        /// Throws exception if self accumulator was empty =>
        /// => can't be called twice without getting new deltas!
        /// </summary>
        public void PropagateBackwards()
        {
            delta += CalculateDelta();
            if (acc_empty)
            {
                throw new CannotPropagateWithEmptyAcc();
            }
            acc_empty = true;
            for (int i = 0; i < neurons.Count; i++ )
            {
                neurons[i].AddWeightOnDelta(weights[i]*GetDelta());
            }
            weight_x_delta_acc = 0;
            
        }

        private double CalculateDelta()
        {
            double result = 0;
            if (propagate_like_last_layer)
            {
                result = CalculateLastLayerDelta();
            }
            else
            {
                result = CalculateMidLayerDelta();
            }
            return result;
        }

        public void AddWeightOnDelta(double weight_x_delta)
        {
            weight_x_delta_acc += weight_x_delta;
            acc_empty = false;
        }
    }

    internal class CannotAccessDeltaBeforeBackpropHasBeenDone : Exception
    {
    }

    internal class CannotPropagateWithEmptyAcc : Exception
    {
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