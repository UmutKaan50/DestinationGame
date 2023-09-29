using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover {
    // Experience:
    public int xpValue = 1;

    // Logic:
    public float triggerLenght = 1;
    public float chaseLenght = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    // Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    // private AudioSource audioSource;
    private Animator animator;

    protected override void Start() {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        ChasePlayerInRange();
    }

    private void ChasePlayerInRange() {
        // Is the player in range?
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght) {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght) {
                chasing = true;
            }

            if (chasing) {
                if (!collidingWithPlayer) {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
                else {
                    UpdateMotor(startingPosition - transform.position);
                }
            }
        }
        else {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        // Chech for overlaps:
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i] == null) {
                continue;
            }

            if (hits[i].tag == "Fighter" && hits[i].name == "Player") {
                collidingWithPlayer = true;
            }

            // The array isn't cleaned up so we do it ourselves:
            hits[i] = null;
        }
    }

    protected override void RecieveDamage(Damage dmg) {
        base.RecieveDamage(dmg);
        if (hitpoint > 0) {
            animator.SetTrigger("Hurt");
        }
    }

    protected override void GetDestroyed() {
        if (hitpoint <= 0) {
            hitpoint = 0;
            StartCoroutine(PlayDeathAnimation());
            // I should figure out how does rows below work even if Destroy(GameObject) command is given.
            GameManager.instance.GrantXp(xpValue);
            GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40,
                1.0f);
        }
    }

    private IEnumerator PlayDeathAnimation() {
        float animationLength = 1;
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(animationLength);
        Destroy(gameObject);
    }
}