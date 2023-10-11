//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace y01cu {
    [Obsolete("Vanishing time can be customised whenever an instance is created later on.")]
    public class OptionPanel : PanelBase {
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;

        private Action yesButtonLocalAction;
        private Action noButtonLocalAction;

        public void AddOptionButtonFunctionalities(Action yesButtonAction, Action noButtonAction) {
            // Both buttons should have the same effect of the exit button, that's playing the exit animation.

            yesButton.onClick.AddListener(() => {
                yesButtonAction.Invoke();
                audioSource.PlayOneShot(interfaceClickSoundEffect);
                exitButton.onClick.Invoke();
            });

            noButton.onClick.AddListener(() => {
                PlayExitAnimation();
                noButtonAction.Invoke();
                audioSource.PlayOneShot(interfaceClickSoundEffect);
            });
        }
    }
}