using System.Collections.Generic;

namespace neural_network.Primitives
{
    public class Neuron
    {
        public List<Dendrite> Dendrites { get; set; } = new List<Dendrite>();

        public Pulse OutputPulse { get; set; } = new Pulse();
        private readonly double _threshold = 1;

        public void Fire()
        {
            OutputPulse.SignalValue = ActivationValue();
        }

        public void UpdateWeights(double recalibratedWeights)
        {
            foreach (var terminal in Dendrites)
            {
                terminal.SynapticWeight = recalibratedWeights;
            }
        }

        private double InputSignalsSum()
        {
            double computeValue = 0.0f;
            foreach (var d in Dendrites)
            {
                computeValue += d.CarriedPulse.SignalValue * d.SynapticWeight;
            }

            return computeValue;
        }

        private double ActivationValue()
        {
            return InputSignalsSum() <= _threshold ? 0 : _threshold;
        }
    }
}