using System;
using UnityEditor;
using UnityEngine;
using UniSharp.Security.Cryptography.Settings;

namespace UniSharp.Security.Cryptography.Editor
{
    [CustomEditor(typeof(CryptographySettings))]
    public class CryptographySettingsEditor : UnityEditor.Editor
    {
        private int selectedAlgorithmIndex;

        public override void OnInspectorGUI()
        {
            var settings = (CryptographySettings)target;

            // Draw default inspector
            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Key Generation Tools", EditorStyles.boldLabel);

            // Algorithm Selection (DES, AES128, AES192, AES256)
            string[] algorithmNames = Enum.GetNames(typeof(AlgorithmType));
            selectedAlgorithmIndex = EditorGUILayout.Popup("Algorithm", selectedAlgorithmIndex, algorithmNames);

            // Set the key length based on the selected algorithm
            int keyLength = selectedAlgorithmIndex switch
            {
                0 => 8,  // DES
                1 => 16, // AES128
                2 => 24, // AES192
                3 => 32, // AES256
                _ => 16  // Default to AES128 if invalid
            };

            // Display Key Length
            EditorGUILayout.LabelField($"Key Length: {keyLength} bytes");

            // Generate DES Keys if DES is selected
            if (selectedAlgorithmIndex == 0 && GUILayout.Button("Generate DES Keys"))
            {
                settings.SetDesKeys(GenerateKey(keyLength), GenerateKey(keyLength));
                EditorUtility.SetDirty(settings);
            }

            // Generate AES Keys and IV if AES is selected
            if (selectedAlgorithmIndex >= 1 && GUILayout.Button("Generate AES Key and IV"))
            {
                settings.SetAesKeys(GenerateKey(keyLength), GenerateKey(keyLength));
                EditorUtility.SetDirty(settings);
            }

            // Save Changes if any GUI changes
            if (GUI.changed)
            {
                EditorUtility.SetDirty(settings);
            }
        }

        private string GenerateKey(int length)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var keyBytes = new byte[length];
            rng.GetBytes(keyBytes);
            return Convert.ToBase64String(keyBytes).Substring(0, length); // Trim to requested length
        }
    }
}
