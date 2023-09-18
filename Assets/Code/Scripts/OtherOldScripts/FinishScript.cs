//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using UnityEngine;

namespace Code.Scripts {
    public class FinishScript : Collideable
    {
        public GameObject finishCanvas;
        protected override void OnCollide(Collider2D coll) {

            if (coll.name == "Player") {
                finishCanvas.SetActive(true);
            }
        }
    }
}
