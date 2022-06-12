using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public GameObject UIController;

    public static float speed = 80;
    private Rigidbody enemyRb;
    public GameObject playerGoal;

    private void Awake() {
        UIController = GameObject.Find("UIController");
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);

        Debug.Log("Enemy speed: " + speed);

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            // Kendi kalesine gol olursa:

            Destroy(gameObject);

            // 1 azalt:
            UIController.GetComponent<UIContollerX>().goal++;


        } 
        else if (other.gameObject.name == "Player Goal")
        {

            // Bizim kalemize gol olursa:

            Destroy(gameObject);
            UIController.GetComponent<UIContollerX>().health--;
        }

    }

}
