using UnityEngine;

public class TryButton : MonoBehaviour {
    public static TryButton instance;
    public bool isHidden = true;

    private void Awake() {
        if (instance != null) return;
        instance = this;
    }
}