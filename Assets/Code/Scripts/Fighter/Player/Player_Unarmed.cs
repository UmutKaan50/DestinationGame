//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using Code.Scripts;

namespace Destination.Player {
    using UnityEngine;

    public class Player_Unarmed : PlayerCombat {
        private Player player;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private ParticleSystem attackEffect;
        [SerializeField] private Transform playerAttackPointTransform;

        [SerializeField] private Animator attackEffectAnimator;
        // [SerializeField] private AudioClip jabSFX;
        // [SerializeField] private AudioClip crossSFX;
        // [SerializeField] private AudioClip punchInTheAirSFX;

        private void Awake() {
            player = GetComponent<Player>();
        }

        private int attackHitCounterLoggerIndex = 0;

        public override void SendAttackDamageToTheEnemyIfHit() {
            if (DidAttackHitATarget()) {
                // attackEffectAnimator.SetTrigger("Hit");

                bool isNormalAttack = attackHitCounter == 0 || attackHitCounter == 1;
                bool isPowerfulAttack = attackHitCounter == 2;
                if (isNormalAttack) {
                    AttackNormally();
                    PlayNormalAttackSound();
                }
                else if (isPowerfulAttack) {
                    AttackPowerfully();
                    PlayPowerfulAttackSound();
                }

                CreateAttackParticleProperly();

                UpdateAttackHitCounter();
            }
            else {
                float volumeScaleForPunchInTheAirSFX = 0.35f;
                audioSource.PlayOneShot(SoundController.instance.punchInTheAirSFX, volumeScaleForPunchInTheAirSFX);
            }
        }

        private void PlayPowerfulAttackSound() {
            audioSource.PlayOneShot(SoundController.instance.crossSFX);
        }

        private void CreateAttackParticleProperly() {
            // Transform attackEffectTransform = attackEffect.transform;
            // attackEffectTransform.position = new Vector3(0, 0, 0);
            // attackEffectTransform.position = hitTargets[0].transform.position;
            // float hitTargetsXPosition = attackEffectTransform.position.x;
            // float hitTargetsZPosition = attackEffectTransform.position.z;

            // attackEffectTransform.position = new Vector3(hitTargetsXPosition, playerAttackPointTransform.position.y,
            // hitTargetsZPosition);

            Instantiate(attackEffect, playerAttackPointTransform.position, Quaternion.identity);
        }

        private int normalAttackLogCounter = 0;

        /// <summary>
        /// Normal attack function with an audioclip inside.
        /// </summary>
        public override void AttackNormally() {
            Damage damage = new Damage {
                attackType = AttackType.Normal,
                attackDamageAmount = player.GetFinalAttackDamageToSendToTheEnemy(),
                magicDamageAmount = 0f,
                origin = gameObject.transform.position,
                pushForce = 0
            };
            // Play normal attack sound
            // audioSource.PlayOneShot(SoundController.instance.jabSFX);
            Debug.Log("Normal attack hit to: " + hitTargets[0]);
            Debug.Log("Music is played from normal attack " + normalAttackLogCounter++);

            foreach (Collider2D targetCollider in hitTargets) {
                targetCollider.SendMessage("RecieveDamage", damage);
            }
        }

        private void PlayNormalAttackSound() {
            audioSource.PlayOneShot(SoundController.instance.jabSFX);
        }

        private float powerfulAttackDamageMultiplier = 2f;
        private int powerfulAttackLogCounter = 0;

        /// <summary>
        /// Powerful attack function with an audioclip inside.
        /// </summary>
        public override void AttackPowerfully() {
            Damage damage = new Damage {
                attackType = AttackType.Powerful,
                attackDamageAmount = player.GetFinalAttackDamageToSendToTheEnemy() * powerfulAttackDamageMultiplier,
                magicDamageAmount = 0f,
                origin = gameObject.transform.position,
                pushForce = 0
            };
            // audioSource.PlayOneShot(SoundController.instance.crossSFX);
            Debug.Log("Powerful attack hit to: " + hitTargets[0]);
            Debug.Log("Music is played from powerful attack " + powerfulAttackLogCounter++);

            foreach (Collider2D targetCollider in hitTargets) {
                targetCollider.SendMessage("RecieveDamage", damage);
            }
        }
    }
}