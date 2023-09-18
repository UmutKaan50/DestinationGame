//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using UnityEngine;

namespace Code.Scripts {
    public class Chest : Collectable {
        public Sprite emptyChest;
        public int moneyAmount = 5;
        protected override void OnCollect() {
            if (!collected) {
                SoundController.instance.audioSource.PlayOneShot(SoundController.instance.pickingUpMoney);
                collected = true;
                GetComponent<SpriteRenderer>().sprite = emptyChest;
                GameManager.instance.money += moneyAmount;
                GameManager.instance.ShowText("+" + moneyAmount + " gold!", 25, Color.yellow, transform.position, Vector3.up * 50, 3.0f);
            }
        }
    }
}
