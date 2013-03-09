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
        public void XNORTest()
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
            AssertClose(a3_1.Activation(), 1);

            a1_1.Input = 0;
            a1_2.Input = 1;
            AssertClose(a3_1.Activation(), 0);

            a1_1.Input = 1;
            a1_2.Input = 0;
            AssertClose(a3_1.Activation(), 0);

            a1_1.Input = 1;
            a1_2.Input = 1;
            AssertClose(a3_1.Activation(), 1);
        }

        public static void AssertClose(double arg1, double arg2, double by = 0.0001)
        {
            Assert.Less(Math.Abs(arg1 - arg2), by);
        }

    }
}
