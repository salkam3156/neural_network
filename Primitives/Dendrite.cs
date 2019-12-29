namespace neural_network.Primitives
{
    public class Dendrite
    {
        public Pulse CarriedPulse { get; set; }
        public double Synapticewight { get; set; }
        public bool Learnable { get; set; } = true;
    }
}