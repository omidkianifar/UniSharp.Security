namespace UniSharp.Security.Cryptography.Cryptographer
{
    public interface ICryptographer
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
