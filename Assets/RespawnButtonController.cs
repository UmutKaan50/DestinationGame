//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace y01cu {
    public class RespawnButtonController : MonoBehaviour {
        void Start() {
            respawnButton = GetComponent<Button>();
            InvokeRepeating("FindGameManagerAndAssignRequiredFunctionality", 1, 1);
        }

        private Button respawnButton;
        private GameManager gameManager;

        private void FindGameManagerAndAssignRequiredFunctionality() {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            respawnButton.onClick.AddListener(() => { gameManager.Respawn(); });
        }
    }
}