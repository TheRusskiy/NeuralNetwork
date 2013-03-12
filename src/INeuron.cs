namespace NeuralNetwork
{
    interface INeuron
    {
        void Connect(INeuron neuron, double weight=0);
        double Activation();
        bool IsCachingActivationResults
        {get; set;}

        void InvalidateActivationCache();
        void SetAnswer(double desired);
        double GetDelta();
        void PropagateBackwards();
        void AddWeightOnDelta(double weight_x_delta);
    }
}
