//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using UnityEngine;

namespace y01cu {
    public class ActiOrDeactivation : MonoBehaviour {
        public void DeactivateItself() {
            gameObject.SetActive(false);
        }

        public void ActivateItself() {
            gameObject.SetActive(true);
        }
    }
}