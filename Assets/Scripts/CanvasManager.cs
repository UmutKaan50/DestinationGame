//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System;
using System.Collections;
using UnityEngine;

namespace y01cu {
    /// <summary>
    ///     This CanvasManger scripts will only be able to control InstructionPanel.
    /// </summary>
    public class CanvasManager : MonoBehaviour {
        private Animator instructionPanelAnimator;

        [SerializeField] private GameObject[] gameObjectsToBeActivated;

        [SerializeField] private GameObject[] gameObjectsToBeDeactivated;


        [SerializeField] private GameObject helpButton;
        private AudioClip anotherOne;
        private AudioSource audioSource;
        private AudioClip interfaceClickSoundEffect;

        private void Awake() {
            instructionPanelAnimator =
                GameObject.Find("HUD").transform.Find("InstructionPanel").GetComponent<Animator>();
        }
        
        private void Start() {
            Invoke("MakeSureSpecificGameObjectsAreActive", .1f);
            Invoke("MakeSureSpecificGameObjectsAreInactive", .1f);

            TriggerInstructionPanelEntranceAnimation();
            audioSource = GetComponent<AudioSource>();
        }
        
        

        private void MakeSureSpecificGameObjectsAreActive() {
            foreach (var gameObjToBeActivated in gameObjectsToBeActivated) gameObjToBeActivated.SetActive(true);
        }

        private void MakeSureSpecificGameObjectsAreInactive() {
            foreach (var gameObjToBeDeactivated in gameObjectsToBeDeactivated) gameObjToBeDeactivated.SetActive(false);
        }

        private void TriggerInstructionPanelEntranceAnimation() {
            // Enter is the initial state that we want to trigger:
            instructionPanelAnimator.SetTrigger("Enter");
        }

        public void CallHelpButtonActivationCoroutine() {
            StartCoroutine(ActivateHelpButtonAfterSomeTime());
        }

        private IEnumerator ActivateHelpButtonAfterSomeTime() {
            yield return new WaitForSeconds(5f);
            helpButton.SetActive(true);
        }
    }
}