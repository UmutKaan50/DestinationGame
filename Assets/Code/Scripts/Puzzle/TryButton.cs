//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using UnityEngine;

namespace Code.Scripts {
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
}
