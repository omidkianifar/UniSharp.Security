using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UniSharp.Security.Cryptography.Settings;

namespace UniSharp.Security.Cryptography.Cryptographer
{
    public class AESCryptographer : BaseCryptographer
    {
        public AESCryptographer(Options options, CryptographySettings settings) : base(options, settings) { }

        protected override void ValidateKeys(Options options, CryptographySettings settings)
        {
            base.ValidateKeys(options, settings);

            if (string.IsNullOrEmpty(options.Key1))
                options.Key1 = settings.AesKey;

            if (string.IsNullOrEmpty(options.Key2))
                options.Key2 = settings.AesIV;

            var keyLength = options.Algorithm switch
            {
                AlgorithmType.AES128 => settings.AES128_KeyLength,
                AlgorithmType.AES192 => settings.AES192_KeyLength,
                AlgorithmType.AES256 => settings.AES256_KeyLength,
                _ => throw new NotImplementedException()
            };

            if (options.Key1.Length != keyLength)
                throw new ArgumentException($"AesKey must be {keyLength} bytes for AES.");

            if (options.Key2.Length != keyLength)
                throw new ArgumentException($"AesIV must be {keyLength} bytes for AES.");
        }

        public override string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) throw new ArgumentNullException("plainText is null or empty");

            using var aes = new AesCryptoServiceProvider { Key = _key1Bytes, IV = _key2Bytes };
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            var inputByteArray = Encoding.UTF8.GetBytes(plainText);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        public override string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) throw new ArgumentNullException("cipherText is null or empty");

            using var aes = new AesCryptoServiceProvider { Key = _key1Bytes, IV = _key2Bytes };
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            var inputByteArray = Convert.FromBase64String(cipherText.Replace(' ', '+'));
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}
