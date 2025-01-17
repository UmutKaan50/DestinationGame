using UnityEngine;

public class CameraMotor : MonoBehaviour {
    public float boundX = 0.15f;
    public float boundY = 0.05f;

    private Transform lookAt;

    private void Start() {
        lookAt = GameObject.Find("Player").transform;
    }

    private void LateUpdate() {
        var delta = Vector3.zero;
        // This is to check if we're inside the bounds in the X axis.
        var deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX) {
            if (transform.position.x < lookAt.position.x)
                delta.x = deltaX - boundX;
            else
                delta.x = deltaX + boundX;
        }

        var deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY) {
            if (transform.position.y < lookAt.position.y)
                delta.y = deltaY - boundY;
            else
                delta.y = deltaY + boundY;
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}