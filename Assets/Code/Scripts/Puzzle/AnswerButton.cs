//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts {
    public class AnswerButton : MonoBehaviour {
        public static AnswerButton instance;

        public bool isSolved = false;
        public bool isTried = false;

        public int requiredNumber;
        //public int requiredDigit;
        public int playersTry;

        public Sprite normalButtonSprite;
        public Sprite pressedButtonSprite;

        //public int firstDigit;
        //public int secondDigit;

        //public bool firstSelected;
        //public bool secondSelected;

        public bool isPressed = false;
        public GameObject lockPanel;

        private void FixedUpdate() {
            // TODO: Understand what this code block below does and figure out a way to implement it if necessary 
            // if (GetComponentInChildren<Text>().text.ToString() != "") {
            //
            //     if (requiredNumber.ToString() == GetComponentInChildren<Text>().text.ToString()) {
            //         isSolved = true;
            //     }
            //     if (int.Parse(GetComponentInChildren<Text>().text.ToString()) < 10 && int.Parse(GetComponentInChildren<Text>().text.ToString()) >= 0) {
            //         isTried = true;
            //     }
            // }
        }

        private void Awake() {
            GetComponent<Button>().onClick.AddListener(ButtonSpriteAlteration);
            if (AnswerButton.instance != null) {
                return;
            }
            instance = this;

        }

        // Changing button sprite based on tap:
        public void ButtonSpriteAlteration() {
            // I selected 4.4f for more realistic experience.
            // I couldn't handle when button press process happens consecutively.
            if (isPressed == false) {
                isPressed = true;
                GetComponent<Button>().image.sprite = pressedButtonSprite;
                SoundManagerPlay(SoundController.instance.buttonTap);
                // If empty button is pressed, hide lock panel:
                lockPanel.GetComponent<Animator>().SetTrigger("hide");
                GetComponentInChildren<Text>().transform.Translate(0, -4.4f, 0);
            } else if (isPressed == true) {
                isPressed = false;
                GetComponent<Button>().image.sprite = normalButtonSprite;
                SoundManagerPlay(SoundController.instance.buttonTap);
                // If empty button is released, show lock panel:
                lockPanel.GetComponent<Animator>().SetTrigger("show");
                GetComponentInChildren<Text>().transform.Translate(0, 4.4f, 0);
            }
        }





        public void SoundManagerPlay(AudioClip audioClip) {
            SoundController.instance.audioSource.PlayOneShot(audioClip);
        }


    }
}

