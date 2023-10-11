//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace y01cu {
    public class PanelBase : MonoBehaviour {
        [SerializeField] protected TextMeshProUGUI textMeshProUGUI;
        [SerializeField] protected Button exitButton;
        [SerializeField] protected Animator animator;
        [SerializeField] protected AnimationClip animation;
        [SerializeField] protected AudioClip interfaceClickSoundEffect;
        protected AudioSource audioSource;

        private void Start() {
            // There's no need to play anentrance animation since we'll assign an animation as initial one which'll be played when the object becomes enable.
            float displayTimeBeforeVanishing = 3f;
            AdjustPositionAndParent();
            Invoke("AddButtonExitFunctionality", 1f);
            Invoke("Vanish", displayTimeBeforeVanishing);
            audioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        }

        private void Vanish() {
            // Play exit animation and then destroy itself:
            string exitAnimationName = "Exit";

            float exitAnimationLength = animation.length * 1.1f;

            animator.Play(exitAnimationName);

            Destroy(gameObject, exitAnimationLength);
        }

        private GameObject exampleParentGameObject;
        [SerializeField] private Transform exampleTransform;

        private void AdjustPositionAndParent() {
            // I can look for a better way to find a specific gameobject even though i think i can't find.
            gameObject.transform.SetParent(GameObject.Find("HUD").transform);
            // We're lowering our scale here since we're using a canvas that's small, 800x600.
            Invoke("AdjustScale", 0.1f);
        }

        private void AdjustScale() {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        public void SetMessage(string desiredMessage) {
            textMeshProUGUI.text = desiredMessage;
        }

        private void AddButtonExitFunctionality() {
            exitButton.onClick.AddListener(() => {
PlayExitAnimation();            
                audioSource.PlayOneShot(interfaceClickSoundEffect);
            });
        }

        protected void PlayExitAnimation() {
            // Play the exit animation:
            string exitAnimationName = "Exit";
            animator.Play(exitAnimationName);

        }
    }
}