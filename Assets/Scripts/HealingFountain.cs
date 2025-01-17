using UnityEngine;

public class HealingFountain : Collideable {
    public int healingAmount = 1;
    private readonly float healCooldown = 1.0f;
    private float lastHeal;

    protected override void OnCollide(Collider2D coll) {
        if (coll.name != "Player")
            return;

        if (Time.time - lastHeal > healCooldown) {
            lastHeal = Time.time;
            GameManager.instance.player.Heal(healingAmount);
            SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.healing, 0.1f);
        }
    }
}