using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.src
{
    class NetworkTrainer
    {
        private NNetwork network;
        private double cost_acc;
        private int iteration_count;
        private bool _isLearning = true;

        public NetworkTrainer(NNetwork network)
        {
            this.network = network;
        }

        public bool IsLearning
        {
            get { return _isLearning; }
            set { _isLearning = value; }
        }

        public void TrainPrediction(double[] train_set, double lambda=0, double alpha=1)
        {
            cost_acc = 0;
            iteration_count = 0;
            int input_count = network.Neurons[0].Length-1; //without bias
            int output_count = network.Neurons[network.Neurons.Length - 1].Length - 1; //without bias
            for (int i = 0; i < train_set.Length - input_count - output_count; i++)
            {
                network.SetInput(GetArrayRange(train_set, i, i+input_count));
                network.SetAnswers(GetArrayRange(train_set, i + input_count, i + input_count+output_count));
                CalculateError();
                network.BackPropagate();
            }
            if (_isLearning) network.ApplyTraining(lambda, alpha);
        }

        private double[] GetArrayRange(double[] array, int from, int to_exclusive)
        {
            return array.ToList().GetRange(from, to_exclusive - from).ToArray();
        }

        private void CalculateError()
        {
            cost_acc += network.CostFunction();
            iteration_count++;
        }
        public double GetError()
        {
            if (iteration_count == 0) throw new NoErrorInfoYetException();
            return -cost_acc/iteration_count;
        }

        public void TrainCalculation(double[][] inputs, double[][] outputs, double lambda = 0, double alpha = 1)
        {
            //TODO assert all types of dimensions
            cost_acc = 0;
            iteration_count = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                network.SetInput(inputs[i]);
                network.SetAnswers(outputs[i]);
                CalculateError();
                network.BackPropagate();
            }
            if (_isLearning) network.ApplyTraining(lambda, alpha);
        }
    }

    internal class NoErrorInfoYetException : Exception{}
}
