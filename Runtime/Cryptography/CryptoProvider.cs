using System;
using UniSharp.Security.Cryptography.Cryptographer;
using UniSharp.Security.Cryptography.Settings;
using UnityEngine;

namespace UniSharp.Security.Cryptography
{
    public class CryptoProvider
    {
        private static CryptographySettings _settings;

        public static ICryptographer Create(Action<Options> setupAction = null)
        {
            LoadSettings();

            var options = new Options();

            setupAction?.Invoke(options);

            return options.Algorithm switch
            {
                AlgorithmType.DES => new DESCryptographer(options, _settings),
                AlgorithmType.AES128 | AlgorithmType.AES192 | AlgorithmType.AES256 => new AESCryptographer(options, _settings),
                _ => throw new NotSupportedException("Unsupported algorithm type")
            };
        }

        private static void LoadSettings() 
        {
            _settings ??= Resources.Load<CryptographySettings>(CryptographySettings.FileName);

            if(_settings == null)
                throw new MissingReferenceException($"{CryptographySettings.FileName} not found!");
        }
    }
}
