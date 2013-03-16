using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.src
{
    class NetworkTrainer
    {
        private NNetwork network;
        public NetworkTrainer(NNetwork network)
        {
            this.network = network;
        }

        public void TrainPrediction(double[] train_set)
        {
            throw new NotImplementedException();
        }
    }
}
