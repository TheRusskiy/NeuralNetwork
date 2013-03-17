using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.src
{
    class TanhNeuron : Neuron
    {
        protected override double Function()
        {
            double z = WeightsOnInputs();
            double value = (Math.Exp(z) - Math.Exp(-z))/(Math.Exp(z) + Math.Exp(-z));
            return value;
        }
        protected override double CalculateMidLayerDelta()
        {
            double a = Activation();
            return weight_x_delta_acc * (1 - a*a);
        }

        public override double LastLayerCost()
        {
            if (!is_last_layer)
            {
                throw new CannotCalculateCostForNonLastLayerNeuronException();
            }
            //test it in google: 
            //  abs(y) * log( abs(x) ) + (1 - abs(y)) * log (1 - abs(x)) + log(1 - abs(x - y))
            double h = Activation();
            double y = GetAnswer();
            var addition =  Math.Log(1-Math.Abs(h - y));
            h = Math.Abs(h);
            y = Math.Abs(y);
            return y * Math.Log(h) + (1 - y) * Math.Log(1 - h) + addition;
        }
    }
}
