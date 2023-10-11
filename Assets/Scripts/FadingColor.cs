//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

namespace y01cu {
    public class FadingColor : MonoBehaviour {
        public Image image;
        public float fadeDuration = 1f;

        private void Start() {
            image.color = Color.black;
        }

        private void Update() {
            FadeToTransparent();
        }

        private void FadeToTransparent() {
            // Decrease alpha over time
            float alpha = Mathf.MoveTowards(image.color.a, 0f, Time.deltaTime / fadeDuration);
            image.color = new Color(0, 0, 0, alpha);
        }
    }
}