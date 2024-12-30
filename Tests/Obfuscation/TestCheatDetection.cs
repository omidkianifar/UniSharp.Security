using UniSharp.Security.Obfuscation;
using UnityEngine;

namespace UniSharp.SecurityTest.Obfuscation
{
    // Assign this Script to a gameobject, then try change the value with CheatEngine

    public class TestCheatDetection : MonoBehaviour 
    {
        public int secureInt;
        public int unSecureInt;

        private ObfuscateInt secureInt1;

        public void Start()
        {
            ObfuscateInt.OnCheetDetected += (a) => 
            {
                Debug.Log($"Cheat Detected!");
            };

            SetRandomNumber();

            Debug.Log("Try to cheet...");
            Debug.Log($"First Value (Secure): {secureInt1}");
            Debug.Log($"First Value (UnSecure): {unSecureInt}");
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetRandomNumber();

                Debug.Log($"New Value (Secure): {secureInt1}");
                Debug.Log($"New Value (UnSecure): {unSecureInt}");
            }

            Debug.Log($"Secure: {secureInt1}");
            Debug.Log($"UnSecure: {unSecureInt}");

            secureInt = secureInt1;
        }

        private void SetRandomNumber()
        {
            secureInt1 = Random.Range(1, 1000);
            unSecureInt = Random.Range(1, 1000);
        }
    }
}