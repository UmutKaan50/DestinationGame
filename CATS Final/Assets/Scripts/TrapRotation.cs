using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRotation : MonoBehaviour {


    // Update is called once per frame
    void Update() {
        transform.Rotate(Vector3.forward);
        transform.Rotate(Vector3.right);
    }
}
