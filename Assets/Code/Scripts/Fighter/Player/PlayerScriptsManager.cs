//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Destination.Player {
    public class PlayerScriptsManager : MonoBehaviour {
        private int currentWeapon = 0;


        public void CallMethodsInOrder(InputAction.CallbackContext context) {
            // if (context.performed)
            // {
            //     if (currentWeapon % 3 == 0)
            //     {
            //         MakePlayerWithSword();
            //     }
            //     else if (currentWeapon % 3 == 1)
            //     {
            //         MakePlayerWithGun();
            //     }
            //     else if (currentWeapon % 3 == 2)
            //     {
            //         MakePlayerUnarmed();
            //     }
            // }
        }

        public void MakePlayerWithSword() {
            currentWeapon++;
            Destroy(gameObject.GetComponent<Player_Unarmed>());
            Destroy(gameObject.GetComponent<Player_WithGun>());
            gameObject.AddComponent<Player_WithSword>();
        }

        public void MakePlayerWithGun() {
            currentWeapon++;
            Destroy(gameObject.GetComponent<Player_Unarmed>());
            Destroy(gameObject.GetComponent<Player_WithSword>());
            gameObject.AddComponent<Player_WithGun>();
        }

        public void MakePlayerUnarmed() {
            currentWeapon++;
            Destroy(gameObject.GetComponent<Player_WithSword>());
            Destroy(gameObject.GetComponent<Player_WithGun>());
            gameObject.AddComponent<Player_Unarmed>();
        }
    }
}