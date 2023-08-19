using UnityEngine;
using y01cu;

public class Player_Unarmed : PlayerCombat {
    private Player player;

    private void Awake() {
        player = GetComponent<Player>();
    }

    public override void SendAttackDamageToTheEnemy() {
        
        
        bool isNormalAttack = attackHitCounter == 0 || attackHitCounter == 1 || attackHitCounter == 2;
        bool isPowerfulAttack = attackHitCounter == 3;
        if (isNormalAttack) {
            AttackNormally();
        }
        else if (isPowerfulAttack) {
            AttackPowerfully();
        }

        attackHitCounter++;
        attackHitCounter = attackHitCounter % 4;
    }

    public override void AttackNormally() {
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

    private float powerfulAttackDamageMultiplier = 2f;
    private int logCounter = 0;

    public override void AttackPowerfully() {
        Damage damage = new Damage {
            attackDamageAmount = player.GetFinalAttackDamageToSendToTheEnemy() * powerfulAttackDamageMultiplier,
            magicDamageAmount = 0f,
            origin = gameObject.transform.position,
            pushForce = 0
        };
        Debug.Log(logCounter + "- Player damage values: " + damage.attackDamageAmount + " " + damage.magicDamageAmount +
                  " " + damage.origin + " " + damage.pushForce + " ");
        bool didAttackHitSomething = hitEnemies != null;
        if (didAttackHitSomething) {
            // hitAttackCounter++;
            foreach (Collider2D targetCollider in hitEnemies) {
                targetCollider.SendMessage("RecieveDamage", damage);
            }
        }
    }
}