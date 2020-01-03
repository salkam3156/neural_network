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

        public NeuralLayer(int neuronCount, double initialWeight, string name)
        {
            Weight = initialWeight;
            Name = name;

            Enumerable.Range(0, neuronCount).ToList()
                .ForEach(elem => Neurons.Add(new Neuron()));
        }

        public void ForwardSignal()
        {
            Neurons.ForEach(neuron => neuron.Fire());
        }

        public void CalibrateWeights(double learningRate, double delta)
        {
            Weight += learningRate * delta;

            Neurons.ForEach(neuron => neuron.UpdateWeights(Weight));
        }

        public void PrintDetails()
        {
            Console.WriteLine($"{Name}, Weight: {Weight}");
        }
    }
}