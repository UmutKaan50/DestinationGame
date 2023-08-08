using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script will hold the logic for drawing the debug console and handling debug commands
/// </summary>
public class DebugManager : MonoBehaviour
{
    bool isConsoleShown;
    bool isHelpShown;

    string inputCommand;

    public static DebugCommand FLOW;
    public static DebugCommand HELP;

    public List<object> commandList;

    public void OnToggle(InputValue inputValue)
    {
        isConsoleShown = !isConsoleShown;
    }

    void Awake()
    {
        FLOW = new DebugCommand("flow", "Flows the game by going to next NPC", "flow", () => { });

        HELP = new DebugCommand("help", "Shows a list of commands.", "help", () => { });

        commandList = new List<object>
        {
            FLOW,
            HELP,
        };
    }

    public void OnReturn(InputValue inputValue)
    {
        if (isConsoleShown)
        {
            HandleInput();
            inputCommand = "";
        }
    }

    Vector2 scroll;

    void OnGUI()
    {
        if (!isConsoleShown)
        {
            return;
        }

        float yValue = 0f;
        if (isHelpShown)
        {
            GUI.Box(new Rect(0, yValue, Screen.width, 100), "");
            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);
            scroll = GUI.BeginScrollView(new Rect(0, yValue + 5, Screen.width, 90), scroll, viewport);
            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;
                string label = $"{command.CommandFormat} - {command.CommandDescription}";
                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();

            yValue += 100;
        }

        GUI.Box(new Rect(0, yValue, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        inputCommand = GUI.TextField(new Rect(10, yValue + 5, Screen.width - 20, 20), inputCommand);
    }

    void HandleInput()
    {
        string[] properties = inputCommand.Split(' ');

        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if (inputCommand.Contains(commandBase.CommandId))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    // Cast to this type and invoke the command
                    (commandList[i] as DebugCommand).InvokeCommand();
                }
                else if (commandList[i] as DebugCommand<int> != null)
                {
                    (commandList[i] as DebugCommand<int>).InvokeCommand(int.Parse(properties[1]));
                }
            }
        }
    }
}