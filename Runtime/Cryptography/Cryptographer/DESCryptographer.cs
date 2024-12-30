using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UniSharp.Security.Cryptography.Settings;

namespace UniSharp.Security.Cryptography.Cryptographer
{
    public class DESCryptographer : BaseCryptographer
    {
        public DESCryptographer(Options options, CryptographySettings settings) : base(options, settings) { }

        protected override void ValidateKeys(Options options, CryptographySettings settings)
        {
            base.ValidateKeys(options, settings);

            if (string.IsNullOrEmpty(options.Key1))
                options.Key1 = settings.DesPublicKey;

            if (string.IsNullOrEmpty(options.Key2))
                options.Key2 = settings.DesPrivateKey;

            if(options.Key1.Length != settings.DES_KeyLength)
                throw new ArgumentException($"Public Key must be {settings.DES_KeyLength} bytes for DES.");

            if (options.Key2.Length != settings.DES_KeyLength)
                throw new ArgumentException($"Private Key must be {settings.DES_KeyLength} bytes for DES.");
        }

        public override string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) throw new ArgumentNullException("plainText is null or empty");

            using var des = new DESCryptoServiceProvider();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, des.CreateEncryptor(_key1Bytes, _key2Bytes), CryptoStreamMode.Write);
            var inputByteArray = Encoding.UTF8.GetBytes(plainText);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        public override string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) throw new ArgumentNullException("cipherText is null or empty");

            using var des = new DESCryptoServiceProvider();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, des.CreateDecryptor(_key1Bytes, _key2Bytes), CryptoStreamMode.Write);
            var inputByteArray = Convert.FromBase64String(cipherText.Replace(' ', '+'));
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}
