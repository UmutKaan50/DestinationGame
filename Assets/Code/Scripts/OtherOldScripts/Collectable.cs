//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using UnityEngine;

namespace Code.Scripts
{
    public class Collectable : Collideable
    {
        // Logic:
        protected bool collected;
        protected override void OnCollide(Collider2D coll)
        {
            if (coll.name == "Player")
            {
                OnCollect();
            }
        }

        protected virtual void OnCollect()
        {
            collected = true;
        }

    }
}
