using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    private void Update() {
        transform.Rotate(Vector3.forward);
    }
}
