using System;
using neural_network.Constructs;
using neural_network.DataStructures;

namespace neural_network
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuralNetwork model = new NeuralNetwork();
            model.Layers.Add(new NeuralLayer(2, 0.1, "INPUT"));
            model.Layers.Add(new NeuralLayer(1, 0.1, "OUTPUT"));

            model.Build();
            Console.WriteLine("----Before Training------------");
            model.PrintDataTable();

            Console.WriteLine();

            Data input = new Data(4);
            input.Add(0, 0);
            input.Add(0, 1);
            input.Add(1, 0);
            input.Add(1, 1);

            Data expectedOutput = new Data(4);
            expectedOutput.Add(0);
            expectedOutput.Add(0);
            expectedOutput.Add(0);
            expectedOutput.Add(1);

            model.Train(input, expectedOutput, iterations: 10);
            Console.WriteLine();
            Console.WriteLine("----After Training------------");
            model.PrintDataTable();
        }
    }
}
