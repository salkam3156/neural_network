namespace neural_network.Primitives
{
    public class Dendrite
    {
        public Pulse InputPulse { get; set; }
        public double Synapticewight { get; set; }
        public bool Learnable { get; set; } = true;
    }
}