//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using Code.Scripts;

namespace Destination.Player {
    using System;
    using Destination.Fighters;
    using UnityEngine;

    /// <summary>
    /// Player
    /// </summary>
    public class Player : FighterNew {
        private PlayerCombat playerCombat;
        private PlayerAnimations playerAnimations;

        private void Awake() {
            playerCombat = GetComponent<PlayerCombat>();
        }

        private void Start() {
            AssignPlayerInitialAttributeValues();
        }

        private void AssignPlayerInitialAttributeValues() {
            // Assigned random values for now
            float initialAttackDamage = 2f;
            float initialAttackCooldown = 1.5f;
            float initialAttackRange = 2f;
            float initialCriticalAttackChance = 0.01f;
            float initialCriticalAttackDamageMultiplier = 1.5f;
            float initialMovementSpeed = 20f;
            float initialCastingSpeed = 0.3f;
            float initialMagicDamage = 3f;
            float initialArmour = 0f;
            float initialMagicResistance = 0f;
            float initialCriticalMagicChance = 0.01f;
            float initialCriticalMagicDamageMultiplier = 1.5f;
            float initialHitpoint = 10f;

            SetInitialAttributeValues(initialAttackDamage, initialAttackCooldown, initialAttackRange,
                initialCriticalAttackChance, initialCriticalAttackDamageMultiplier, initialMovementSpeed,
                initialCastingSpeed, initialMagicDamage, initialArmour, initialMagicResistance,
                initialCriticalMagicChance, initialCriticalMagicDamageMultiplier, initialHitpoint);
        }

        public float GetFinalAttackDamageToSendToTheEnemy() {
            return GetFinalAttackDamage();
        }

        public float GetFinalMagicDamageToSendToTheEnemy() {
            return GetFinalMagicDamage();
        }

        protected override void RecieveDamage(Damage damage) {
            base.RecieveDamage(damage);
            
        }

        public void RecieveDamageTemporarily() {
            RecieveDamage(new Damage { attackDamageAmount = 1f, magicDamageAmount = 1f });
        }

        // TODO: Create a similar method for magic damage but distinguish whether player used magic or not
    }
}