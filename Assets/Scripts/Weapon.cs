using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
// using y01cu;
using EZCameraShake;

public class Weapon : Collideable {
    // Damage struct:
    private int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7 };
    private float[] pushForce = { 2.0f, 2.3f, 2.6f, 3.0f, 3.5f, 4.1f, 4.8f };

    // Upgrade:
    public int weaponlevel = 0;
    private SpriteRenderer spriteRenderer;

    // Swing:
    private Animator anim;
    private float cooldown = 0.7f;
    private float lastSwing;

    private int collisionCount;

    private AudioSource audioSource;

    private void Awake() {
        // Instead, you could've made spriteRenderer public and assign it in the inspector.
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Start() {
        // We keep base.Start() because we need to assign our boxcollider:
        base.Start();
        // spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    [SerializeField] private float shakeMagnitude;
    [SerializeField] private float shakeRoughness;
    [SerializeField] private float shakeFadeInTime;
    [SerializeField] private float shakeFadeOutTime;

    protected override void Update() {
        base.Update();
        // Player attack state:
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Time.time - lastSwing > cooldown) {
                // Assigning last swing moment:
                lastSwing = Time.time;

                Swing();
                audioSource.PlayOneShot(SoundManager.instance.swordHurl);
                CameraShaker.Instance.ShakeOnce(shakeMagnitude, shakeRoughness, shakeFadeInTime, shakeFadeOutTime);
            }
        }
    }

    //public void BasicAttack() {
    //    if (Time.time - lastSwing > cooldown) {
    //        lastSwing = Time.time;
    //        Swing();
    //    }
    //}

    private bool canPlaySound = false;
    private bool isAttackParticlesJustPlayed = false;
    [SerializeField] private ParticleSystem attackParticles;

    protected override void OnCollide(Collider2D coll) {
        if (coll.tag == "Fighter") {
            // The reason why we've used return below needs to be understood.
            if (coll.name == "Player")
                return;

            Damage dmg = new Damage {
                damageAmount = damagePoint[weaponlevel],
                origin = transform.position,
                pushForce = pushForce[weaponlevel]
            };

            // Debug.Log("dmg: " + dmg);
            // Debug.Log("col: " + coll);
            if (!isAttackParticlesJustPlayed) {
                attackParticles.Play();
                isAttackParticlesJustPlayed = true;
                StartCoroutine(ReactivateAttackParticles());
            }

            coll.SendMessage("RecieveDamage", dmg);
        }
    }

    private IEnumerator ReactivateAttackParticles() {
        float reactivationDelay = 0.4f;
        yield return new WaitForSeconds(reactivationDelay);

        isAttackParticlesJustPlayed = false;
    }

    private void Swing() {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon() {
        weaponlevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponlevel];
        // Change the stats:
    }

    public void SetWeaponLevel(int level) {
        weaponlevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponlevel];
    }
}