//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System;

namespace Destination.Player {
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class PlayerInputSystem : MonoBehaviour {
        private Player player;
        private PlayerCombat playerCombat;
        private Player_Unarmed playerUnarmed;
        private PlayerAnimations playerAnimations;
        private PlayerScriptsManager playerScriptsManager;
        private PlayerMovement playerMovement;
        private static PlayerInputActions playerInputActions; // Should it be static?
        private bool playerIsAlive;

        private Player_WithSword playerWithSword;

        public PlayerInputActions GetPlayerInputActions() {
            return playerInputActions;
        }

        private void FixedUpdate() {
            playerIsAlive = player.GetHitpoint() > 0;
            // TODO: Make sure the hitpoint is updated one, not the max one.
            if (playerIsAlive) {
                // Handling movement
                Vector2 movementInputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
                float speed = 1.2f;
                playerMovement.rigidbody2D.velocity =
                    new Vector2(movementInputVector.x * speed, movementInputVector.y * speed);
                playerMovement.UpdatePlayerMovement(movementInputVector);
            }
        }

        private void Awake() {
            player = GetComponent<Player>();

            playerCombat = GetComponent<PlayerCombat>();
            playerAnimations = GetComponent<PlayerAnimations>();
            playerScriptsManager = GetComponent<PlayerScriptsManager>();
            playerMovement = GetComponent<PlayerMovement>();
            playerUnarmed = GetComponent<Player_Unarmed>();

            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            playerInputActions.Player.Attack.performed += CallPlayerAttackActionFunctions;
        }

        private float lastAttackTime;

        private void CallPlayerAttackActionFunctions(InputAction.CallbackContext context) {
            if (playerIsAlive) {
                // Calculate the time elapsed since the last attack
                float elapsedTimeSinceLastAttack = Time.time - lastAttackTime;


                bool isAttackOnCooldown = elapsedTimeSinceLastAttack < player.GetAttackCooldown();
                if (isAttackOnCooldown) {
                    return;
                }

                // Update the timestamp of the last attack
                lastAttackTime = Time.time;

                // TODO: Make it flexible so that it can be used for other weapons as well. And don't forget to update animations based on attack phase.
                // playerCombat.GetTargetColliders(context);
                // playerCombat.SendAttackDamageToTheEnemy();

                // playerAnimations.TriggerAttack(context); needs to be called before GetTargetColliders and SendAttackDamageToTheEnemy methods. In order to play attack animation without any errors.

                playerAnimations.TriggerAttack(context);
                playerUnarmed.GetTargetColliders(context);
                // TODO: Add a function that enables and updates the enemy ui which shows the enemy's health.

                playerUnarmed.SendAttackDamageToTheEnemyIfHit();

                playerScriptsManager.CallMethodsInOrder(context);
            }
        }
    }
}