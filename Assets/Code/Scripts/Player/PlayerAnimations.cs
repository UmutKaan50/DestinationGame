using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerReadyToAttack = true;

    // [SerializeField] private float attackCooldownTime = 2f;
    // private float nextAttackFireTime = 0f;
    // private static int numberOfAttackPresses = 0;
    // private float lastAttackPressedTime = 0;
    // private float maxComboDelayTime = 1f;

    private int attackClickCounter;

    private string animatorIntegerName = "AttackPhase";


    void Start()
    {
        animator = GetComponent<Animator>();
        attackClickCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f &&
        //     animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_0"))
        // {
        //     animator.SetBool("Attack_0", false);
        // }
        //
        // if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f &&
        //     animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_1"))
        // {
        //     animator.SetBool("Attack_1", false);
        //     numberOfAttackPresses = 0;
        // }
        //
        // if (Time.time - lastAttackPressedTime > maxComboDelayTime)
        // {
        //     numberOfAttackPresses = 0;
        // }

        //
        // if (Time.time > nextAttackFireTime )
        // {
        //     
        // }
    }

    /// <summary>
    /// Called when the player presses the attack button.
    /// </summary>
    public void TriggerAttack(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            attackClickCounter++;
            Debug.Log("Attack count: " + attackClickCounter + " " + callbackContext.phase);


            if (attackClickCounter == 1)
            {
                animator.SetInteger(animatorIntegerName, 1);
                Invoke("ResetAttackPhase", 0.5f);
            }

            if (attackClickCounter == 2)
            {
                animator.SetInteger(animatorIntegerName, 2);
                Invoke("ResetAttackPhase", 0.1f);
            }
        }
        // lastAttackPressedTime = Time.time;
        // numberOfAttackPresses++;
        //
        // if (numberOfAttackPresses == 1)
        // {
        //     animator.SetBool("Attack_0", true);
        // }
        //
        // numberOfAttackPresses = Mathf.Clamp(numberOfAttackPresses, 0, 3);
        //
        // if (numberOfAttackPresses >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7 &&
        //     animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_0"))
        // {
        //     animator.SetBool("Attack_0", false);
        //     animator.SetBool("Attack_1", true);
        // }

        // if (isPlayerReadyToAttack)
        // {
        //     animator.SetTrigger("Attack");
        //     isPlayerReadyToAttack = false;
        //     Invoke("RefreshAttackAbility", 1f);
        // }
    }

    private void ResetAttackPhase()
    {
        attackClickCounter = 0;
        animator.SetInteger(animatorIntegerName, 0);
    }

    private void RefreshAttackAbility()
    {
        isPlayerReadyToAttack = true;
    }

    private void ChangeAnimatorLayer(int nextLayerIndex, int previousLayerIndex)
    {
        animator.SetLayerWeight(previousLayerIndex, 0);
        animator.SetLayerWeight(nextLayerIndex, 1);
    }

    public void ExecuteAnimationEvent()
    {
        Debug.Log("Animation end check.");
    }
}