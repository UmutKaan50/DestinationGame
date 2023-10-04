public class NPCTextPerson : Collideable {
    //public string message;

    private float cooldown = 4.0f;

    private float lastShout = -4.0f; // Instant reply at the beginning.
    //protected override void OnCollide(Collider2D coll) {
    //    if (coll.name == "Player") {
    //        if (Time.time - lastShout > cooldown) {
    //            lastShout = Time.time;
    //            GameManager.instance.ShowText(message, 25, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
    //        }

    //    }
    //}
}