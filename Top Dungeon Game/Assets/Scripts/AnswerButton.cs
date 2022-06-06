using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour {
    public static AnswerButton instance;

    public bool isSolved = false;
    public bool isTried = false;

    public int requiredNumber;
    public int requiredDigit;
    public int playersTry;

    public Sprite normalButtonSprite;
    public Sprite pressedButtonSprite;

    public int firstDigit;
    public int secondDigit;

    public bool firstSelected;
    public bool secondSelected;

    public bool isPressed = false;
    public GameObject lockPanel;
    private void Awake() {
        GetComponent<Button>().onClick.AddListener(ButtonSpriteAlteration);
        if (AnswerButton.instance != null) {
            return;
        }
        instance = this;

    }

    // Changing button sprite based on tap:
    public void ButtonSpriteAlteration() {

        if (isPressed == false) {
            isPressed = true;
            GetComponent<Button>().image.sprite = pressedButtonSprite;
            for (int i = 0; i < 100; i++) {
                Debug.Log("Sprite should've changed.");

            }
            SoundManagerPlay(SoundManager.instance.buttonTap);
            // If empty button is pressed, hide lock panel:
            lockPanel.GetComponent<Animator>().SetTrigger("hide");
        } else if (isPressed == true) {
            isPressed = false;
            GetComponent<Button>().image.sprite = normalButtonSprite;
            SoundManagerPlay(SoundManager.instance.buttonTap);
            // If empty button is released, show lock panel:
            lockPanel.GetComponent<Animator>().SetTrigger("show");
        }
    }

    public void Calculate() {





        //if (isPressed && !isTried) {
        //    playersTry = MathsManager.instance.unitedNumber;
        //    GetComponentInChildren<Text>().text = playersTry.ToString();
        //    isTried = true;

        //}
        //if (!isPressed && isTried) {
        //    MathsManager.instance.ClearValues();
        //}
    }

    /*
    public void Calculation() {
        if (!isSolved) {


            if (isPressed) {
                if (requiredDigit == 1) {
                    if (MathsManager.instance.firstSelected && !MathsManager.instance.secondSelected) {
                        firstDigit = MathsManager.instance.firstNumber;
                        playersTry = firstDigit;
                        GetComponentInChildren<Text>().text = playersTry.ToString();

                        if (isTried != true) {
                            isTried = true;
                        }
                    }
                } else if (requiredDigit == 2) {
                    if (MathsManager.instance.firstSelected && MathsManager.instance.secondSelected) {
                        firstDigit = MathsManager.instance.firstNumber * 10;
                        secondDigit = MathsManager.instance.secondNumber;
                        playersTry = firstDigit + secondDigit;

                        if (isTried != true) {
                            isTried = true;
                        }
                    }
                }
                GetComponentInChildren<Text>().text = playersTry.ToString();
                
            }
            if (isTried) {


                if (playersTry == requiredNumber) {
                    isSolved = true;
                    SoundManagerPlay(SoundManager.instance.tempMathsTrue);
                    // Clearing try:
                    isTried = false;
                } else {
                    // Maybe we can add coroutine here.
                    GetComponentInChildren<Text>().text = "";
                    isSolved = false;
                    SoundManagerPlay(SoundManager.instance.tempMathsFalse);
                    
                }
                playersTry = 0;
            }
        } else {
            return;
        }

    }
     
    */
    public void SoundManagerPlay(AudioClip audioClip) {
        SoundManager.instance.audioSource.PlayOneShot(audioClip);
    }

    private void FixedUpdate() {
        //Calculate();
        //Calculation();
        // Assinging values will happen here:
    }
}
