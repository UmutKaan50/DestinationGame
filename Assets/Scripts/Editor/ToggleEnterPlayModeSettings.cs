//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System.Reflection;
using UnityEditor;
using UnityEngine;

public class ToggleEnterPlayModeSettings {
    [MenuItem("Tools/Toggle Enter Play Mode Settings")]
    public static void ToggleSettings() {
        // Toggle the 'Enter Play Mode Options Enabled' setting
        EditorSettings.enterPlayModeOptionsEnabled = !EditorSettings.enterPlayModeOptionsEnabled;

        if (EditorSettings.enterPlayModeOptionsEnabled)
            // Set specific Enter Play Mode options if needed
            EditorSettings.enterPlayModeOptions =
                EnterPlayModeOptions.DisableSceneReload | EnterPlayModeOptions.DisableDomainReload;

        // Log the current state for verification
        var isEnterPlayModeOptionsEnabled = EditorSettings.enterPlayModeOptionsEnabled;
        // Clear the console so that messages can be seen clearly
        ClearConsole();
        if (isEnterPlayModeOptionsEnabled)
            Debug.Log(
                "<color=green>Enter Play Mode Options is enabled.</color>");
        else
            Debug.Log(
                "<color=red>Enter Play Mode Options is disabled.</color>");
    }

    private static void ClearConsole() {
        var assembly = Assembly.GetAssembly(typeof(Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}