namespace UniSharp.Security.Cryptography
{
    public class Options
    {
        public AlgorithmType Algorithm { get; set; } = AlgorithmType.DES;
        public string Key1 { get; set; } = null;
        public string Key2 { get; set; } = null;
    }
}
