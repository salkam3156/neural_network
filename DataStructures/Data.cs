namespace neural_network.DataStructures
{
    class Data
    {
        public double[][] DataMatrix { get; set; }

        private int recordsNumber = 0;

        public Data(int rows)
        {
            DataMatrix = new double[rows][];
        }

        public void Add(params double[] record)
        {
            DataMatrix[recordsNumber] = record;
            recordsNumber++;
        }
    }
}