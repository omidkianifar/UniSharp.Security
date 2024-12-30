#if UNITY_EDITOR

using UnityEngine;

namespace UniSharp.Security.Cryptography.Settings
{
    [CreateAssetMenu(fileName = FileName, menuName = "UniSharp/Security/Create Cryptography Settings")]
    public partial class CryptographySettings 
    {
        public void SetDesKeys(string publicKey, string privateKey)
        {
            _desPublicKey = publicKey;
            _desPrivateKey = privateKey;
        }

        public void SetAesKeys(string key, string iv)
        {
            _aesKey = key;
            _aesIV = iv;
        }
    }
}

#endif
