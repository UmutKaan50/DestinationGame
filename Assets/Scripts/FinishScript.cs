using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : Collideable
{
    public GameObject finishCanvas;
    protected override void OnCollide(Collider2D coll) {

        if (coll.name == "Player") {
            finishCanvas.SetActive(true);
        }
    }
}
