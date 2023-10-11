//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace y01cu {
    public class FontReplacer : EditorWindow {
        private Font newFontForProject;

        private Font newFontForScene;

        private TMP_FontAsset newTMP_FontAssetForProject;
        private TMP_FontAsset newTMP_FontAssetForScene;

        private void OnGUI() {
            GUILayout.Label("Replace all TMP fonts in the scene", EditorStyles.boldLabel);

            newTMP_FontAssetForScene =
                (TMP_FontAsset)EditorGUILayout.ObjectField("New TMP Font For Scene", newTMP_FontAssetForScene,
                    typeof(TMP_FontAsset),
                    false);

            if (GUILayout.Button("Replace TMP Fonts In The Scene")) ReplaceTMPFontsInScene();

            //--------------------------------------------------------------------------------

            GUILayout.Label("Replace all legacy fonts in the scene", EditorStyles.boldLabel);

            newFontForScene =
                (Font)EditorGUILayout.ObjectField("New Font For Scene", newFontForScene, typeof(Font), false);

            if (GUILayout.Button("Replace Fonts In The Scene")) ReplaceLegacyFontsInScene();

            //--------------------------------------------------------------------------------

            CreateDivider();

            GUILayout.Label("Replace all TMP fonts in the project", EditorStyles.boldLabel);

            newTMP_FontAssetForProject = (TMP_FontAsset)EditorGUILayout.ObjectField("New TMP Font For Project",
                newTMP_FontAssetForProject, typeof(TMP_FontAsset),
                false);

            if (GUILayout.Button("Replace TMP Fonts In The Project")) ReplaceTMPFontsInProject();

            //--------------------------------------------------------------------------------

            GUILayout.Label("Replace all legacy fonts in the project", EditorStyles.boldLabel);

            newFontForProject =
                (Font)EditorGUILayout.ObjectField("New Font For Project", newFontForProject, typeof(Font), false);

            if (GUILayout.Button("Replace Fonts In The Project")) ReplaceLegacyFontsInProject();
        }


        [MenuItem("Tools/Font Replacer")]
        public static void ShowWindow() {
            GetWindow<FontReplacer>("Font Replacer");
        }

        //------------------------------------------------------------

        private void ReplaceTMPFontsInScene() {
            if (newTMP_FontAssetForScene == null) {
                Debug.LogError("No TMP font selected!");
                return;
            }

            // Get all TMP objects in the scene
            var texts = FindObjectsOfType<TextMeshProUGUI>();

            // Start the undo grouping
            Undo.RecordObjects(texts, "TMP Font Replacement");

            foreach (var text in texts)
                // Change the font
                text.font = newTMP_FontAssetForScene;

            var textMeshProUGUIs = FindObjectsOfType<TextMeshProUGUI>();

            Undo.RecordObjects(textMeshProUGUIs, "TMP Font Replacement");

            foreach (var textMeshProUGUI in textMeshProUGUIs)
                // Change the font
                textMeshProUGUI.font = newTMP_FontAssetForScene;

            // Save the changes
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            Debug.Log("TMP Font replaced in " + textMeshProUGUIs.Length + " objects.");
        }

        //------------------------------------------------------------

        private void ReplaceLegacyFontsInScene() {
            // Ensure that a new font is selected
            if (newFontForScene == null) {
                Debug.LogError("No font selected!");
                return;
            }

            // Get all Text objects in the scene
            var texts = FindObjectsOfType<Text>();

            // Start the undo grouping
            Undo.RecordObjects(texts, "Text Font Replacement");

            foreach (var text in texts)
                // Change the font
                text.font = newFontForScene;

            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            Debug.Log("Legacy Text Fonts replaced in " + texts.Length + " objects.");
        }

        //------------------------------------------------------------

        private void ReplaceTMPFontsInProject() {
            // Ensure that a new font is selected
            if (newTMP_FontAssetForProject == null) {
                Debug.LogError("No font selected!");
                return;
            }

            // Get all Text objects in all prefabs
            var guids = AssetDatabase.FindAssets("t:Prefab");
            var allPrefabs = new List<GameObject>();
            foreach (var guid in guids) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                allPrefabs.Add(prefab);
            }

            foreach (var prefab in allPrefabs) {
                var prefabUpdated = false;

                var textMeshProUGUIs = prefab.GetComponentsInChildren<TextMeshProUGUI>(true);

                foreach (var textMeshProUGUI in textMeshProUGUIs)
                    if (textMeshProUGUI.font != newTMP_FontAssetForProject) {
                        if (!prefabUpdated) {
                            prefabUpdated = true;
                            Undo.RecordObject(textMeshProUGUI, "Font Replacement");
                        }

                        textMeshProUGUI.font = newTMP_FontAssetForProject;
                    }

                if (prefabUpdated) {
                    EditorUtility.SetDirty(prefab);
                    AssetDatabase.SaveAssets();
                }
            }

            Debug.Log("TMP Font replaced in prefabs.");
        }

        //------------------------------------------------------------

        private void ReplaceLegacyFontsInProject() {
            // Ensure that a new font is selected
            if (newFontForProject == null) {
                Debug.LogError("No font selected!");
                return;
            }

            // Get all Text objects in all prefabs
            var guids = AssetDatabase.FindAssets("t:Prefab");
            var allPrefabs = new List<GameObject>();
            foreach (var guid in guids) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                allPrefabs.Add(prefab);
            }

            foreach (var prefab in allPrefabs) {
                var prefabUpdated = false;

                var texts = prefab.GetComponentsInChildren<Text>(true);

                foreach (var text in texts)
                    if (text.font != newFontForProject) {
                        if (!prefabUpdated) {
                            prefabUpdated = true;
                            Undo.RecordObject(text, "Font Replacement");
                        }

                        text.font = newFontForProject;
                    }

                if (prefabUpdated) {
                    EditorUtility.SetDirty(prefab);
                    AssetDatabase.SaveAssets();
                }
            }

            Debug.Log("Legacy Text Fonts replaced in prefabs.");
        }

        private void CreateDivider() {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.Space();
        }
    }
}