using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : Mover {
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;
    public bool hasKey = false;
    private bool isMoving;
    private bool isVerticalMoving;
    private bool isHorizontalMoving;

    public Joystick joystick;

    private float horizontalMove;
    private float verticalMove;

    public AudioSource audioSource;
    public Portal portal;
    public void SetHasKey() {
        hasKey = true;
        portal.SetCanTransfer();
    }
    protected override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        // DontDestroyOnLoad(gameObject);
    }
    protected override void RecieveDamage(Damage dmg) {
        if (!isAlive)
            return;

        base.RecieveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }
    protected override void Death() {
        isAlive = false;
        GameManager.instance.deathMenuAnim.SetTrigger("Show");

    }
    public void WalkSound() {
        SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.walking);
    }
    private void FixedUpdate() {
        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");

        if(joystick.Horizontal >= .2f) {
            horizontalMove = xSpeed;

        } else if (joystick.Horizontal <= -.2f) {
            horizontalMove = -xSpeed;
        } else {
            horizontalMove = 0f;
            isHorizontalMoving = false;
        }

        if (joystick.Vertical >= .2f) {
            verticalMove = ySpeed;
        } else if (joystick.Vertical <= -.2f) {
            verticalMove = -ySpeed;
        } else {
            verticalMove = 0f;
            isVerticalMoving = false;
        }


        if (verticalMove == 0 && horizontalMove == 0) {
            isMoving = false;
            Debug.Log("Stopped.");
            audioSource.Stop();
        } else {
            if(!audioSource.isPlaying)
            audioSource.Play();
            Debug.Log("Walking.");
            //SoundManager.instance.audioSource.clip = SoundManager.instance.walking;
            //SoundManager.instance.audioSource.loop = true;
            //SoundManager.instance.audioSource.Play();


        }

        if (isAlive)
        UpdateMotor(new Vector3(horizontalMove, verticalMove, 0));
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
        GameManager.instance.ShowText("+" + healingAmount.ToString() + " hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.OnHitpointChange();
    }

    public void Respawn() {
        Heal(maxHitpoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }

}
