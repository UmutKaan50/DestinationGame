//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace y01cu {
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEditor;

    public class FontReplacer : EditorWindow {
        Font newFont;

        TMP_FontAsset newTMP_FontAsset;

        [MenuItem("Tools/Font Replacer")]
        public static void ShowWindow() {
            GetWindow<FontReplacer>("Font Replacer");
        }

        private void OnGUI() {
            GUILayout.Label("Replace all TMP fonts in the scene", EditorStyles.boldLabel);

            newTMP_FontAsset =
                (TMP_FontAsset)EditorGUILayout.ObjectField("New TMP Font", newTMP_FontAsset, typeof(TMP_FontAsset),
                    false);

            if (GUILayout.Button("Replace TMP Fonts")) {
                ReplaceTMPFontsInScene();
            }


            GUILayout.Label("Replace all legacy fonts in the scene", EditorStyles.boldLabel);

            newFont = (Font)EditorGUILayout.ObjectField("New Font", newFont, typeof(Font), false);

            if (GUILayout.Button("Replace Fonts")) {
                ReplaceLegacyFontsInScene();
            }
        }

        void ReplaceTMPFontsInScene() {
            if (newTMP_FontAsset == null) {
                Debug.LogError("No TMP font selected!");
                return;
            }

            // Get all TMP objects in the scene
            TextMeshProUGUI[] texts = GameObject.FindObjectsOfType<TextMeshProUGUI>();

            // Start the undo grouping
            Undo.RecordObjects(texts, "TMP Font Replacement");

            foreach (TextMeshProUGUI text in texts) {
                // Change the font
                text.font = newTMP_FontAsset;
            }

            TextMeshProUGUI[] textMeshProUGUIs = GameObject.FindObjectsOfType<TextMeshProUGUI>();

            Undo.RecordObjects(textMeshProUGUIs, "TMP Font Replacement");

            foreach (TextMeshProUGUI textMeshProUGUI in textMeshProUGUIs) {
                // Change the font
                textMeshProUGUI.font = newTMP_FontAsset;
            }

            // Save the changes
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Debug.Log("TMP Font replaced in " + textMeshProUGUIs.Length + " objects.");
        }

        void ReplaceLegacyFontsInScene() {
            // Ensure that a new font is selected
            if (newFont == null) {
                Debug.LogError("No font selected!");
                return;
            }

            // Get all Text objects in the scene
            Text[] texts = GameObject.FindObjectsOfType<Text>();

            // Start the undo grouping
            Undo.RecordObjects(texts, "Text Font Replacement");

            foreach (Text text in texts) {
                // Change the font
                text.font = newFont;
            }
        }
        
        void ReplaceTMPFontsInProject()
        {
            // Ensure that a new font is selected
            if (newFont == null)
            {
                Debug.LogError("No font selected!");
                return;
            }

            // Get all Text objects in all prefabs
            string[] guids = AssetDatabase.FindAssets("t:Prefab");
            List<GameObject> allPrefabs = new List<GameObject>();
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                allPrefabs.Add(prefab);
            }

            foreach (GameObject prefab in allPrefabs)
            {
                bool prefabUpdated = false;

                Text[] texts = prefab.GetComponentsInChildren<Text>(true);

                foreach (Text text in texts)
                {
                    if (text.font != newFont)
                    {
                        if (!prefabUpdated)
                        {
                            prefabUpdated = true;
                            Undo.RecordObject(text, "Font Replacement");
                        }

                        text.font = newFont;
                    }
                }

                if (prefabUpdated)
                {
                    EditorUtility.SetDirty(prefab);
                    AssetDatabase.SaveAssets();
                }
            }

            Debug.Log("Font replaced in prefabs.");
        }
        
        
        void ReplaceLegacyFontsInProject()
        {
            // Ensure that a new font is selected
            if (newFont == null)
            {
                Debug.LogError("No font selected!");
                return;
            }

            // Get all Text objects in all prefabs
            string[] guids = AssetDatabase.FindAssets("t:Prefab");
            List<GameObject> allPrefabs = new List<GameObject>();
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                allPrefabs.Add(prefab);
            }

            foreach (GameObject prefab in allPrefabs)
            {
                bool prefabUpdated = false;

                Text[] texts = prefab.GetComponentsInChildren<Text>(true);

                foreach (Text text in texts)
                {
                    if (text.font != newFont)
                    {
                        if (!prefabUpdated)
                        {
                            prefabUpdated = true;
                            Undo.RecordObject(text, "Font Replacement");
                        }

                        text.font = newFont;
                    }
                }

                if (prefabUpdated)
                {
                    EditorUtility.SetDirty(prefab);
                    AssetDatabase.SaveAssets();
                }
            }

            Debug.Log("Font replaced in prefabs.");
        }
    }
}