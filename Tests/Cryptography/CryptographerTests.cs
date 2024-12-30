using NUnit.Framework;
using System;
using System.Text;
using UniSharp.Security.Cryptography.Cryptographer;
using UniSharp.Security.Cryptography.Settings;
using UnityEngine;

namespace UniSharp.Security.Cryptography.Tests
{
    public class CryptographerTests
    {
        private CryptographySettings _settings;

        [SetUp]
        public void Setup()
        {
            // Initialize the settings with appropriate key lengths.
            _settings = Resources.Load<CryptographySettings>(CryptographySettings.FileName);
        }

        [Test]
        public void TestDESCryptographer_EncryptDecrypt()
        {
            // Arrange
            var options = new Options
            {
                Key1 = _settings.DesPublicKey,
                Key2 = _settings.DesPrivateKey,
                Algorithm = AlgorithmType.DES
            };

            var desCryptographer = new DESCryptographer(options, _settings);
            var plainText = "Hello, DES!";

            // Act
            var encrypted = desCryptographer.Encrypt(plainText);
            var decrypted = desCryptographer.Decrypt(encrypted);

            // Assert
            Assert.AreNotEqual(plainText, encrypted); // Ensure the encrypted text is different.
            Assert.AreEqual(plainText, decrypted); // Ensure the decrypted text matches the original.
        }

        [Test]
        public void TestAESCryptographer_EncryptDecrypt()
        {
            // Arrange
            var options = new Options
            {
                Key1 = _settings.AesKey,
                Key2 = _settings.AesIV,
                Algorithm = AlgorithmType.AES128
            };

            var aesCryptographer = new AESCryptographer(options, _settings);
            var plainText = "Hello, AES!";

            // Act
            var encrypted = aesCryptographer.Encrypt(plainText);
            var decrypted = aesCryptographer.Decrypt(encrypted);

            // Assert
            Assert.AreNotEqual(plainText, encrypted); // Ensure the encrypted text is different.
            Assert.AreEqual(plainText, decrypted); // Ensure the decrypted text matches the original.
        }

        [Test]
        public void TestAESCryptographer_InvalidKeyLength()
        {
            // Arrange with invalid key length for AES
            var options = new Options
            {
                Key1 = "shortkey", // Invalid length for AES128 (should be 16 bytes)
                Key2 = _settings.AesIV,
                Algorithm = AlgorithmType.AES128
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new AESCryptographer(options, _settings));
        }

        [Test]
        public void TestDESCryptographer_InvalidKeyLength()
        {
            // Arrange with invalid key length for DES
            var options = new Options
            {
                Key1 = "shortkey", // Invalid length for DES (should be 8 bytes)
                Key2 = _settings.DesPrivateKey,
                Algorithm = AlgorithmType.DES
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new DESCryptographer(options, _settings));
        }
    }
}
