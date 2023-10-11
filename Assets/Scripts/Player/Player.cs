using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[Obsolete(
    "It's ok for now to save time, but later on entrance animation must be check before letting player to move in player movement method.")]
public class Player : Mover {
    private bool isPlayerReady;

    private BoxCollider2D playerBoxCollider2D;

    private SpriteRenderer playerSpriteRenderer;

    protected override void Start() {
        base.Start();

        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        // StartCoroutine(SetPlayerAsReadyAfterSomeTime());

        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        // DontDestroyOnLoad(gameObject);

        animator = GetComponent<Animator>();
        playerBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() {
        isPlayerReady = isAlive && !IsAnInterruptionOccuring && isEntranceAnimationEnded;

        PlayerMovement();
        // PlayerHit();
        UpdatePlayerSprite();
    }

    private void PlayerMovement() {
        var horizontalMove = Input.GetAxisRaw("Horizontal");
        var verticalMove = Input.GetAxisRaw("Vertical");

        if (isPlayerReady) {
            UpdateMotor(new Vector3(horizontalMove, verticalMove, 0));

            // Stopped:
            var isPlayerStopped = verticalMove == 0 && horizontalMove == 0;

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

    public void RecieveDamageFromPuzzle() {
        Damage puzzleDamage = new Damage() {
            damageAmount = 2,
            origin = transform.position,
            pushForce = 0.2f
        };
        RecieveDamage(puzzleDamage);
    }

    public bool GetIsPlayerAlive() {
        return isAlive;
    }

    public float detectionWidth = 5f;
    public float detectionHeight = 5f;
    [SerializeField] private LayerMask detectionLayer;

    private void UpdatePlayerSprite() {
        // Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, Vector2.zero)
        //
        // playerSpriteRenderer.sortingLayerName = "Top";

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position,
            new Vector2(detectionWidth, detectionHeight), 0, detectionLayer);
        foreach (Collider2D hitCollider in hitColliders) {
            float distance = Vector2.Distance(transform.position, hitCollider.transform.position);
            if (distance <= Mathf.Max(detectionWidth, detectionHeight)) // replace with your "close enough" distance
            {
                if (transform.position.y < hitCollider.transform.position.y) {
                    spriteRenderer.sortingOrder = 1; // or any other value to set it in front
                }
                else {
                    spriteRenderer.sortingOrder = -1; // or any other value to set it behind
                }
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(detectionWidth, detectionHeight, 0));
    }


    private void PlayerHit() {
        var hit = Physics2D.Raycast(transform.position, transform.forward, rayCastDistance);

        if (hit.collider != null)
            Debug.DrawLine(transform.position, hit.point, Color.red);
        else
            Debug.DrawLine(transform.position, transform.position + transform.right * rayCastDistance, Color.green);
    }

    public bool GetIsPlayerReady() {
        return isPlayerReady;
    }

    public bool IsAnInterruptionOccuring { get; set; }


    [Obsolete("Will be replaced by signals in timeline.")]
    private IEnumerator SetPlayerAsReadyAfterSomeTime() {
        var initalDelay = 0.5f;
        yield return new WaitForSeconds(initalDelay);
        // float animationLength = entranceAnimator.GetCurrentAnimatorClipInfo(0).Length;
        var entranceAnimationLength = 12f;
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
        if (hitpoint > 0) animator.SetTrigger("Hurt");
    }

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
        for (var i = 0; i < level; i++)
            OnLevelUp();
    }

    public void Heal(int healingAmount) {
        if (hitpoint == maxHitpoint)
            return;

        hitpoint += healingAmount;

        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;
        GameManager.instance.ShowText("+" + healingAmount + " hp", 25, Color.green, transform.position,
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

    #region Variables

    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;
    public bool hasKey;
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

    private bool isEntranceAnimationEnded;

    #endregion
}