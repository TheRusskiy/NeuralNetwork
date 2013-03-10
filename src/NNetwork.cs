using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.src
{
    class NNetwork
    {
        private INeuron[][] neurons;
        public NNetwork(int[] neurons_in_layers_without_bias)
        {
            CheckDimensions(neurons_in_layers_without_bias);
            ConstructNetwork(neurons_in_layers_without_bias);
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
                        nn = new Neuron();
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
                    neurons[layer][i] = new Neuron();
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
    }

    internal class InvalidDimensionException : Exception
    {
    }
}
