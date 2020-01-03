namespace neural_network.Primitives
{
    public class Dendrite
    {
        public Pulse CarriedPulse { get; set; } = new Pulse();
        public double SynapticWeight { get; set; }
        public bool Learnable { get; set; } = true;
    }
}