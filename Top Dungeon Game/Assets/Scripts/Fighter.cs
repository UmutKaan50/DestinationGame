using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {
    // Public fields:
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;

    // Immunity:
    protected float immuneTime = 0.7f;
    protected float lastImmune;

    // Push:
    protected Vector3 pushDirection;

    // All fighters can recieve damage and die.

    protected virtual void RecieveDamage(Damage dmg) {
        // Make player immune, not enemy:
        if (Time.time - lastImmune > immuneTime) {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            if (gameObject.name == "Player") {
                GameManager.instance.ShowText(dmg.damageAmount.ToString(), 18, Color.red, transform.position, Vector3.zero, 0.2f);
            } else {
                GameManager.instance.ShowText(dmg.damageAmount.ToString(), 22, Color.yellow, transform.position, Vector3.zero, 0.2f);
            }

            if (hitpoint <= 0) {
                hitpoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death() {

    }

}
