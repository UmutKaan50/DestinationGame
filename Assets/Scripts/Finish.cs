using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public void FinishGame() {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
