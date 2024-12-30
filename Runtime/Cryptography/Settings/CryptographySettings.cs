using UnityEngine;

namespace UniSharp.Security.Cryptography.Settings
{
    public partial class CryptographySettings : ScriptableObject
    {
        public const string FileName = "CryptographySettings";

        // Shared Settings
        [Header("General Settings")]
        [SerializeField] private AlgorithmType _defaultAlgorithm = AlgorithmType.DES;

        // DES Settings
        [Header("DES Settings")]
        [SerializeField] int _desKeyLength = 8; // 8 bytes (64 bits)
        [SerializeField] private string _desPublicKey;
        [SerializeField] private string _desPrivateKey;

        // AES Settings
        [Header("AES Settings")]
        [SerializeField] int _aes128KeyLength = 16; // 16 bytes (128 bits)
        [SerializeField] int _aes192KeyLength = 24; // 24 bytes (192 bits)
        [SerializeField] int _aes256KeyLength = 32; // 32 bytes (256 bits)
        [SerializeField] private string _aesKey; // 16 characters for AES-128
        [SerializeField] private string _aesIV;   // 16 characters for AES-128

        // Additional algorithms can be added here...

        public AlgorithmType DefaultAlgorithm => _defaultAlgorithm;

        public int DES_KeyLength => _desKeyLength;
        public string DesPublicKey => _desPublicKey;
        public string DesPrivateKey => _desPrivateKey;

        public int AES128_KeyLength => _aes128KeyLength;
        public int AES192_KeyLength => _aes192KeyLength;
        public int AES256_KeyLength => _aes256KeyLength;
        public string AesKey => _aesKey;
        public string AesIV => _aesIV;
    }
}
