using System;
using System.Globalization;
using Code.Scripts;

namespace Destination.Fighters {
    using System.Collections;
    using System.Collections.Generic;
    using Destination.DamagePopups;
    using UnityEngine;

    [Obsolete("This script is not used anymore. It's replaced with NewFighter.cs script.")]
    public class Fighter : MonoBehaviour {
        [SerializeField] private string name;

        // Public fields:
        public float hitpoint = 10;
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
                hitpoint -= dmg.attackDamageAmount;
                pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

                bool isCriticalHit = Random.Range(0, 100) < 30;

                bool isLeftSide;
                bool isKillingBlow;

                if (hitpoint <= 0) {
                    isKillingBlow = true;
                }
                else {
                    isKillingBlow = false;
                }

                if (dmg.origin.x > gameObject.transform.position.x) {
                    // Damagepopup will go left.
                    isLeftSide = true;
                }
                else {
                    // Damagepopup will go right.
                    isLeftSide = false;
                }

                string nameOfTheGameObject = gameObject.name;

                // bool isTargetPlayer = GetComponent<Fighter>().name == "Player";
                bool targetIsPlayer = nameOfTheGameObject == "Player";
                bool targetIsSkeleton = nameOfTheGameObject == "SmallEnemy";
                bool targetIsBoss = nameOfTheGameObject == "Boss";

                // Here I've shown texts instead of playing sounds that tells which sounds are required:

                Vector3 textPositionOffset = transform.position + new Vector3(.2f, .2f, 0);

                string playerHitText = "player";
                string skeletonHitText = "skeleton";
                string bossHitText = "boss";
                string otherHitText = "other";

                if (targetIsPlayer) {
                    DamagePopup.Create(transform.position, dmg.attackDamageAmount.ToString(), "FF2B00",
                        isLeftSide); // FF2B00 is red.
                    // DamagePopup.Create(textPositionOffset, playerHitText, "18DBB9", isLeftSide);
                }
                else if (targetIsSkeleton) {
                    DamagePopup.Create(transform.position, dmg.attackDamageAmount.ToString(), "FFC500",
                        isLeftSide); // FFC500 is yellow.
                    // DamagePopup.Create(textPositionOffset, skeletonHitText, "18DBB9", isLeftSide);
                }
                else if (targetIsBoss) {
                    DamagePopup.Create(transform.position, dmg.attackDamageAmount.ToString(), "FF2B00", isLeftSide);
                    // DamagePopup.Create(textPositionOffset, bossHitText, "18DBB9", isLeftSide);
                }
                else {
                    // Instead of using a text like "other", I can use empty text instead.

                    DamagePopup.Create(transform.position, "", "FFC500", isLeftSide);

                    SoundController.instance.audioSource.PlayOneShot(SoundController.instance.crateBreak);

                    // DamagePopup.Create(textPositionOffset, "", "18DBB9", isLeftSide);
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
}