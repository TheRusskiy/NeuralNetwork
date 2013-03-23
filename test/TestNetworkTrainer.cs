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
        public void DimensionTestCheck()
        {
            NNetwork network = NNetwork.SigmoidNetwork(new int[] { 2, 4, 3 });
            NetworkTrainer trainer = new NetworkTrainer(network);
            double[][] incorrect_input = new double[1][] { new double[3] };
            double[][] correct_input = new double[1][] { new double[2] };
            double[][] incorrect_output = new double[1][] { new double[4] };
            double[][] correct_output = new double[1][] { new double[3] };
            Assert.Throws(typeof(IncorrectInputDimensionException),
                          () => trainer.TrainClassification(incorrect_input, correct_output));
            Assert.Throws(typeof(IncorrectOutputDimensionException),
                          () => trainer.TrainClassification(correct_input, incorrect_output));
        }

        [Test]
        [Ignore]
        public void TrainPrediction()
        {
            NNetwork network = NNetwork.SigmoidNetwork(new int[] { 5, 2, 2 });
            NetworkTrainer trainer = new NetworkTrainer(network);
            double[] train_set = new double[] { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
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

        [Test]
        public void TestHyperbolicClassification()
        {
            NNetwork network = NNetwork.HyperbolicNetwork(new int[] { 1, 2, 1 });
            network.RandomizeWeights(1, 2);
            NetworkTrainer trainer = new NetworkTrainer(network);
            double[][] inputs = SinusTrainSet()[0];
            double[][] outputs = SinusTrainSet()[1];
            double error = 1;
            double delta = 1;
            int j = 0;
            for (; error > 0.01 && !(delta <= 0.000001) || j == 1; j++)
            {
                trainer.TrainClassification(inputs, outputs);
                double new_cost = trainer.GetError();
                delta = error - new_cost;
                error = new_cost;
            }
            double[][] input_test = SinusTrainSet(20)[0];
            double[][] output_test = SinusTrainSet(20)[1];
            trainer.IsLearning = false;
            trainer.TrainClassification(input_test, output_test);
            error = trainer.GetError();
            Assert.Less(error, 0.55);
        }

        private static double[][][] SinusTrainSet(int size = 9)
        {
            double[][] inputs = new double[size][];
            double[][] outputs = new double[size][];
            double pi = Math.PI;
            double step = pi / (double)size;
            for (int i = 0; i < size; i++)
            {
                var value = -pi / 2 + i * step;
                inputs[i] = new double[] { value };
                outputs[i] = new double[] { Math.Sin(value) };
            }
            return new double[][][] { inputs, outputs };
        }
    }
}
