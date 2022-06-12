using UnityEngine;

public class Portal : Collideable {
    public string[] sceneNames;
    public bool canTransfer = false;
    public GameObject finishScreen;
    public string message;

    private float cooldown = 4.0f;
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
                string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

            }
        }

        if (Time.time - lastShout > cooldown) {
            lastShout = Time.time;
            GameManager.instance.ShowText(message, 25, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
        }
    }
}
