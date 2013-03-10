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
            AssertCloseTo(neuron.Activation(), expected_activation);
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
        public static void AssertCloseTo(double arg1, double arg2, double by=0.0001)
        {
            Assert.Less(Math.Abs(arg1 - arg2), by);
        }
    }
    
}
