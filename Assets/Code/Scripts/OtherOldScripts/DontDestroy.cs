//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using UnityEngine;

namespace Code.Scripts {
    public class DontDestroy : MonoBehaviour {
        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }
    }
}
