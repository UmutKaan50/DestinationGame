using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {
    public float[] fireballSpeed = { 2.5f, -2.5f };
    public float distance = 0.25f;
    public Transform[] fireballs;

    public AudioSource audioSource;
    public AudioClip deathSound;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update() {
        for (int i = 0; i < fireballs.Length; i++) {
            fireballs[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireballSpeed[i]) * distance, Mathf.Sin(Time.time * fireballSpeed[i]) * distance, 0);

        }        
    }

    protected override void Death() {
        audioSource.PlayOneShot(deathSound);
        Destroy(gameObject);
        // I should figure out how does rows below work even if Destroy(GameObject) command is given.
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }
}
