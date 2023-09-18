//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using Code.Scripts;

namespace Destination.Player {
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class PlayerCombat : MonoBehaviour, IDifferentAttackPhases {
        private Player player;
        // protected AttackType attackType;
        protected static int attackHitCounter = 0;

        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange = 0.5f;
        [SerializeField] private LayerMask enemyLayers;

        protected Collider2D[] hitTargets;

        private void Awake() {
            player = GetComponent<Player>();
        }

        public static int GetAttackHitCounter() {
            return attackHitCounter;
        }

        public static void UpdateAttackHitCounter() {
            attackHitCounter += 1;
            attackHitCounter %= 3;
        }

        public void GetTargetColliders(InputAction.CallbackContext callbackContext) {
            // Empty the array at the beginning
            hitTargets = null;
            if (callbackContext.performed) {
                hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                for (int i = 0; i < hitTargets.Length; i++) {
                    Debug.Log("We hit + " + hitTargets[i].name);
                }
            }
        }

        protected virtual bool DidAttackHitSomething() {
            bool didAttackHitSomething = hitTargets.Length > 0 && hitTargets[0] != null;
            return didAttackHitSomething;
        }

        // The approach taken is that when attack damage is sent there's no magic damage and vice versa.

        // private int hitAttackCounter = 0;
        // This function is called from Player_Unarmed.cs but it's fully updated there. I'm not sure if it necessary to fill inside of it here.
        public virtual void SendAttackDamageToTheEnemyIfHit() {
            // Damage damage = new Damage {
            //     attackDamageAmount = player.GetFinalAttackDamageToSendToTheEnemy(),
            //     magicDamageAmount = 0f,
            //     origin = gameObject.transform.position,
            //     pushForce = 0
            // };
            // Debug.Log("Player damage values: " + damage.attackDamageAmount + " " + damage.magicDamageAmount + " " +
            //           damage.origin + " " + damage.pushForce + " ");
            // bool didAttackHitSomething = hitTargets != null;
            // if (didAttackHitSomething) {
            //     // hitAttackCounter++;
            //     foreach (Collider2D targetCollider in hitTargets) {
            //         Debug.Log(targetCollider.gameObject.name);
            //         targetCollider.SendMessage("RecieveDamage", damage);
            //     }
            // }
        }

        public void SendMagicDamageToTheEnemy() {
            Damage damage = new Damage {
                attackDamageAmount = 0f,
                magicDamageAmount = player.GetFinalMagicDamageToSendToTheEnemy(),
                origin = gameObject.transform.position,
                pushForce = 0
            };
            if (hitTargets != null) {
                foreach (Collider2D targetCollider in hitTargets) {
                    targetCollider.SendMessage("RecieveDamage", damage);
                }
            }
        }

        private void OnDrawGizmosSelected() {
            if (attackPoint == null) {
                return;
            }

            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        public virtual void AttackNormally() {
        }

        public virtual void AttackPowerfully() {
        }

        public enum AttackType {
            Normal,
            Powerful
        }
    }
}