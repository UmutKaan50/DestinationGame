//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts {
    public class FloatingText {
        public bool active;
        public GameObject go;
        public Text txt;
        public Vector3 motion;
        public float duration;
        public float lastShown;

        public void Show() {
            active = true;
            lastShown = Time.time;
            go.SetActive(active);
        }

        public void Hide() {
            active = false;
            go.SetActive(active);
        }

        public void UpdateFloatingText() {
            if (!active)
                return;

            //     10     -     7     >     2
            // Showing text long enough:
            if (Time.time - lastShown > duration) {
                Hide();
            }


            //go.transform.position += motion * Time.deltaTime;
            //// Instead of the transforming upwards I can animate it:
            //go.GetComponent<Animator>().SetTrigger("show");
            //go.GetComponent<Animation>().GetClip(go.GetComponent<Animator>().clip).length;
            //AnimatorClipInfo info = go.GetComponent<AnimatorClipInfo>();
            //info.
        }
    }
}