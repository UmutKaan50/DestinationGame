using UnityEngine;
using UnityEngine.InputSystem;
using y01cu;

public class PlayerCombat : MonoBehaviour, IDifferentAttackPhases {
    private Player player;
    protected int attackHitCounter = 0;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;

    protected Collider2D[] hitEnemies;

    private void Awake() {
        player = GetComponent<Player>();
    }

    public void GetTargetColliders(InputAction.CallbackContext callbackContext) {
        // Empty the array at the beginning
        hitEnemies = null;
        if (callbackContext.performed) {
            hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            for (int i = 0; i < hitEnemies.Length; i++) {
                Debug.Log("We hit + " + hitEnemies[i].name);
            }
        }
    }

    // The approach taken is that when attack damage is sent there's no magic damage and vice versa.

    // private int hitAttackCounter = 0;
    public virtual void SendAttackDamageToTheEnemy() {
        Damage damage = new Damage {
            attackDamageAmount = player.GetFinalAttackDamageToSendToTheEnemy(),
            magicDamageAmount = 0f,
            origin = gameObject.transform.position,
            pushForce = 0
        };
        Debug.Log("Player damage values: " + damage.attackDamageAmount + " " + damage.magicDamageAmount + " " +
                  damage.origin + " " + damage.pushForce + " ");
        bool didAttackHitSomething = hitEnemies != null;
        if (didAttackHitSomething) {
            // hitAttackCounter++;
            foreach (Collider2D targetCollider in hitEnemies) {
                targetCollider.SendMessage("RecieveDamage", damage);
            }
        }
    }

    public void SendMagicDamageToTheEnemy() {
        Damage damage = new Damage {
            attackDamageAmount = 0f,
            magicDamageAmount = player.GetFinalMagicDamageToSendToTheEnemy(),
            origin = gameObject.transform.position,
            pushForce = 0
        };
        if (hitEnemies != null) {
            foreach (Collider2D targetCollider in hitEnemies) {
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
}