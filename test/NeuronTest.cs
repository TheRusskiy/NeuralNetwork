using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NeuralNetwork.src;

namespace NeuralNetwork.test
{   
    class NeuronTest
    {
        private Neuron neuron;
        private InputNeuron input;
        [SetUp]
        public void SetUp()
        {
            neuron =  new Neuron();
            input = new InputNeuron();
        }
        [Test]
        public void ActivateInput()
        {
            Assert.Throws(typeof (NotConfiguredException),
                () => input.Activation());
            input.Input = 1;
            Assert.AreEqual(input.Activation(), 1);
        }
        [Test]
        public void ActivateNeuron()
        {
            BiasNeuron bias = new BiasNeuron();
            double w0 = 0;
            neuron.Connect(bias, w0);

            Assert.Throws(typeof(NotConfiguredException),
                          () => neuron.Activation());
            InputNeuron i1 = new InputNeuron();
            InputNeuron i2 = new InputNeuron();
            InputNeuron i3 = new InputNeuron();
            
            i1.Input = 1;
            i2.Input = 1;
            i3.Input = 1;

            double w1 = 1;
            double w2 = 1;
            double w3 = 1;

            neuron.Connect(i1, w1);
            neuron.Connect(i2, w2);
            neuron.Connect(i3, w3);
            double tx = i1.Input*w1+i2.Input*w2+i3.Input*w3;
            double expected_activation = 1/(1 + Math.Pow(Math.E, -tx));
            MyAssert.CloseTo(neuron.Activation(), expected_activation);
        }
        [Test]
        public void ConnectToNeuron()
        {
            InputNeuron input1 = new InputNeuron();
            InputNeuron input2 = new InputNeuron();
            InputNeuron input3 = new InputNeuron();
            neuron.Connect(input1, 0);
            neuron.Connect(input2, 0);
            neuron.Connect(input3, 0);
            INeuron[] neurons = neuron.Neurons;
            Assert.Contains(input1, neurons);
            Assert.Contains(input2, neurons);
        }
        [Test]
        public void WeightsChange()
        {
            neuron.Connect(new InputNeuron(), 0);
            Assert.AreEqual(neuron.Weights[0], 0);
            neuron.SetWeight(0, 2.0);
            Assert.AreEqual(neuron.Weights[0], 2);
        }

        [Test]
        public void TestLength()
        {
            Assert.AreEqual(neuron.Length, 0);
            neuron.Connect(new Neuron());
            Assert.AreEqual(neuron.Length, 1);
        }

        [Test]
        public void TestOnlySingleBias()
        {
            neuron.Connect(new BiasNeuron());
            Assert.Throws(typeof(MoreThanOneBiasException),
                          () => neuron.Connect(new BiasNeuron()));
        }

        [Test]
        public void TestSelfConnect()
        {
            neuron.Connect(new BiasNeuron());
            Assert.Throws(typeof(CannotConnectToSelfException),
                          () => neuron.Connect(neuron));
        }

        [Test]
        public void TestInputNeuron()
        {
            input.Input=1;
            Assert.AreEqual(input.Activation(), 1);
        }

        [Test]
        public void DuplicateTest()
        {
            Neuron n2 = new Neuron();
            neuron.Connect(n2);
            Assert.Throws(typeof (AlreadyConnectedException), () => neuron.Connect(n2));
        }

        [Test]
        public void NeuronCanHaveAutoForwardPropagationDisabled()
        {
            neuron.Connect(new BiasNeuron());
            InputNeuron input = new InputNeuron();
            neuron.Connect(input, 1);
            input.Input = 1;
            var first_time = neuron.Activation();
            neuron.IsCachingActivationResults = true;
            input.Input = 2;
            var with_cache = neuron.Activation();
            Assert.AreEqual(first_time, with_cache);
            neuron.InvalidateActivationCache();
            var without_cache = neuron.Activation();
            Assert.AreNotEqual(first_time, without_cache);
        }

        [Test]
        public void CachingCanBeConfiguredOnConstruction()
        {
            neuron=new Neuron();
            Assert.AreEqual(neuron.IsCachingActivationResults, false);
            neuron = new Neuron(true);
            Assert.AreEqual(neuron.IsCachingActivationResults, true);
        }

        [Test]
        public void TanhActivation()
        {
            TanhNeuron tn = new TanhNeuron();
            BiasNeuron bias = new BiasNeuron();
            double w0 = 0;
            tn.Connect(bias, w0);

            Assert.Throws(typeof(NotConfiguredException),
                          () => tn.Activation());
            InputNeuron i1 = new InputNeuron();
            InputNeuron i2 = new InputNeuron();
            InputNeuron i3 = new InputNeuron();

            i1.Input = 1;
            i2.Input = 1;
            i3.Input = 1;

            double w1 = 1;
            double w2 = 1;
            double w3 = 1;

            tn.Connect(i1, w1);
            tn.Connect(i2, w2);
            tn.Connect(i3, w3);
            double z = i1.Input * w1 + i2.Input * w2 + i3.Input * w3;
            double expected_activation = (Math.Exp(z) - Math.Exp(-z)) / (Math.Exp(z) + Math.Exp(-z));
            MyAssert.CloseTo(tn.Activation(), expected_activation);
        }

        [Test]
        public void TestTanhLearningOnSinus()
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
                trainer.TrainCalculation(inputs, outputs);
                double new_cost = trainer.GetError();
                delta = error - new_cost;
                error = new_cost;
            }
            double[][] input_test = SinusTrainSet(20)[0];
            double[][] output_test = SinusTrainSet(20)[1];
            trainer.IsLearning = false;
            trainer.TrainCalculation(input_test, output_test);
            error = trainer.GetError();
            Assert.Less(error, 0.53);
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
