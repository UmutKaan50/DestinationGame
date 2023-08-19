//
// copyright (c) y01cu. All rights reserved.
//

using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Random = System.Random;

namespace y01cu {
    /// <summary>
    /// FighterNew
    /// </summary>
    public class FighterNew : MonoBehaviour {
        public event Action OnRecieveDamage;

        protected string name;

        protected int level;

        protected float attackingDamage;
        protected float attackCooldown;
        protected float attackRange;

        protected float criticalAttackChance;
        protected float criticalAttackDamageMultiplier;

        protected float movementSpeed;

        protected float castingSpeed;
        protected float magicDamage;

        protected float armour;
        protected float magicResistance;

        protected float criticalMagicChance;
        protected float criticalMagicDamageMultiplier;

        protected float hitpoint;

        // A function in which we set all of the attribute values
        protected void SetInitialAttributeValues(float attackingDamage, float attackingSpeed, float attackRange,
            float criticalAttackChance, float criticalAttackDamageMultiplier, float movementSpeed, float castingSpeed,
            float magicDamage, float armour, float magicResistance, float criticalMagicChance,
            float criticalMagicDamageMultiplier, float hitpoint) {
            this.attackingDamage = attackingDamage;
            this.attackCooldown = attackingSpeed;
            this.attackRange = attackRange;
            this.criticalAttackChance = criticalAttackChance;
            this.criticalAttackDamageMultiplier = criticalAttackDamageMultiplier;
            this.movementSpeed = movementSpeed;
            this.castingSpeed = castingSpeed;
            this.magicDamage = magicDamage;
            this.armour = armour;
            this.magicResistance = magicResistance;
            this.criticalMagicChance = criticalMagicChance;
            this.criticalMagicDamageMultiplier = criticalMagicDamageMultiplier;
            this.hitpoint = hitpoint;
        }

        // protected void SetAttributeValues(float attacking)

        protected void RecieveDamage(Damage damage) {
            Debug.Log("Previous health of " + gameObject.name + " was " + hitpoint + ".");
            Debug.Log(gameObject.name + " has recieved " + damage.attackDamageAmount + " attack damage and " +
                      damage.magicDamageAmount + " magic damage.");
            float comingAttackDamageLoweredByArmour = damage.attackDamageAmount - armour;
            bool isComingAttackDamageZero = comingAttackDamageLoweredByArmour <= 0;
            if (isComingAttackDamageZero) {
                int minimumDamage = 1;
                comingAttackDamageLoweredByArmour = minimumDamage;
            }

            float comingMagicDamageLoweredByMagicResistance = damage.magicDamageAmount - magicResistance;
            bool isComingMagicDamageZero = comingMagicDamageLoweredByMagicResistance <= 0;
            if (isComingMagicDamageZero) {
                int minimumDamage = 1;
                comingMagicDamageLoweredByMagicResistance = minimumDamage;
            }

            float totalComingDamageLoweredByDefences =
                comingAttackDamageLoweredByArmour + comingMagicDamageLoweredByMagicResistance;
            hitpoint -= totalComingDamageLoweredByDefences;
            Debug.Log("Current health of " + gameObject.name + " is " + hitpoint + ".");

            OnRecieveDamage?.Invoke();
        }

        protected virtual float GetFinalAttackDamage() {
            float finalAttackDamage = attackingDamage;
            if (IsCritical()) {
                float criticalAttackDamage = attackingDamage * criticalAttackDamageMultiplier;
                finalAttackDamage = criticalAttackDamage;
            }

            return finalAttackDamage;
        }

        protected virtual float GetFinalMagicDamage() {
            float finalMagicDamage = magicDamage;
            if (IsCritical()) {
                float criticalMagicDamage = magicDamage * criticalMagicDamageMultiplier;
                finalMagicDamage = criticalMagicDamage;
            }

            return finalMagicDamage;
        }

        bool IsCritical() {
            Random randomNumberGenerator = new Random();
            int randomNumber = randomNumberGenerator.Next(1, 101);
            bool isCritical = randomNumber == 1;
            return isCritical;
        }

        public float GetHitpoint() {
            return hitpoint;
        }

        public float GetAttackCooldown() {
            return attackCooldown;
        }
    }
}