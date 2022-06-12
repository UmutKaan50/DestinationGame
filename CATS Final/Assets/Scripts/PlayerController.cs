using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TextController textController;
    public GameObject focalPoint;
    public float movementSpeed;
    public float jumpSpeed;

    public Rigidbody playerRigidbody;
    public float verticalMovement;
    public float horizontalMovement;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        verticalMovement = Input.GetAxis("Vertical");
        horizontalMovement = Input.GetAxis("Horizontal");

        // Focal pointte kald?m.

        // Left/Right movement
        playerRigidbody.AddForce(focalPoint.transform.forward * verticalMovement * movementSpeed);
        playerRigidbody.AddForce(focalPoint.transform.right * horizontalMovement * movementSpeed);

        if (Input.GetKey(KeyCode.Space)) {
            // Jumping
            playerRigidbody.AddForce(Vector3.up * jumpSpeed);

        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Trap") {
            // life--
            textController.life--;
            Destroy(other.gameObject);

        }
        if (other.tag == "Coin") {
            // score++
            textController.score++;
            Destroy(other.gameObject);
        }

    }
}
