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
            NNetwork network = NNetwork.SigmoidNetwork(new int[]{1, 2, 1});
            NetworkTrainer trainer = new NetworkTrainer(network);
        }

        [Test]
        [Ignore]
        public void TrainPrediction()
        {
            NNetwork network = NNetwork.SigmoidNetwork(new int[] { 5, 2, 2 });
            NetworkTrainer trainer = new NetworkTrainer(network);
            double[] train_set = new double[] {};
            trainer.TrainPrediction(train_set);
            //todo
        }

        [Test]
        public void TestCostFunctionAccumulation()
        {
            NNetwork network = NNetwork.SigmoidNetwork(new int[] { 2, 4, 3 });
            NetworkTrainer trainer = new NetworkTrainer(network);
            double[] train_set = new[] { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1};
            Assert.Throws(typeof (NoErrorInfoYetException), () => trainer.GetError());
            double error;
            trainer.TrainPrediction(train_set);
            error = trainer.GetError();
            Assert.AreNotEqual(error, 0);
            trainer.TrainPrediction(train_set);
            Assert.AreNotEqual(error, trainer.GetError());
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
