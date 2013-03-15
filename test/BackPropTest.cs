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
            n.PropagateBackwards();
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
            n.BackPropagate();
            double[] deltas = n.GetDeltasForLayer(3);
            for (int i = 0; i < answers.Length; i++)
            {
                MyAssert.CloseTo(deltas[i], answers[i] - outputs[i]);
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
        public void LastLayerGivesDeltasAndWeightsToTheOneBefore()
        {
            Neuron n31 = new Neuron();
            Neuron n32 = new Neuron();

            BiasNeuron bias2 = new BiasNeuron(); ;
            Neuron n21 = new Neuron();
            Neuron n22 = new Neuron();
            n31.Connect(bias2);
            n31.Connect(n21);
            n31.Connect(n22);
            n32.Connect(bias2);
            n32.Connect(n21);
            n32.Connect(n22);

            InputNeuron input = new InputNeuron();
            BiasNeuron bias1 = new BiasNeuron();
            n21.Connect(bias1);
            n21.Connect(input);
            n22.Connect(bias1);
            n22.Connect(input);

            input.Input = 1;
            n31.SetAnswer(0.9);
            n32.SetAnswer(0.1);
            n31.PropagateBackwards();
            n32.PropagateBackwards();
            double delta31 = n31.GetDelta();
            double delta32 = n32.GetDelta();
            double n21_n31 = n31.Weights[1];
            double n21_n32 = n32.Weights[1];
            n21.PropagateBackwards();
            double desired_delta_for_n21 = n21_n31*delta31 + n21_n32*delta32;
            Assert.AreEqual(desired_delta_for_n21, n21.GetDelta());
        }

        [Test]
        public void ThrowsIfPropagateTwice()
        {
            Neuron n31 = new Neuron();

            BiasNeuron bias2 = new BiasNeuron(); ;
            Neuron n21 = new Neuron();
            n31.Connect(bias2);
            n31.Connect(n21);

            InputNeuron input = new InputNeuron();
            BiasNeuron bias1 = new BiasNeuron();
            n21.Connect(bias1);
            n21.Connect(input);

            input.Input = 1;
            n31.SetAnswer(1);
            n31.PropagateBackwards();
            Assert.Throws(typeof (CannotPropagateWithEmptyAcc), () => n31.PropagateBackwards());
        }

        [Test]
        public void ThrowsIfPropagateMidLayerWasNeverPropagatedTo()
        {
            Neuron n31 = new Neuron();

            BiasNeuron bias2 = new BiasNeuron(); ;
            Neuron n21 = new Neuron();
            n31.Connect(bias2);
            n31.Connect(n21);

            InputNeuron input = new InputNeuron();
            BiasNeuron bias1 = new BiasNeuron();
            n21.Connect(bias1);
            n21.Connect(input);

            input.Input = 1;
            n31.SetAnswer(1);
            Assert.Throws(typeof(CannotPropagateWithEmptyAcc), () => n21.PropagateBackwards());
        }

        [Test]
        public void TestBackPropWithKnownValues()
        {
            NNetwork n = NetworkTest.XorNetwork();
            n.SetInput(new double[]{1, 1});
            n.SetAnswers(new double[]{0});
            n.BackPropagate();
            double[] deltas = n.GetDeltasForLayer(2);
            Assert.AreNotEqual(deltas[0], 0);
            Assert.AreNotEqual(deltas[1], 0);
            MyAssert.CloseTo(deltas[0], 0, 0.001);
            MyAssert.CloseTo(deltas[1], 0, 0.001);
        }

        [Test]
        public void TestAccumulatesWeightShift()
        {
            Neuron n31 = new Neuron();

            BiasNeuron bias2 = new BiasNeuron(); ;
            Neuron n21 = new Neuron();
            n31.Connect(bias2);
            n31.Connect(n21);

            InputNeuron input = new InputNeuron();
            BiasNeuron bias1 = new BiasNeuron();
            n21.Connect(bias1);
            n21.Connect(input);

            input.Input = 1;
            n31.SetAnswer(0.9);

            double[] ws = n31.GetWeightShifts();
            double acc = ws[1];
            Assert.AreEqual(acc, 0);

            n31.PropagateBackwards();
            ws = n31.GetWeightShifts();
            acc = ws[1];
            Assert.AreNotEqual(acc, 0);

            n31.ApplyTraining(0, 1);
            ws = n31.GetWeightShifts();
            acc = ws[1];
            Assert.AreEqual(acc, 0);
        }

        [Test]
        public void CanApplyTrainingForWholeNetwork()
        {
            NNetwork n = new NNetwork(new int[] { 1, 2, 2, 1 });
            n.SetInput(new double[]{0.3});
            n.SetAnswers(new double[]{0.8});
            n.BackPropagate();
            var output_before = n.GetOutput();
            n.ApplyTraining();
            var output_after = n.GetOutput();
            Assert.AreNotEqual(output_after, output_before);
        }

        [Test]
        public void TestDerivative()
        {
            NNetwork n = new NNetwork(new int[] { 2, 2, 1 });
            n.RandomizeWeights(-1, 10);
            Random random = new Random();
            double x;
            double y;
            double z;
            x = random.NextDouble();
            y = random.NextDouble();
            z = some_function(x, y);
            n.SetInput(new double[] { x, y });
            n.SetAnswers(new double[] { z });
            n.BackPropagate();
            double[] ders = n.Derivatives();
            double[] ests = n.Estimation(0.0001);
            for (int i = 0; i < ders.Length; i++)
            {
                MyAssert.CloseTo(ders[i], ests[i], 0.00000001);
            }
        }

        [Test]
        [Ignore]
        public void TestRandomFunction()
        {
            NNetwork n = new NNetwork(new int[] { 2, 6, 6, 1 });
            n.RandomizeWeights();
            Random random = new Random();
            double x;
            double y;
            double z;
            for (int i = 0; i < 1000; i++)
            {
                x = random.NextDouble();
                y = random.NextDouble();
                z = some_function(x, y);
                n.SetInput(new double[]{x, y});
                n.SetAnswers(new double[]{z});
                n.BackPropagate();
                n.ApplyTraining(0.0001, 0.01);
            }
            //todo
        }

        public static double some_function(double x, double y)
        {
            return x*x*y + y*y;
        }
    }
}
