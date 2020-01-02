using System;
using System.Collections.Generic;
using System.Linq;
using neural_network.Primitives;
using neural_network.Data;

namespace neural_network.Constructs
{
    public class NeuralNetwork
    {
        public List<NeuralLayer> Layers { get; set; } = new List<NeuralLayer>();

        public void AddLayer(NeuralLayer layer)
        {
            var dendriteCount = Layers.Any() ? Layers.Last().Neurons.Count : 1;

            layer.Neurons.ForEach(neuron =>
            {
                neuron.PreceedingDendrites.AddRange(GetDendrites(dendriteCount));
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

        private void ComputeOutput()
        {
            Layers
                .Skip(1)
                .ToList()
                .ForEach(layer => layer.ForwardSignal());
        }

        private void OptimizeWeights(double accuracy)
        {
            if (accuracy == 1) return;

            Layers.ForEach(layer => layer.ComputeSynapticWeights(Math.Abs(accuracy), 1));
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

        public void Train(NeuralData input, NeuralData expectedOutput, int iterations, double learningRate = 0.1d)
        {
            int epoch = 0;
            while (iterations >= epoch)
            {
                var inputLayer = Layers.First();
                var outputs = new List<double>();

                for (int i = 0; i < input.DataMatrix.Length; i++)
                {
                    for (int j = 0; j < input.DataMatrix[i].Length; j++)
                    {
                        inputLayer.Neurons[j].OutputPulse.SignalValue = input.DataMatrix[i][j];
                    }
                    ComputeOutput();
                    outputs.Add(Layers.Last().Neurons.First().OutputPulse.SignalValue);
                }

                double accuracySum = 0;
                int y_counter = 0;
                outputs.ForEach((x) =>
                {
                    if (x == expectedOutput.DataMatrix[y_counter].First())
                    {
                        accuracySum++;
                    }

                    y_counter++;
                });

                OptimizeWeights(accuracySum / y_counter);
                Console.WriteLine("Epoch: {0}, Accuracy: {1} %", epoch, (accuracySum / y_counter) * 100);
                epoch++;
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