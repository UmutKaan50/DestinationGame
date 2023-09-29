//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using UnityEditor;
using UnityEngine;
using System.Reflection;


public class ToggleEnterPlayModeSettings {
    [MenuItem("Tools/Toggle Enter Play Mode Settings")]
    public static void ToggleSettings() {
        // Toggle the 'Enter Play Mode Options Enabled' setting
        EditorSettings.enterPlayModeOptionsEnabled = !EditorSettings.enterPlayModeOptionsEnabled;

        if (EditorSettings.enterPlayModeOptionsEnabled) {
            // Set specific Enter Play Mode options if needed
            EditorSettings.enterPlayModeOptions =
                EnterPlayModeOptions.DisableSceneReload | EnterPlayModeOptions.DisableDomainReload;
        }

        // Log the current state for verification
        bool isEnterPlayModeOptionsEnabled = EditorSettings.enterPlayModeOptionsEnabled;
        // Clear the console so that messages can be seen clearly
        ClearConsole();
        if (isEnterPlayModeOptionsEnabled) {
            Debug.Log(
                $"<color=green>Enter Play Mode Options is enabled.</color>");
        }
        else {
            Debug.Log(
                $"<color=red>Enter Play Mode Options is disabled.</color>");
        }
    }

    private static void ClearConsole() {
        Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        System.Type type = assembly.GetType("UnityEditor.LogEntries");
        MethodInfo method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}