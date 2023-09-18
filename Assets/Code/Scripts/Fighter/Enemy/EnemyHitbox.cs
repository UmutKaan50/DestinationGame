//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using Code.Scripts;
using UnityEngine;

namespace Destination.Enemies {
    public class EnemyHitbox : Collideable
    {
        public int damage;
        public float pushForce;

        protected override void OnCollide(Collider2D coll) {
            if (coll.name == "Player") {
                // Create a new damage object, before sending it to the player:
                Damage dmg = new Damage {
                    attackDamageAmount = damage,
                    origin = transform.position,
                    pushForce = pushForce
                };

                coll.SendMessage("RecieveDamage", dmg);
            }
        }
    }
}
