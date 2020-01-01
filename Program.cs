using neural_network.Constructs;

namespace neural_network
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuralNetwork model = new NeuralNetwork();
            model.Layers.Add(new NeuralLayer(2, 0.1, "INPUT"));
            model.Layers.Add(new NeuralLayer(2, 0.1, "HIDDEN"));
            model.Layers.Add(new NeuralLayer(1, 0.1, "OUTPUT"));

            model.Build();
            model.PrintDataTable();
        }
    }
}
