//
// copyright (c) y01cu. All rights reserved.
//

using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Random = System.Random;

namespace y01cu
{
    /// <summary>
    /// FighterNew
    /// </summary>
    public class FighterNew : MonoBehaviour
    {
        private string name;

        public float AttackingDamage { get; protected set; }
        public float AttackingSpeed { get; protected set; }
        public float AttackRange { get; protected set; }

        public float CriticalAttackChance { get; protected set; }
        public float CriticalAttackDamageMultiplier { get; protected set; }

        public float MovementSpeed { get; protected set; }

        public float CastingSpeed { get; protected set; }
        public float MagicDamage { get; protected set; }

        public float Armour { get; protected set; }
        public float MagicResistance { get; protected set; }

        public float CriticalMagicChance { get; protected set; }
        public float CriticalMagicDamageMultiplier { get; protected set; }

        public float Hitpoint { get; protected set; }

        protected virtual void RecieveDamage(Damage damage)
        {
            float comingAttackDamageLoweredByArmour = damage.attackDamageAmount - armour;
            bool isComingAttackDamageZero = comingAttackDamageLoweredByArmour <= 0;
            if (isComingAttackDamageZero)
            {
                int minimumDamage = 1;
                comingAttackDamageLoweredByArmour = minimumDamage;
            }

            float comingMagicDamageLoweredByMagicResistance = damage.magicDamageAmount - magicResistance;
            bool isComingMagicDamageZero = comingMagicDamageLoweredByMagicResistance <= 0;
            if (isComingMagicDamageZero)
            {
                int minimumDamage = 1;
                comingMagicDamageLoweredByMagicResistance = minimumDamage;
            }

            float totalComingDamageLoweredByDefences =
                comingAttackDamageLoweredByArmour + comingMagicDamageLoweredByMagicResistance;
            hitpoint -= totalComingDamageLoweredByDefences;
        }

        protected virtual float GetFinalAttackDamage()
        {
            float finalAttackDamage = AttackingDamage;
            if (IsCritical())
            {
                float criticalAttackDamage = AttackingDamage * criticalAttackDamageMultiplier;
                finalAttackDamage = criticalAttackDamage;
            }

            return finalAttackDamage;
        }

        protected virtual float Cast()
        {
            float finalMagicDamage = magicDamage;
            if (IsCritical())
            {
                float criticalMagicDamage = magicDamage * criticalMagicDamageMultiplier;
                finalMagicDamage = criticalMagicDamage;
            }

            return finalMagicDamage;
        }

        bool IsCritical()
        {
            Random randomNumberGenerator = new Random();
            int randomNumber = randomNumberGenerator.Next(1, 101);
            bool isCritical = randomNumber == 1;
            return isCritical;
        }
    }
}