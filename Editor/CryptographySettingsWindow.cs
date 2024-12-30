using UnityEditor;
using UnityEngine;
using System.Linq;
using UniSharp.Security.Cryptography.Settings;

namespace UniSharp.Security.Cryptography.Editor
{
    public class CryptographySettingsWindow : EditorWindow
    {
        private const string SettingsPath = "Assets/Resources";
        private const string SettingsFile = "CryptographySettings.asset";
        private const string FullSettingsPath = SettingsPath + "/" + SettingsFile;

        [MenuItem("UniSharp/Security/Cryptography Settings")]
        public static void ShowWindow()
        {
            GetWindow<CryptographySettingsWindow>("Cryptography Settings");
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            GUILayout.Label("Cryptography Settings", EditorStyles.boldLabel);

            if (AssetExists())
            {
                EditorGUILayout.HelpBox("Cryptography Settings file exists.", MessageType.Info);

                if (GUILayout.Button("Show Settings"))
                {
                    ShowSettings();
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Cryptography Settings file does not exist.", MessageType.Warning);

                if (GUILayout.Button("Create Settings"))
                {
                    CreateSettings();
                }
            }
        }

        private bool AssetExists()
        {
            return AssetDatabase.LoadAssetAtPath<CryptographySettings>(FullSettingsPath) != null;
        }

        private void ShowSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<CryptographySettings>(FullSettingsPath);
            Selection.activeObject = settings;
        }

        private void CreateSettings()
        {
            // Ensure the directory exists
            if (!AssetDatabase.IsValidFolder(SettingsPath))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }

            // Create the asset
            var settings = ScriptableObject.CreateInstance<CryptographySettings>();
            AssetDatabase.CreateAsset(settings, FullSettingsPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.DisplayDialog("Success", "Cryptography Settings file has been created.", "OK");
        }
    }
}
