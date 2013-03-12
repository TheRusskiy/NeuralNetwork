using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NeuralNetwork.src;

namespace NeuralNetwork.test
{   
    class BackPropTest
    {
        [Test]
        public void TestCanSetAnswer()
        {
            Neuron n = new Neuron();
            n.Connect(new BiasNeuron(), 1);
            InputNeuron input = new InputNeuron();
            n.Connect(input, 1);
            n.SetWeight(0, 1);
            input.Input = 1;
            var desired = 1 / (1 + Math.Pow(Math.E, -2));
            n.SetAnswer(desired);
            MyAssert.CloseTo(n.GetDelta(), 0);
            /*
             1. If answer was set, than calculate like last layer, 
                otherwise require theta and delta.  
             2. In back prop every layer calculates it's values and
                sets theta+delta for every connected neuron.
             */
        }
        [Test]
        public void TestNetworkSetAnswerAndGetDelta()
        {
            NNetwork n = new NNetwork(new int[]{2, 3, 2});
            n.SetInput(new double[]{0, 0});
            double[] outputs = n.GetOutput();
            double[] answers = new double[]{0.1, 0.9};
            n.SetAnswers(answers);
            double[] deltas = n.GetDeltasForLayer(3);
            for (int i = 0; i < answers.Length; i++)
            {
                MyAssert.CloseTo(deltas[i], outputs[i]-answers[i]);
            }
        }

        [Test]
        public void TestRandomInit()
        {
            NNetwork n = new NNetwork(new int[] { 2, 3, 2 });
            n.RandomizeWeights(seed:0);
            var first = n.GetWeightMatrix();
            n.RandomizeWeights(seed: 0);
            var equal = n.GetWeightMatrix();
            Assert.AreEqual(first, equal);
            n.RandomizeWeights(seed: 1);
            var not_equal = n.GetWeightMatrix();
            Assert.AreNotEqual(first, not_equal);
        }

        [Test]
        public void CanNotGetDeltasForFirstLayer()
        {
            NNetwork n = new NNetwork(new int[] { 2, 3, 2 });
            Assert.Throws(typeof (CannotGetDeltasForThisLayer), () => n.GetDeltasForLayer(1));
        }

        [Test]
        public void TestBackPropWithKnownValues()
        {
            NNetwork n = NetworkTest.XorNetwork();
            n.SetInput(new double[]{1, 1});
            n.SetAnswers(new double[]{1});
            double[] deltas = n.GetDeltasForLayer(2);
            MyAssert.CloseTo(deltas[0], 0, 0.001);
            MyAssert.CloseTo(deltas[1], 0, 0.001);
        }

    }
    
}
