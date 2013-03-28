using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.src
{
    class NNetwork
    {
        private bool is_sigmoid;
        public static NNetwork SigmoidNetwork(int[] neurons_in_layers_without_bias)
        {
            return new NNetwork(neurons_in_layers_without_bias, "sigmoid");
        }
        public static NNetwork HyperbolicNetwork(int[] neurons_in_layers_without_bias)
        {
            return new NNetwork(neurons_in_layers_without_bias, "hyperbolic");
        }

        private INeuron[][] neurons;
        private bool cache_enabled;
        //TODO: extract classes(e.g. matrix manipulator)

        private NNetwork(int[] neurons_in_layers_without_bias, string type)
        {
            is_sigmoid = type.Equals("sigmoid");
            CheckDimensions(neurons_in_layers_without_bias);
            ConstructNetwork(neurons_in_layers_without_bias);
            SetNeuronsCaching(true);
        }

        public int InputCount()
        {
            return this.Neurons[0].Length - 1; //without bias
        }

        public int OutputCount()
        {
            return this.Neurons[this.Neurons.Length - 1].Length - 1; //without bias
        }

        private void CheckDimensions(int[] neuronsInLayersWithoutBias)
        {
            if (neuronsInLayersWithoutBias.Length <= 0)
            {
                throw new InvalidDimensionException();
            }
            for (int i = 0; i < neuronsInLayersWithoutBias.Length; i++)
            {
                if (neuronsInLayersWithoutBias[i] <= 0)
                {
                    throw new InvalidDimensionException();
                }
            }
        }

        public INeuron[][] Neurons
        {
            get { return neurons; }
        }

        private void ConstructNetwork(int[] neurons_in_layers_without_bias)
        {
            int layer_count = neurons_in_layers_without_bias.Length;
            neurons = new INeuron[layer_count][];
            for (int i = 0; i < neurons.Length; i++)
            {
                neurons[i]= new INeuron[neurons_in_layers_without_bias[i]+1];
            }
            for (int layer = layer_count-1; layer >= 0; layer--)
            {
                if (layer == layer_count-1)
                {
                    ConstructOutputLayer(neurons_in_layers_without_bias);
                }
                else
                {
                    ConstructLayer(neurons_in_layers_without_bias, layer);
                }
            }
        }

        private void ConstructLayer(int[] neurons_in_layers_without_bias, int layer)
        {
            for (int i = 0; i < neurons_in_layers_without_bias[layer] + 1; i++)
            {
                INeuron nn;
                if (i == 0)
                {
                    nn = new BiasNeuron();
                }
                else
                {
                    if (layer == 0)
                    {
                        nn = new InputNeuron();
                    }
                    else
                    {
                        nn = CreateNeuron();
                    }
                }
                neurons[layer][i] = nn;
                ConnectNeuronToLayer(nn, layer);
            }
        }

        private void ConnectNeuronToLayer(INeuron nn, int layer)
        {
            for (int j = 1; j < neurons[layer + 1].Length; j++)
            {
                neurons[layer + 1][j].Connect(nn);
            }
        }

        private void ConstructOutputLayer(int[] neurons_in_layers_without_bias)
        {
            int layer = neurons_in_layers_without_bias.Length - 1;
            neurons[layer][0] = new BiasNeuron();
            for (int i = 1; i < neurons_in_layers_without_bias[layer] + 1; i++)
            {
                if (layer == 0)
                {
                    neurons[layer][i] = new InputNeuron();
                }
                else
                {
                    neurons[layer][i] = CreateNeuron();
                }
            }
        }

        public int LayerCount
        {
            get { return neurons.Length; }
        }

        public int[] NeuronsInLayersWithoutBias
        {
            get
            {
                int[] neurons_in_layers_without_bias = new int[neurons.Length];
                for (int i = 0; i < neurons.Length; i++)
                {
                    neurons_in_layers_without_bias[i]=neurons[i].Length-1;
                }
                return neurons_in_layers_without_bias;
            }
        }

        public bool CacheEnabled
        {
            get { return cache_enabled; }
            set
            {
                cache_enabled = value;
                SetNeuronsCaching(cache_enabled);
            }
        }


        public void SetWeightMatrix(double[][][] weights)
        {
            double[][] new_weights = new double[weights.Length][];
            for (int layer = 0; layer < weights.Length; layer++)
            {
                List<double> connections = new List<double>();
                for (int neuron = 0; neuron < weights[layer].Length; neuron++)
                {
                    connections.AddRange(weights[layer][neuron]);
                }
                new_weights[layer] = connections.ToArray();
            }
            SetWeightMatrix(new_weights);
        }

        public void SetWeightMatrix(double[][] weights)
        {
            for (int layer = 1; layer < LayerCount; layer++)
            {
                int curr_index = 0;
                for (int neuron = 1; neuron < neurons[layer].Length; neuron++)
                {
                    int neuron_length = ((Neuron) neurons[layer][neuron]).Length;
                    for (int connection = 0; connection < neuron_length; connection++)
                    {
                        ((Neuron)neurons[layer][neuron]).SetWeight(connection, weights[layer-1][curr_index]);
                        curr_index++;
                    }
                }
            }
        }

        public double[][] GetWeightMatrix()
        {
            double[][] weights=new double[neurons.Length-1][];
            for (int layer = 1; layer < LayerCount; layer++)
            {
                int weight_in_layer = 0;
                weights[layer-1]=new double[CountConnectionsToLayer(layer)];
                for (int neuron = 1; neuron < neurons[layer].Length; neuron++)
                {
                    int neuron_connections = ((Neuron) neurons[layer][neuron]).Length;
                    for (int connection = 0; connection < neuron_connections; connection++)
                    {
                        weights[layer-1][weight_in_layer] = ((Neuron) neurons[layer][neuron]).Weights[connection];
                        weight_in_layer++;
                    }
                }
            }
            return weights;
        }

        private int CountConnectionsToLayer(int layer)
        {
            int connection_count=0;
            for (int neuron = 1; neuron < neurons[layer].Length; neuron++)
            {
                int neuron_length = ((Neuron) neurons[layer][neuron]).Length;
                connection_count += neuron_length;
            }
            return connection_count;
        }

        public void SetInput(double[] input)
        {
            InvalidateNeuronsCache();
            if (input.Length != neurons[0].Length-1)
            {
                throw new IndexOutOfRangeException(
                    "passed with length: " + input.Length + ", real (without bias): " + (neurons[0].Length-1));
            }
            for (int i = 0; i < input.Length; i++)
            {
                ((InputNeuron) neurons[0][i + 1]).Input = input[i];
            }
            
        }

        public double[] GetOutput()
        {
            int output_count = neurons[neurons.Length - 1].Length-1;
            double[] output = new double[output_count];
            for (int i = 0; i < output_count; i++)
            {
                output[i] = neurons[neurons.Length - 1][i + 1].Activation();
            }
            return output;
        }

        private void SetNeuronsCaching(bool cache_enabled)
        {
            for (int i = 0; i < neurons.Length; i++)
            {
                for (int j = 0; j < neurons[i].Length; j++ )
                {
                    neurons[i][j].IsCachingActivationResults = cache_enabled;
                }
            }
        }

        private void InvalidateNeuronsCache()
        {
            for (int i = 0; i < neurons.Length; i++)
            {
                for (int j = 0; j < neurons[i].Length; j++)
                {
                    neurons[i][j].InvalidateActivationCache();
                }
            }
        }

        public void RandomizeWeights(int seed=-1, int multiplier = 10)
        {
            Random random;
            if (seed >= 0)
            {
                random = new Random(seed);
            }
            else
            {
                random = new Random();
            }
            double[][] old_matrix = GetWeightMatrix();
            for (int i = 0; i < old_matrix.Length; i++)
            {
                for (int j = 0; j < old_matrix[i].Length; j++)
                {
                    old_matrix[i][j] = RandomWeight(random, multiplier);
                }
            }
            SetWeightMatrix(old_matrix);
        }

        private double RandomWeight(Random random, int multiplier)
        {
            double result = random.NextDouble();
            result -= 0.5;
            result *= multiplier;
//            result = result < 0 ? -multiplier : multiplier;
            return result;
        }

        private Neuron CreateNeuron()
        {
            if (is_sigmoid)
            {
                return new Neuron();
            }
            else
            {
                return new TanhNeuron();
            }
        }

        public double[] GetDeltasForLayer(int layer_number)
        {
            if (layer_number <= 1)
            {
                throw new CannotGetDeltasForThisLayer();
            }
            INeuron[] layer = neurons[layer_number-1];
            double[] deltas = new double[layer.Length];
            for (int i = 1; i < layer.Length; i++)
            {
                deltas[i-1]=layer[i].GetDelta();
            }
            return deltas;
        }

        public void SetAnswers(double[] answers)
        {
            INeuron[] last_layer = neurons[LayerCount - 1];
            for (int i = 1; i < last_layer.Length; i++)
            {
                last_layer[i].SetAnswer(answers[i-1]);
            }
        }

        public void BackPropagate()
        {
            for (int layer = LayerCount-1; layer >= 1; layer--)
            {
                for (int neuron = 0; neuron < neurons[layer].Length; neuron++)
                {
                    neurons[layer][neuron].PropagateBackwards();
                }
            }
        }

        /// <summary>
        /// ! Invalidates Cache !
        /// </summary>
        public void ApplyTraining(double lambda=0, double alpha=1)
        {
            InvalidateNeuronsCache();
            for (int layer = 1; layer < LayerCount; layer++)
            {
                for (int neuron = 0; neuron < neurons[layer].Length; neuron++)
                {
                    neurons[layer][neuron].ApplyTraining(lambda, alpha);
                }
            }
        }

        public double CostFunction()
        {
            INeuron[] last_layer = neurons[LayerCount - 1];
            double acc = 0;
            for (int i = 1; i < last_layer.Length; i++)
            {
                Neuron n = ((Neuron)last_layer[i]);
                acc += n.LastLayerCost();

            }
            return acc;
        }

        public double[] Estimation(double epsilon = 0.001)
        {
            List<double> estimations = new List<double>();
            double[][] wm = GetWeightMatrix();
            for (int i = 0; i < wm.Length; i++)
            {
                for (int j = 0; j < wm[i].Length; j++)
                {
                    wm[i][j] += epsilon;
                    this.SetWeightMatrix(wm);
                    InvalidateNeuronsCache();
                    double left = CostFunction();
                    wm[i][j] -= 2*epsilon;
                    this.SetWeightMatrix(wm);
                    InvalidateNeuronsCache();
                    double right = CostFunction();
                    wm[i][j] += epsilon;
                    this.SetWeightMatrix(wm);
                    estimations.Add((left-right)/(2*epsilon));
                    InvalidateNeuronsCache();
                }
            }
            return estimations.ToArray();
        }
        public double[] Derivatives()
        {
            List<double> ders = new List<double>();
            for (int layer = 1; layer < LayerCount; layer++)
            {
                for (int neuron = 1; neuron < neurons[layer].Length; neuron++)
                {
                    Neuron n = (Neuron) neurons[layer][neuron];
                    ders.AddRange(n.Derivatives());
                }
            }
            return ders.ToArray();
        }
    }

    internal class CannotGetDeltasForThisLayer : Exception
    {
    }

    internal class InvalidDimensionException : Exception
    {
    }
}
