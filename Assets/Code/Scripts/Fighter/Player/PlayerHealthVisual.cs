//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System;
using System.Collections;
using System.Collections.Generic;
using Destination.Player;
using UnityEngine;
using UnityEngine.UI;

namespace y01cu {
    public class PlayerHealthVisual : MonoBehaviour {
        private Player player;
        [SerializeField] private Slider playerHealthSlider;

        private void Awake() {
            player = GetComponent<Player>();
        }

        private void Start() {
            playerHealthSlider.maxValue = player.GetHitpoint();
            playerHealthSlider.value = player.GetHitpoint();

            player.OnRecieveDamage += UpdateSliderValue;
        }

        private void UpdateSliderValue() {
            playerHealthSlider.value = player.GetHitpoint();
            Debug.Log("Here's the hitpoint from our window:" + player.GetHitpoint());
        }
    }
}