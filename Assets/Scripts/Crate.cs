using System.Collections;
using UnityEngine;

public class Crate : MonoBehaviour {
    [SerializeField] private AudioClip crateHit;
    [SerializeField] private AudioClip crateBreak;

    [SerializeField] private int hitpoint;
    private AudioSource audioSource;

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
    ///     We're forgetting about the upcoming damage since these are just crates. We'll apply our own hitpoint logic here.
    /// </summary>
    /// <param name="dmg"></param>
    private bool canBeAffected = true;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void RecieveDamage(Damage dmg) {
        if (canBeAffected) {
            canBeAffected = false;
            Debug.Log("Coroutine is called.");
            StartCoroutine(RecieveDamageAfterDelay());
        }
    }


    private IEnumerator RecieveDamageAfterDelay() {
        var damageDealingDelay = crateHit.length;


        hitpoint -= 1;
        if (hitpoint == 0) {
            audioSource.PlayOneShot(crateBreak);
            Destroy(gameObject, damageDealingDelay);
        }

        audioSource.PlayOneShot(crateHit);
        yield return new WaitForSeconds(damageDealingDelay);

        canBeAffected = true;
    }
}