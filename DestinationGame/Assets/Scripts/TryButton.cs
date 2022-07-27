using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryButton : MonoBehaviour {
    public static TryButton instance;
    private void Awake() {
        if (TryButton.instance != null) {
            return;
        }
        instance = this;
    }
    public bool isHidden = true;

}
