using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : Mover {
    #region Variables

    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;
    public bool hasKey = false;
    private bool isMoving;
    private bool isVerticalMoving;
    private bool isHorizontalMoving;
    private Animator animator;
    // public Joystick joystick;

    private float horizontalMove;
    private float verticalMove;
    [SerializeField] private AudioClip deathSound;

    // public AudioSource audioSource;
    [SerializeField] private AudioSource additionalAudioSource;

    public Portal portal;

    [SerializeField] private float rayCastDistance;

    [SerializeField] private Animator weaponAnimator;

    [SerializeField] private Animator entranceAnimator;

    private bool isEntranceAnimationEnded = false;

    #endregion

    private void FixedUpdate() {
        PlayerMovement();
        PlayerHit();
    }

    public bool GetIsPlayerAlive() {
        return isAlive;
    }

    private void PlayerHit() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, rayCastDistance);

        if (hit.collider != null) {
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        else {
            Debug.DrawLine(transform.position, transform.position + transform.right * rayCastDistance, Color.green);
        }
    }

    private bool isPlayerReady;

    public bool GetIsPlayerReady() {
        return isPlayerReady;
    }

    private void PlayerMovement() {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        isPlayerReady = isAlive && isEntranceAnimationEnded;
        if (isPlayerReady) {
            UpdateMotor(new Vector3(horizontalMove, verticalMove, 0));

            // Stopped:
            bool isPlayerStopped = verticalMove == 0 && horizontalMove == 0;

            if (isPlayerStopped) {
                isMoving = false;
                audioSource.Stop();
                animator.SetBool("isPlayerWalking", false);
            }
            else { // Walking:
                if (!audioSource.isPlaying)
                    audioSource.Play();

                animator.SetBool("isPlayerWalking", true);
            }

            #region JoystickControls

            // With joystick:
            //if(joystick.Horizontal >= .2f) {
            //    horizontalMove = xSpeed;

            //} else if (joystick.Horizontal <= -.2f) {
            //    horizontalMove = -xSpeed;
            //} else {
            //    horizontalMove = 0f;
            //    isHorizontalMoving = false;
            //}

            //if (joystick.Vertical >= .2f) {
            //    verticalMove = ySpeed;
            //} else if (joystick.Vertical <= -.2f) {
            //    verticalMove = -ySpeed;
            //} else {
            //    verticalMove = 0f;
            //    isVerticalMoving = false;
            //}

            #endregion
        }
    }

    protected override void Start() {
        base.Start();
        
        // StartCoroutine(SetPlayerAsReadyAfterSomeTime());
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        // DontDestroyOnLoad(gameObject);

        animator = GetComponent<Animator>();
        playerBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    [Obsolete("Will be replaced by signals in timeline.")]
    private IEnumerator SetPlayerAsReadyAfterSomeTime() {
        float initalDelay = 0.5f;
        yield return new WaitForSeconds(initalDelay);
        // float animationLength = entranceAnimator.GetCurrentAnimatorClipInfo(0).Length;
        float entranceAnimationLength = 12f;
        yield return new WaitForSeconds(entranceAnimationLength);
        isEntranceAnimationEnded = true;
    }
    
    public void SetAnimationEnded() {
        isEntranceAnimationEnded = true;
    }

    public void SetHasKey() {
        hasKey = true;
        portal.SetCanTransfer();
    }

    protected override void RecieveDamage(Damage dmg) {
        if (!isAlive)
            return;

        base.RecieveDamage(dmg);
        GameManager.instance.OnHitpointChange();
        if (hitpoint > 0) {
            animator.SetTrigger("Hurt");
        }
    }

    private BoxCollider2D playerBoxCollider2D;

    protected override void GetDestroyed() {
        if (hitpoint <= 0) {
            hitpoint = 0;
            isAlive = false;
            playerBoxCollider2D.enabled = false;
            additionalAudioSource.PlayOneShot(deathSound);
            StartCoroutine(PlayDeathAnimation());
        }
    }

    private IEnumerator PlayDeathAnimation() {
        float animationLength = 1 * 4;
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(animationLength);
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
    }

    public void SwapSprite(int skinId) {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUp() {
        maxHitpoint++;
        hitpoint = maxHitpoint;
    }

    public void SetLevel(int level) {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }

    public void Heal(int healingAmount) {
        if (hitpoint == maxHitpoint)
            return;

        hitpoint += healingAmount;

        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;
        GameManager.instance.ShowText("+" + healingAmount.ToString() + " hp", 25, Color.green, transform.position,
            Vector3.up * 30, 1.0f);
        GameManager.instance.OnHitpointChange();
    }

    public void Respawn() {
        Heal(maxHitpoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }

    public void PlayWeaponFallAnimation() {
        weaponAnimator.SetTrigger("Fall");
    }

    protected override void PlayHurtAudioClip() {
        additionalAudioSource.PlayOneShot(hurtAudioClip);
    }
}