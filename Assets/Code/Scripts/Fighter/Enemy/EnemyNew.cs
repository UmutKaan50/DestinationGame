using Code.Scripts;
using Destination.Fighters;
using Destination.Player;
using UnityEngine;

namespace Destination.Enemies {
    public class EnemyNew : FighterNew {
        [SerializeField] private AudioSource audioSource;

        private void SetProperties() {
            name = "Enemy";

            attackingDamage = 5;
            attackCooldown = 1;
            attackRange = 1;

            criticalAttackChance = 0;
            criticalAttackDamageMultiplier = 1;

            movementSpeed = 1;

            castingSpeed = 1;
            magicDamage = 0;

            armour = 0;
            magicResistance = 0;

            criticalMagicChance = 0;
            criticalMagicDamageMultiplier = 1;

            hitpoint = 50;
        }

        private void Start() {
            SetProperties();
        }

        protected override void RecieveDamage(Damage damage) {
            // Handle sound effects
            bool isDamageSourcePlayer = damage.fighterType == FighterType.Player;
            bool isPlayerAttackNormal = isDamageSourcePlayer && damage.attackType == PlayerCombat.AttackType.Normal;
            bool isPlayerAttackPowerful = isDamageSourcePlayer && damage.attackType == PlayerCombat.AttackType.Powerful;
            if (isPlayerAttackNormal) {
                audioSource.PlayOneShot(SoundController.instance.jabSFX);
            }
            else if (isPlayerAttackPowerful) {
                audioSource.PlayOneShot(SoundController.instance.crossSFX);
            }

            base.RecieveDamage(damage);
        }
    }
}