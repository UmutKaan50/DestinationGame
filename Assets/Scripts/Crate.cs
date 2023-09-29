using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {
    private AudioSource audioSource;
    [SerializeField] private AudioClip crateHit;

    [SerializeField] private int hitpoint;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    // protected override void GetDestroyed() {
    //     Destroy(gameObject);
    // }

    // private void OnCollisionEnter2D(Collision2D collision) {
    //     Debug.Log("CRATE");
    //     if (collision.gameObject.name == "weapon") {
    //         if (hitpoint > 1) {
    //             audioSource.PlayOneShot(crateHit);
    //         }
    //         else {
    //             audioSource.PlayOneShot(crateHit);
    //         }
    //     }
    //     else {
    //         Debug.Log("Hi");
    //     }
    // }

    /// <summary>
    /// We're forgetting about the upcoming damage since these are just crates. We'll apply our own hitpoint logic here.
    /// </summary>
    /// <param name="dmg"></param>
    ///
    private bool canBeAffected = true;

    private void RecieveDamage(Damage dmg) {
        if (canBeAffected) {
            canBeAffected = false;
            Debug.Log("Coroutine is called.");
            audioSource.PlayOneShot(crateHit);
            StartCoroutine(RecieveDamageAfterDelay());
        }
    }

    private IEnumerator RecieveDamageAfterDelay() {
        float damageDealingDelay = crateHit.length;

        yield return new WaitForSeconds(damageDealingDelay);

        hitpoint -= 1;
        if (hitpoint == 0) {
            Destroy(gameObject);
        }

        canBeAffected = true;
    }
}