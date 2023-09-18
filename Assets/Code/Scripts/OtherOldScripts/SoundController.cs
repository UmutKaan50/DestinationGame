//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using UnityEngine;

namespace Code.Scripts {
    public class SoundController : MonoBehaviour {
        public AudioSource audioSource;
        public AudioClip jabSFX;
        public AudioClip crossSFX;
        public AudioClip teleport;
        public AudioClip walking;
        public AudioClip attackingAir;
        public AudioClip attackingEnemy;
        public AudioClip attackingWall;
        public AudioClip pickingUpMoney;
        public AudioClip buttonTap;
        public AudioClip softButtonTap;
        public AudioClip healing;
        public AudioClip chestClose;
        public AudioClip chestOpen;
        public AudioClip buySound;
        public AudioClip skeletonDeath;
        public AudioClip keyPickUp;
        public AudioClip failedAttempt;
        // Temp audioclips for checking the result of calculation:
        public AudioClip tempMathsTrue;
        public AudioClip tempMathsFalse;
        public AudioClip levelUp;
        public AudioClip crateHit;
        public AudioClip crateBreak;
        public AudioClip punchInTheAirSFX;

        public static SoundController instance;

        public bool isHittingEnemy = false;
        public bool isHittingWall = false;
        private void Awake() {
            if (SoundController.instance != null) {
                return;
            }
            instance = this;
            audioSource = GetComponent<AudioSource>();

        }

        //public void AttackingAirLogic() {
        //    if (isHittingEnemy) {
        //        audioSource.PlayOneShot(attackingEnemy);
        //        isHittingEnemy = false;
        //    } else {
        //        audioSource.PlayOneShot(attackingAir);
        //    }
        //}
    }
}
