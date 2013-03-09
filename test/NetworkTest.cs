using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NeuralNetwork.src;

namespace NeuralNetwork.test
{
    class NetworkTest
    {
        [Test]
        public void TestXNOR_Manualy()
        {
            Neuron a3_1 = new Neuron();
            Neuron a2_1 = new Neuron();
            a3_1.Connect(a2_1);
            Neuron a2_2 = new Neuron();
            a3_1.Connect(a2_2);
            InputNeuron a1_1 = new InputNeuron();
            a2_1.Connect(a1_1);
            a2_2.Connect(a1_1);
            InputNeuron a1_2 = new InputNeuron();
            a2_1.Connect(a1_2);
            a2_2.Connect(a1_2);

            a3_1.SetWeight(0, -10);
            a3_1.SetWeight(1, 20);
            a3_1.SetWeight(2, 20);

            a2_1.SetWeight(0, -30);
            a2_1.SetWeight(1, 20);
            a2_1.SetWeight(2, 20);

            a2_2.SetWeight(0, 10);
            a2_2.SetWeight(1, -20);
            a2_2.SetWeight(2, -20);

            a1_1.Input = 0;
            a1_2.Input = 0;
            AssertCloseTo(a3_1.Activation(), 1);

            a1_1.Input = 0;
            a1_2.Input = 1;
            AssertCloseTo(a3_1.Activation(), 0);

            a1_1.Input = 1;
            a1_2.Input = 0;
            AssertCloseTo(a3_1.Activation(), 0);

            a1_1.Input = 1;
            a1_2.Input = 1;
            AssertCloseTo(a3_1.Activation(), 1);
        }

        [Test]
        public void TestNetworkCreation()
        {
            NNetwork network = new NNetwork(1, new int[]{1,1,1});
        }

        [Test]
        public void TestDimensions()
        {
            int n_layers = 3;
            int[] neurons_in_layers = new int[]{3, 4, 2, 1}; 
            NNetwork network = new NNetwork(n_layers, neurons_in_layers);
            Assert.AreEqual(network.LayerCount, n_layers);
            Assert.AreEqual(network.NeuronsInLayers, neurons_in_layers);
        }

        [Test]
        [Ignore]
        public void TestGetWeightMatrix()
        {
            
        }

        [Test]
        [Ignore]
        public void TestSetWeightMatrix()
        {

        }


        public static void AssertCloseTo(double arg1, double arg2, double by = 0.0001)
        {
            Assert.Less(Math.Abs(arg1 - arg2), by);
        }

    }
}
