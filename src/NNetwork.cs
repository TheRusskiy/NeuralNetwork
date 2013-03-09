using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.src
{
    class NNetwork
    {
        private int layer_count;
        private int[] neurons_in_layers;
        public NNetwork(int layer_count, int[] neurons_in_layers)
        {
            this.layer_count = layer_count;
            this.neurons_in_layers = neurons_in_layers;
        }

        public int LayerCount
        {
            get { return layer_count; }
        }

        public int[] NeuronsInLayers
        {
            get { return neurons_in_layers; }
        }
    }
}
