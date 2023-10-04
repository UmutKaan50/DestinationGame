using UnityEngine;

public class Fighter : MonoBehaviour {
    [SerializeField] private string name;

    // Public fields:
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;

    [SerializeField] protected AudioClip hurtAudioClip;

    // All fighters can recieve damage and die.

    protected AudioSource audioSource;

    // Immunity:
    protected float immuneTime = 0.7f;
    protected float lastImmune;

    // Push:
    protected Vector3 pushDirection;


    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void RecieveDamage(Damage dmg) {
        // Make player immune, not enemy:
        if (Time.time - lastImmune > immuneTime) {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;


            var isCriticalHit = Random.Range(0, 100) < 30;

            bool isLeftSide;
            bool isKillingBlow;

            if (hitpoint <= 0)
                isKillingBlow = true;
            else
                isKillingBlow = false;

            if (dmg.origin.x > gameObject.transform.position.x)
                // Damagepopup will go left.
                isLeftSide = true;
            else
                // Damagepopup will go right.
                isLeftSide = false;

            // bool isTargetPlayer = GetComponent<Fighter>().name == "Player";
            var isTargetPlayer = gameObject.name == "Player";
            var isTargetSkeleton = gameObject.name == "SmallEnemy";
            var isTargetBoss = gameObject.name == "Boss";

            // Here I've shown texts instead of playing sounds that tells which sounds are required:

            var textPositionOffset = transform.position + new Vector3(.2f, .2f, 0);

            var playerHitText = "player";
            var skeletonHitText = "skeleton";
            var bossHitText = "boss";
            var otherHitText = "other";

            if (isTargetPlayer)
                DamagePopup.Create(transform.position, dmg.damageAmount.ToString(), "FF2B00",
                    isLeftSide); // FF2B00 is red.
            // DamagePopup.Create(textPositionOffset, playerHitText, "18DBB9", isLeftSide);
            else if (isTargetSkeleton)
                DamagePopup.Create(transform.position, dmg.damageAmount.ToString(), "FFC500",
                    isLeftSide); // FFC500 is yellow.
            // DamagePopup.Create(textPositionOffset, skeletonHitText, "18DBB9", isLeftSide);
            else if (isTargetBoss)
                DamagePopup.Create(transform.position, dmg.damageAmount.ToString(), "FF2B00", isLeftSide);
            // DamagePopup.Create(textPositionOffset, bossHitText, "18DBB9", isLeftSide);
            else
                DamagePopup.Create(transform.position, dmg.damageAmount.ToString(), "FFC500", isLeftSide);
            // DamagePopup.Create(textPositionOffset, otherHitText, "18DBB9", isLeftSide);
            Debug.Log($"{gameObject} is hurt.");

            PlayHurtAudioClip();
            GetDestroyed();
        }
    }

    protected virtual void GetDestroyed() {
        if (hitpoint <= 0) {
            hitpoint = 0;
            Destroy(gameObject);
        }
    }

    protected virtual void PlayHurtAudioClip() {
        audioSource.PlayOneShot(hurtAudioClip);
    }
}