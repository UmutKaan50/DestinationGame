using System;
using UnityEngine;
using UnityEngine.InputSystem;
using y01cu;

public class PlayerAnimations : MonoBehaviour {
    private Animator animator;
    private bool isPlayerReadyToAttack = true;
    private Player player;

    private int attackClickCounter;
    private string animatorIntegerName = "AttackPhase";
    private float lastAttackTime;

    private void Awake() {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    void Start() {
        attackClickCounter = 0;
    }

    /// <summary>
    /// Called when the player presses the attack button.
    /// </summary>
    public void TriggerAttack(InputAction.CallbackContext callbackContext) {
        if (callbackContext.performed) {
            
            
            attackClickCounter++;
            Debug.Log("Attack count: " + attackClickCounter + " " + callbackContext.phase);

            if (attackClickCounter == 1) {
                animator.SetInteger(animatorIntegerName, attackClickCounter);
            }

            if (attackClickCounter == 2) {
                animator.SetInteger(animatorIntegerName, attackClickCounter);
            }

            if (attackClickCounter == 3) {
                animator.SetInteger(animatorIntegerName, attackClickCounter);
                attackClickCounter = 0;
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