using System;
using System.Collections.Generic;
using System.Linq;
using neural_network.Primitives;

namespace neural_network.Constructs
{
    public class NeuralNetwork
    {
        public List<NeuralLayer> Layers { get; set; } = new List<NeuralLayer>();

        public void AddLayer(NeuralLayer layer)
        {
            layer.Neurons.ForEach(neuron =>
            {
                neuron.PreceedingDendrites.AddRange(GetDendrites(Layers?.Last().Neurons.Count
                ));
            }
            );
        }

        public void Build()
        {
            int i = 0;
            foreach (var layer in Layers)
            {
                if (i >= Layers.Count - 1)
                {
                    break;
                }

                var nextLayer = Layers[i + 1];
                AppendLayer(layer, nextLayer);

                i++;
            }
        }

        public void PrintDataTable()
        {
            Console.WriteLine($@"Name    | Neurons   | Weight {Environment.NewLine}");

            Layers.ForEach(layer =>
            {
                Console.WriteLine($@"{layer.Name}    | {layer.Neurons.Count}   | {layer.Weight} {Environment.NewLine}");
            });
        }

        private void AppendLayer(NeuralLayer prevLayer, NeuralLayer nextLayer)
        {
            foreach (var to in nextLayer.Neurons)
            {
                foreach (var from in prevLayer.Neurons)
                {
                    to.PreceedingDendrites.Add(new Dendrite() { CarriedPulse = to.OutputPulse, Synapticewight = nextLayer.Weight });
                }
            }
        }

        private IEnumerable<Dendrite> GetDendrites(int? count)
        {
            if (count.HasValue)
            {
                for (int i = 0; i < count; i++)
                {
                    yield return new Dendrite();
                }
            }
        }


    }
}