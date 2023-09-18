//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using UnityEngine;

namespace Code.Scripts {
    public class HealingFountain : Collideable {
        public int healingAmount = 1;
        private float healCooldown = 1.0f;
        private float lastHeal;
        protected override void OnCollide(Collider2D coll) {
            if (coll.name != "Player")
                return;

            if (Time.time - lastHeal > healCooldown) {
                lastHeal = Time.time;
                GameManager.instance.player.Heal(healingAmount);
                SoundController.instance.audioSource.PlayOneShot(SoundController.instance.healing, 0.1f);

            }
        }


    }
}
