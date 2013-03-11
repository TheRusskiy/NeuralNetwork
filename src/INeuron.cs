namespace NeuralNetwork
{
    interface INeuron
    {
        void Connect(INeuron neuron, double weight=0);
        double Activation();
        bool IsCachingActivationResults
        {get; set;}

        void InvalidateActivationCache();
    }
}
