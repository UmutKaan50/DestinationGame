using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip walking;
    public AudioClip attackingAir;
    public static SoundManager instance;
    private void Awake() {
        if (SoundManager.instance != null) {
            return;
        }
        instance = this;
        audioSource = GetComponent<AudioSource>();

    }
    public void AttackingAir() {
        audioSource.PlayOneShot(attackingAir);
    }
}
