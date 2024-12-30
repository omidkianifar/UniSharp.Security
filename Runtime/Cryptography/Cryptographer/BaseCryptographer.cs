using System;
using System.Text;
using UniSharp.Security.Cryptography.Settings;

namespace UniSharp.Security.Cryptography.Cryptographer
{
    public abstract class BaseCryptographer : ICryptographer
    {
        protected readonly byte[] _key1Bytes;
        protected readonly byte[] _key2Bytes;

        protected BaseCryptographer(Options options, CryptographySettings settings)
        {
            ValidateKeys(options, settings);

            if (string.IsNullOrEmpty(options.Key1) || string.IsNullOrEmpty(options.Key2))
                throw new ArgumentNullException("Keys must not be null or empty.");

            _key1Bytes = Encoding.UTF8.GetBytes(options.Key1);
            _key2Bytes = Encoding.UTF8.GetBytes(options.Key2);
        }

        public abstract string Encrypt(string plainText);
        public abstract string Decrypt(string cipherText);

        protected virtual void ValidateKeys(Options options, CryptographySettings settings) 
        {

        }
    }
}
