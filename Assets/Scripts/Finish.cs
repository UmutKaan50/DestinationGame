using UnityEngine;

public class Finish : MonoBehaviour {
    public void FinishGame() {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}