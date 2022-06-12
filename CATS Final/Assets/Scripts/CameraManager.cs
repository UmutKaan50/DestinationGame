using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{


    public GameObject player;

    // Focal pointe neden rigidbody koyduk emin olamad?m.

    // Update is called once per frame
    void Update()
    {

        transform.position = player.transform.position; // Move focal point with player

    }
}
