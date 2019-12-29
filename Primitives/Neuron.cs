using System;
using System.Collections.Generic;

namespace neural_network.Primitives
{
    public class Neuron
    {
        public List<Dendrite> PreceedingDendrites { get; set; } = new List<Dendrite>();
        public Pulse OutputPulse { get; set; } = new Pulse();
        private double _weight;
        private readonly double _treshold = 1;

        public void Fire()
        {
            OutputPulse.SignalValue = ActivationValue();
        }

        public void Compute(double learningRate, double delta)
        {
            _weight += learningRate * delta;

            foreach (var dendrite in PreceedingDendrites)
            {
                dendrite.Synapticewight = _weight;
            }
        }

        private double InputSignalsSum()
        {
            double computeValue = 0.0f;

            foreach (var dendrite in PreceedingDendrites)
            {
                computeValue += dendrite.InputPulse.SignalValue * dendrite.Synapticewight;
            }

            return computeValue;
        }

        private double ActivationValue()
        {
            return InputSignalsSum() >= _treshold ? 0 : _treshold;
        }
    }
}