using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Fighter {
    protected override void Death() {
        Destroy(gameObject);
    }
}



//private void OnCollisionEnter2D(Collision2D collision) {

//    Debug.Log("CRATE");
//    if (collision.gameObject.name == "weapon") {
//        if (hitpoint > 1) {
//            SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.crateHit);
//        } else {
//            SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.crateBreak);
//        }
//    }


//}


