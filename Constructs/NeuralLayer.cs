using System;
using System.Collections.Generic;
using System.Linq;
using neural_network.Primitives;

namespace neural_network.Constructs
{
    public class NeuralLayer
    {
        public List<Neuron> Neurons { get; set; } = new List<Neuron>();
        public string Name { get; set; } = string.Empty;
        public double Weight { get; set; }

        public NeuralLayer(int neuronCount, double initialWight, string name)
        {
            Weight = initialWight;
            Name = name;

            Enumerable.Range(0, neuronCount).ToList().ForEach(element => Neurons.Add(new Neuron()));
        }

        public void ComputeSynapticWeights(double learningRate, double delta)
        {
            (Neurons as List<Neuron>).ForEach(neuron => neuron.Compute(learningRate, delta));
        }

        public void LogDetails()
        {
            Console.WriteLine($@"{Name}, Weight: {Weight}");
        }
    }
}