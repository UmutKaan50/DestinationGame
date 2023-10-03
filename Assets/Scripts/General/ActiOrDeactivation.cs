//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System;
using System.Collections;
using System.Collections.Generic;
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