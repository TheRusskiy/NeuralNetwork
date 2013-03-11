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

    }
    
}
