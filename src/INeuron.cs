namespace NeuralNetwork
{
    interface INeuron
    {
        void Connect(INeuron neuron, double weight);
        double Activation();
    }
}
