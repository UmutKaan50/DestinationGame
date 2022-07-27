using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {

    [SerializeField]
    private string name;
    // Public fields:
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;

    // Immunity:
    protected float immuneTime = 0.7f;
    protected float lastImmune;

    // Push:
    protected Vector3 pushDirection;

    // All fighters can recieve damage and die.

    protected virtual void RecieveDamage(Damage dmg) {
        // Make player immune, not enemy:
        if (Time.time - lastImmune > immuneTime) {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
        }

        bool isCriticalHit = Random.Range(0, 100) < 30;

        bool isLeftSide;

        if (dmg.origin.x > gameObject.transform.position.x) {
            // Damagepopup will go left.
            isLeftSide = true;

        } else {
            // Damagepopup will go right.
            isLeftSide = false;
        }
        bool isPlayer = GetComponent<Fighter>().name == "Player";
        

        if (isPlayer) {
            DamagePopup.Create(transform.position, dmg.damageAmount, "FF2B00", isLeftSide);
            // Previously: GameManager.instance.ShowText(dmg.damageAmount.ToString(), 18, Color.red, transform.position, Vector3.zero, 0.2f);
        } 
        //else if () {
        //    SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.attackingEnemy);
        //    DamagePopup.Create(transform.position, dmg.damageAmount, "FFC500", isLeftSide);
        //    //if () {

        //    //}
        //} else if () {
        //    SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.crateHit);
        //    DamagePopup.Create(transform.position, dmg.damageAmount, "FFC500", isLeftSide);
        //}

        //Previously: GameManager.instance.ShowText(dmg.damageAmount.ToString(), 22, Color.yellow, transform.position, Vector3.zero, 0.2f);

        // Checking death condition:

        if (hitpoint <= 0) {
            hitpoint = 0;
            Death();
        }

    }



    protected virtual void Death() {



    }
}