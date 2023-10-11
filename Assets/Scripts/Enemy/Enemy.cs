using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : Mover {
    // Experience:
    public int xpValue = 1;

    // Logic:
    [FormerlySerializedAs("triggerLenght")] [SerializeField]
    private float triggerLength = 1;

    [FormerlySerializedAs("chaseLenght")] [SerializeField]
    private float chaseLength = 5;

    private Player player;

    // Hitbox
    public ContactFilter2D filter;
    private readonly Collider2D[] hits = new Collider2D[10];

    // private AudioSource audioSource;
    private Animator animator;
    private bool collidingWithPlayer;
    private BoxCollider2D hitbox;

    private bool isEnemyChasingPlayer;
    private Vector3 startingPosition;

    protected override void Awake() {
        base.Awake();
    }

    protected override void Start() {
        base.Start();
        isEnemyReadyToChase = false;
        // playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        float timeDelay = 0.6f;
        Invoke("FindPlayerAndMakeEnemyReadyToChase", timeDelay);
    }

    private void FindPlayerAndMakeEnemyReadyToChase() {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player != null) {
            isEnemyReadyToChase = true;
        }
    }

    private bool isEnemyReadyToChase = false;

    private void FixedUpdate() {
        if (isEnemyReadyToChase) {
            ChasePlayerInRange();
        }
    }

    private void ChasePlayerInRange() {
        // Is the player in range?
        var isPlayerInChaseRange = Vector3.Distance(player.transform.position, startingPosition) < chaseLength;
        var isPlayerInTriggerRange = Vector3.Distance(player.transform.position, startingPosition) < triggerLength;
        var isPlayerAlive = player.GetIsPlayerAlive();
        if (isPlayerInChaseRange && isPlayerAlive) {
            if (isPlayerInTriggerRange) isEnemyChasingPlayer = true;

            if (isEnemyChasingPlayer) {
                if (!collidingWithPlayer)
                    // Chase the player:
                    UpdateMotor((player.transform.position - transform.position).normalized);
                else
                    // Stop chasing the player: (?)
                    UpdateMotor(startingPosition - transform.position);
            }
        }
        else {
            UpdateMotor(startingPosition - transform.position);
            isEnemyChasingPlayer = false;
        }

        // Chech for overlaps:
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (var i = 0; i < hits.Length; i++) {
            if (hits[i] == null) continue;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player") collidingWithPlayer = true;

            // The array isn't cleaned up so we do it ourselves:
            hits[i] = null;
        }
    }

    protected override void RecieveDamage(Damage dmg) {
        base.RecieveDamage(dmg);
        if (hitpoint > 0) animator.SetTrigger("Hurt");
    }

    protected override void GetDestroyed() {
        if (hitpoint <= 0) {
            hitpoint = 0;
            StartCoroutine(Die());
            // I should figure out how does rows below work even if Destroy(GameObject) command is given.
            GameManager.instance.GrantXp(xpValue);
            GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40,
                1.0f);
        }
    }

    public void GetDestroyedByPuzzleSolution() {
        hitpoint = 0;
        StartCoroutine(Die());
    }

    protected IEnumerator Die() {
        float animationLength = 1;
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(animationLength);
        Destroy(gameObject);
    }
}