//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//


namespace Destination.Player {
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class PlayerAnimations : MonoBehaviour {
        private Animator animator;
        private bool isPlayerReadyToAttack = true;
        private Player player;

        // private int attackClickCounter;
        private string animatorIntegerName = "AttackPhase";
        private float lastAttackTime;

        private void Awake() {
            player = GetComponent<Player>();
            animator = GetComponent<Animator>();
            player.OnRecieveDamage += FaceWithUpcomingDamage;
            // TODO: Add appropriate sound effects when player is hit.
        }

        private void FaceWithUpcomingDamage() {
            // TODO: Make sure player health is updated before this method is called. And we get the current health, not the max health.
            bool playerStillAlive = player.GetHitpoint() > 0;

            if (playerStillAlive) {
                animator.SetTrigger("Hurt");
            }
            else {
                animator.SetTrigger("Die");
            }
        }

        /// <summary>
        /// Called when the player presses the attack button.
        /// </summary>
        public void TriggerAttack(InputAction.CallbackContext callbackContext) {
            if (callbackContext.performed) {
                int attackPhase = PlayerCombat.GetAttackHitCounter() + 1;
                bool isAttackPhaseValid = attackPhase == 1 || attackPhase == 2 || attackPhase == 3;
                if (isAttackPhaseValid) {
                    animator.SetInteger(animatorIntegerName, attackPhase);
                }
            }
        }

        /// <summary>
        /// It's called at the end of player attack animations.
        /// </summary>
        public void ResetAttackPhase() {
            int resetingAttackPhaseIntValue = 0;
            animator.SetInteger(animatorIntegerName, resetingAttackPhaseIntValue);
        }

        private void RefreshAttackAbility() {
            isPlayerReadyToAttack = true;
        }

        private void ChangeAnimatorLayer(int nextLayerIndex, int previousLayerIndex) {
            animator.SetLayerWeight(previousLayerIndex, 0);
            animator.SetLayerWeight(nextLayerIndex, 1);
        }
    }
}