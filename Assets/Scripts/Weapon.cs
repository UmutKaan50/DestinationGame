using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using y01cu;
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

                CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
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
            // The reason why we've used return below needs to be understood.
            if (coll.name == "Player")
                return;

            Damage dmg = new Damage {
                damageAmount = damagePoint[weaponlevel],
                origin = transform.position,
                pushForce = pushForce[weaponlevel]
            };

            coll.SendMessage("RecieveDamage", dmg);

            if (coll.name.Contains("SmallEnemy")) {
                Debug.Log("Small Enemy hit!");
            }
        }
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