using System;
using System.Collections.Generic;
using System.Linq;
using neural_network.Primitives;
using neural_network.DataStructures;

namespace neural_network.Constructs
{
    class NeuralNetwork
    {
        public List<NeuralLayer> Layers { get; set; } = new List<NeuralLayer>();

        public void AddLayer(NeuralLayer layer)
        {
            var dendriteCount = (Layers.Count > 0) ? Layers.Last().Neurons.Count : 1;

            Enumerable
                .Range(0, dendriteCount)
                .ToList()
                .ForEach((dendrite) =>
                {
                    layer.Neurons.ForEach(neuron => neuron.Dendrites.Add(new Dendrite()));
                });


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

        public void Train(Data input, Data expectedOutput, int iterations)
        {
            int epoch = 1;
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

        public void PrintDataTable()
        {
            Console.WriteLine($@"Name    | Neurons   | Weight {Environment.NewLine}");

            Layers.ForEach(layer =>
            {
                Console.WriteLine($@"{layer.Name}    | {layer.Neurons.Count}   | {layer.Weight} {Environment.NewLine}");
            });
        }


        private void ComputeOutput()
        {
            Layers
                .Skip(1)
                .ToList()
                .ForEach(postInputLayer => postInputLayer.ForwardSignal());
        }

        private void OptimizeWeights(double accuracy)
        {
            double learningRate = 0.1f;

            if (accuracy == 1) return;
            if (accuracy > 1) learningRate = -learningRate;

            Layers.ForEach(layer => layer.CalibrateWeights(learningRate, 1));
        }

        private void AppendLayer(NeuralLayer from, NeuralLayer to)
        {
            from.Neurons.ForEach(neuron => neuron.Dendrites.Add(new Dendrite()));

            to.Neurons
                .ForEach((toNeuron) =>
                {
                    from.Neurons
                        .ForEach(fromNeuron => toNeuron.Dendrites.Add(new Dendrite() { CarriedPulse = fromNeuron.OutputPulse, SynapticWeight = to.Weight }));
                });
        }
    }
}