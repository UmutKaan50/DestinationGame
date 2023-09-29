using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    // Instead of calling the audio clips from here why don't I add them as serializefields to the required places? That looks smarter.
    
    public AudioSource audioSource;
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
    public AudioClip keyPickUp;
    public AudioClip failedAttempt;
    // Temp audioclips for checking the result of calculation:
    public AudioClip tempMathsTrue;
    public AudioClip tempMathsFalse;
    public AudioClip levelUp;
    public AudioClip crateHit;
    public AudioClip crateBreak;
    public AudioClip swordHurl;
    public AudioClip playerHurt;
    public AudioClip skeletonHurt;
    public AudioClip skeletonDeath;

    public static SoundManager instance;

    public bool isHittingEnemy = false;
    public bool isHittingWall = false;
    private void Awake() {
        if (SoundManager.instance != null) {
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
