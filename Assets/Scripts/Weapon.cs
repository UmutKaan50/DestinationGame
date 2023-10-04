using System.Collections;
using EZCameraShake;
using UnityEngine;

// using y01cu;

public class Weapon : Collideable {
    // Upgrade:
    public int weaponlevel;

    [SerializeField] private Player player;

    [SerializeField] private float shakeMagnitude;
    [SerializeField] private float shakeRoughness;
    [SerializeField] private float shakeFadeInTime;
    [SerializeField] private float shakeFadeOutTime;
    [SerializeField] private ParticleSystem attackParticles;

    private readonly float cooldown = 0.7f;

    // Damage struct:
    private readonly int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7 };
    private readonly float[] pushForce = { 2.0f, 2.3f, 2.6f, 3.0f, 3.5f, 4.1f, 4.8f };

    // Swing:
    private Animator anim;

    private AudioSource audioSource;

    //public void BasicAttack() {
    //    if (Time.time - lastSwing > cooldown) {
    //        lastSwing = Time.time;
    //        Swing();
    //    }
    //}

    private bool canPlaySound = false;

    private int collisionCount;
    private bool isAttackParticlesJustPlayed;
    private float lastSwing;
    private SpriteRenderer spriteRenderer;

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

    protected override void Update() {
        base.Update();
        // Player attack state:
        if (Input.GetKeyDown(KeyCode.Space) && player.GetIsPlayerReady())
            if (Time.time - lastSwing > cooldown) {
                // Assigning last swing moment:
                lastSwing = Time.time;

                Swing();
                audioSource.PlayOneShot(SoundManager.instance.swordHurl);
                CameraShaker.Instance.ShakeOnce(shakeMagnitude, shakeRoughness, shakeFadeInTime, shakeFadeOutTime);
            }
    }

    protected override void OnCollide(Collider2D coll) {
        if (coll.tag == "Fighter") {
            // The reason why we've used return below needs to be understood.
            if (coll.name == "Player")
                return;

            var dmg = new Damage {
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
        var reactivationDelay = 0.4f;
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