using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NeuralNetwork.src;

namespace NeuralNetwork.test
{
    class TestNetworkTrainer
    {
        [Test]
        public void CreateTrainer()
        {
            NNetwork network = new NNetwork(new int[]{1, 2, 1});
            NetworkTrainer trainer = new NetworkTrainer(network);
        }

        [Test]
        [Ignore]
        public void TrainPrediction()
        {
            NNetwork network = new NNetwork(new int[] { 5, 2, 2 });
            NetworkTrainer trainer = new NetworkTrainer(network);
            double[] train_set = new double[] {};
            trainer.TrainPrediction(train_set);
            //todo
        }

        private double[][] SinusTrainSet()
        {
            int n = 9;
            double[] inputs = new double[n];
            double[] outputs = new double[n];
            double pi = Math.PI;
            for (int i = 0; i < n; i++)
            {
                var value = -pi/2 + i*pi/8;
                inputs[i] = value;
                outputs[i] = Math.Sin(value);
            }
            return new double[][]{inputs, outputs};
        }
    }
}
