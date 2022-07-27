using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

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

    private void Awake() {
        // Instead, you could've made spriteRenderer public and assign it in the inspector.
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Time.time - lastSwing > cooldown) {
                // Assigning last swing moment:
                lastSwing = Time.time;

                Swing();


            }
        }

    }

    //public void BasicAttack() {
    //    if (Time.time - lastSwing > cooldown) {
    //        lastSwing = Time.time;
    //        Swing();
    //    }
    //}



    protected override void OnCollide(Collider2D coll) {
        if (coll.tag == "Fighter") {
            if (coll.name == "Player")
                return;

            // Debug.Log("Weapon level: " + weaponlevel);
            Damage dmg = new Damage {
                damageAmount = damagePoint[weaponlevel],
                origin = transform.position,
                pushForce = pushForce[weaponlevel]
            };

            coll.SendMessage("RecieveDamage", dmg);


            // SoundManager.instance.isHittingEnemy = true;

            int attackDamage = damagePoint[weaponlevel];
            int targetHealth = coll.GetComponent<Fighter>().hitpoint;

            bool killingBlow = attackDamage > targetHealth;



            bool isSkeleton = coll.gameObject.GetComponent<Fighter>().name == "Skeleton";
            bool isCrate = coll.gameObject.GetComponent<Fighter>().name == "Crate";
            //if (isSkeleton) {
                
            //    if (killingBlow) {
            //        SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.skeletonDeath);
            //    }
            //} else if (isCrate) {
                
            //    if (killingBlow) {
            //        SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.crateBreak);

            //    }
            //} else { // Air:
            //    SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.attackingAir);
            //}

            FunctionTimer.Create(() => Debug.Log("Let's wait 0.8 seconds."), .8f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {

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
