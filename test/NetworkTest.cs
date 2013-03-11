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
            BiasNeuron bias_2 = new BiasNeuron();
            a3_1.Connect(bias_2);
            Neuron a2_1 = new Neuron();
            a3_1.Connect(a2_1);
            Neuron a2_2 = new Neuron();
            a3_1.Connect(a2_2);
            BiasNeuron bias_1 = new BiasNeuron();
            a2_1.Connect(bias_1);
            a2_2.Connect(bias_1);
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
            NNetwork network = new NNetwork(new int[]{1,1,1});
        }

        [Test]
        public void TestDimensions()
        {
            int[] neurons_in_layers = new int[]{3, 4, 2, 1}; 
            NNetwork network = new NNetwork(neurons_in_layers);
            Assert.AreEqual(network.LayerCount, neurons_in_layers.Length);
            Assert.AreEqual(network.NeuronsInLayersWithoutBias, neurons_in_layers);
        }

        [Test]
        public void TestWeightMatrix()
        {
            double[] from_l1 = new double[] {-30, 20, 20, 10, -20, -20};
            double[] from_l2 = new double[] { -10, 20, 20 };
            double[][] weights = new double[][]
                {
                    from_l1,
                    from_l2
                };
            NNetwork n = new NNetwork(new int[] { 2, 2, 1 });
            n.SetWeightMatrix(weights);
            Assert.AreEqual(n.GetWeightMatrix(), weights);
        }

        [Test]
        public void TestSimplestConnection()
        {
            NNetwork n = new NNetwork(new int[] {1, 1});
            n.SetWeightMatrix(new double[][]
                {
                    new double[]{1, 1},
                    new double[]{1, 1} 
                });
            n.SetInput(new double[]{1});
            var output = n.GetOutput()[0];
            var desired = 1/(1 + Math.Pow(Math.E, -2));
            AssertCloseTo(output, desired);
        }

        [Test]
        public void TestInputEqualsOutput()
        {
            NNetwork n = new NNetwork(new int[] { 1 });
            n.SetWeightMatrix(new double[][]
                {
                    new double[]{1, 1}
                });
            n.SetInput(new double[]{9});
            Assert.AreEqual(n.GetOutput()[0], 9);

        }

        [Test]
        public void TestXNORAuto()
        {
            double[] from_l1 = new double[] { -30, 20, 20, 10, -20, -20 };
            double[] from_l2 = new double[] { -10, 20, 20 };
            double[][] weights = new double[][]
                {
                    from_l1,
                    from_l2
                };
            NNetwork n = new NNetwork(new int[] { 2, 2, 1 });
            n.SetWeightMatrix(weights);

            double[] input = new double[]{0, 0};
            n.SetInput(input);
            double[] output = n.GetOutput();
            AssertCloseTo(output[0], 1);

            input = new double[] { 0, 1 };
            n.SetInput(input);
            output = n.GetOutput();
            AssertCloseTo(output[0], 0);

            input = new double[] { 1, 0 };
            n.SetInput(input);
            output = n.GetOutput();
            AssertCloseTo(output[0], 0);

            input = new double[] { 1, 1 };
            n.SetInput(input);
            output = n.GetOutput();
            AssertCloseTo(output[0], 1);
        }

        [Test]
        public void TestAlternativeMatrix()
        {
            double[][] from_l1 = new double[][]
                {
                    new double[]{-30, 20, 20},
                    new double[]{10, -20, -20}
                };
            double[][] from_l2 = new double[][]
                {
                    new double[]{-10, 20, 20}
                };
            double[][][] weights = new double[][][]
                {
                    from_l1,
                    from_l2
                };
            NNetwork n = new NNetwork(new int[] { 2, 2, 1 });
            n.SetWeightMatrix(weights);

            double[] from_l1_expected = new double[] { -30, 20, 20, 10, -20, -20 };
            double[] from_l2_expected = new double[] { -10, 20, 20 };
            double[][] weights_expected = new double[][]
                {
                    from_l1_expected,
                    from_l2_expected
                };
            Assert.AreEqual(weights_expected, n.GetWeightMatrix());
        }

        [Test]
        public void IllegalDimensionsTest()
        {
            Assert.Throws(typeof(InvalidDimensionException), () => new NNetwork(new int[] {}));
            Assert.Throws(typeof(InvalidDimensionException), () => new NNetwork(new int[] { 1, 0}));
        }

        [Test]
        [Ignore]
        public void ActivateCachingAcrossAllNetwork()
        {
            //or should cache by default?
        }
        
        [Test]
        [Ignore]
        public void InvalidateCacheAcrossAllNetwork()
        {
            //necessary to test? 
        }

        [Test]
        [Ignore]
        public void IfInputNeuronsWereChangedThanBeforeCalculatingOutputInvalidateCache()
        {

        }

        public static void AssertCloseTo(double arg1, double arg2, double by = 0.0001)
        {
            Assert.Less(Math.Abs(arg1 - arg2), by);
        }

    }
}
