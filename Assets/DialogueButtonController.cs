//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace y01cu {
    public class DialogueButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        private Animator animator;

        private void Awake() {
            animator = GetComponent<Animator>();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            animator.SetBool("isPlayerHoveringOver", true);
        }

        public void OnPointerExit(PointerEventData eventData) {
            animator.SetBool("isPlayerHoveringOver", false);
        }
    }
}