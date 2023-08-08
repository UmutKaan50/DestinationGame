using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using y01cu;

public class PlayerCombat : MonoBehaviour
{
    private Player player;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;

    private Collider2D[] hitEnemies;

    private void Awake()
    {
        player.GetComponent<Player>();
    }

    public void GetTargetColliders(InputAction.CallbackContext callbackContext)
    {
        // Empty the array at the beginning
        hitEnemies = null;
        if (callbackContext.performed)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            for (int i = 0; i < hitEnemies.Length; i++)
            {
                Debug.Log("We hit + " + hitEnemies[i].name);
            }
        }
    }

    private void SendAttackDamageToTheEnemy()
    {
        Damage damage = new Damage
        {
            attackDamageAmount = player.GetFinalAttackDamageToSendToTheEnemy();
          
            magicDamageAmount = player.MagicDamage;
            origin = this.gameObject;
            pushForce = 0;
        };
        foreach (Collider2D targetCollider in hitEnemies)
        {
            targetCollider.SendMessage("RecieveDamage", dmg);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}