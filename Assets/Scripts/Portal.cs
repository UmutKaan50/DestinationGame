using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collideable {
    public string[] sceneNames;
    public bool canTransfer;
    public GameObject finishScreen;
    public string message;

    [SerializeField] private Animator animator;

    private readonly float cooldown = 4.0f;
    private float lastShout = -4.0f; // Instant reply at the beginning.

    public void SetCanTransfer() {
        canTransfer = true;
    }

    protected override void OnCollide(Collider2D coll) {
        if (coll.name == "Player") {
            if (canTransfer) {
                // Teleport the player:
                SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.teleport);
                GameManager.instance.SaveState();
                var sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
                SceneManager.LoadScene(sceneName);
            }

            if (Time.time - lastShout > cooldown) {
                lastShout = Time.time;

                animator.SetTrigger("show");

                //GameManager.instance.ShowText(message, 25, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
            }
        }
    }
}