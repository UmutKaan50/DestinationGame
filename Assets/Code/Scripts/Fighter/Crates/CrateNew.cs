//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System;
using Code.Scripts;

namespace y01cu {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CrateNew : MonoBehaviour {
        [SerializeField] private int hitpoint = 2;

        private AudioSource audioSource;

        private void Awake() {
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// This method lowers the hitpoint of the crate by 1.
        /// </summary>
        private void RecieveDamage() {
            hitpoint--;
            if (hitpoint > 0) {
                audioSource.PlayOneShot(SoundController.instance.crateHit);
            }
            else {
                float createBreakSFXLength = 0.4f;
                audioSource.PlayOneShot(SoundController.instance.crateBreak);
                Destroy(gameObject, createBreakSFXLength);
            }
        }
    }
}