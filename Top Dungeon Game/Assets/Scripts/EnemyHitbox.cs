using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collideable
{
    public int damage;
    public float pushForce;

    protected override void OnCollide(Collider2D coll) {
        if (coll.name == "Player") {
            // Create a new damage object, before sending it to the player:
            Damage dmg = new Damage {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("RecieveDamage", dmg);
        }
    }
}
