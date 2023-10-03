//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace y01cu {

    /// <summary>
    /// This CanvasManger scripts will only be able to control InstructionPanel.
    /// </summary>


    
    public class CanvasManager : MonoBehaviour {

        [SerializeField] private Animator instructionPanelAnimator;
        private AudioSource audioSource;
        private AudioClip interfaceClickSoundEffect;
        private AudioClip anotherOne;
        private void Start() {
            MakeSureSpecificGameObjectsAreActive();
            MakeSureSpecificGameObjectsAreInactive();
            
            TriggerInstructionPanelEntranceAnimation();
            audioSource = GetComponent<AudioSource>();
        }
        
        [SerializeField] private GameObject[] gameObjectsToBeActivated;
        private void MakeSureSpecificGameObjectsAreActive() {
            foreach (GameObject gameObjToBeActivated in gameObjectsToBeActivated) {
                gameObjToBeActivated.SetActive(true);
            }
        }
        
        [SerializeField] private GameObject[] gameObjectsToBeDeactivated;
        private void MakeSureSpecificGameObjectsAreInactive() {
            foreach (GameObject gameObjToBeDeactivated in gameObjectsToBeDeactivated) {
                gameObjToBeDeactivated.SetActive(true);
            }
        }
        
        private void TriggerInstructionPanelEntranceAnimation() {
            // Enter is the initial state that we want to trigger:
            instructionPanelAnimator.SetTrigger("Enter");
        }
        

        [SerializeField] private GameObject helpButton;
        public void CallHelpButtonActivationCoroutine() {
            StartCoroutine(ActivateHelpButtonAfterSomeTime());
        }
        
        private IEnumerator ActivateHelpButtonAfterSomeTime() {
            AnimatorStateInfo animatorStateInfo = instructionPanelAnimator.GetCurrentAnimatorStateInfo(0);
            float activationDelay = animatorStateInfo.length;
            
            yield return new WaitForSeconds(activationDelay);
            helpButton.SetActive(true);
        }
    }

}