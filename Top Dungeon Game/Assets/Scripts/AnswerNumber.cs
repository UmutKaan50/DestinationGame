using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerNumber : MonoBehaviour {

    private IEnumerator coroutineNumberSprite;

    public Sprite normalButtonSprite;
    public Sprite pressedButtonSprite;

    public Text buttonValueText;

    public int firstNumber;
    public bool firstSelected;
    public int secondNumber;
    public bool secondSelected;

    private void Awake() {
        GetComponent<Button>().onClick.AddListener(SpriteChange);
        buttonValueText = GetComponentInChildren<Text>();
    }
    public void SoundManagerPlay(AudioClip audioClip) {
        SoundManager.instance.audioSource.PlayOneShot(audioClip);
    }

    // Calculations also happen here:
    public void SpriteChange() {
        if (gameObject.tag == "ProcessButton") {
            // Seperating buttons based on the text on them:
            if (buttonValueText.text.ToString() == "Try") {
                // It's Try button.
            } else if (buttonValueText.text.ToString() == "Del") {
                // It's Del button.
            }
        } else {
            if (!AnswerButton.instance.isTried) {
                if (AnswerButton.instance.requiredDigit == 1 && !AnswerButton.instance.firstSelected) {
                    firstNumber = int.Parse(buttonValueText.text.ToString());
                    AnswerButton.instance.firstDigit = firstNumber;
                    AnswerButton.instance.playersTry = firstNumber;
                }
                if (AnswerButton.instance.requiredDigit == 2 && AnswerButton.instance.firstSelected && !AnswerButton.instance.secondSelected) {
                    secondNumber = int.Parse(buttonValueText.text.ToString());
                    AnswerButton.instance.secondDigit = secondNumber;
                    AnswerButton.instance.playersTry = firstNumber * 10 + secondNumber;
                }
            }

            //if (!MathsManager.instance.firstSelected) {
            //    firstNumber = int.Parse(buttonValueText.text.ToString());
            //    MathsManager.instance.firstNumber = firstNumber;
            //    MathsManager.instance.firstSelected = true;
            //} else if (MathsManager.instance.firstSelected) {
            //    secondNumber = int.Parse(buttonValueText.text.ToString());
            //    MathsManager.instance.secondNumber = secondNumber;
            //    MathsManager.instance.secondSelected = true;
            //} else if (MathsManager.instance.secondSelected) {
            //    return;
            //}
        }


        // With this we can call coroutines with parameters I guess:
        SoundManagerPlay(SoundManager.instance.softButtonTap);
        coroutineNumberSprite = NumberSpriteChangeCoroutine(0.3f);
        StartCoroutine(coroutineNumberSprite);
    }
    private IEnumerator NumberSpriteChangeCoroutine(float time) {
        //GetComponent<Button>().image.sprite = pressedButtonSprite;
        GetComponent<Image>().sprite = pressedButtonSprite;
        GetComponentInChildren<Text>().gameObject.transform.Translate(0f, -3f, 0f);
        yield return new WaitForSeconds(time);
        GetComponent<Image>().sprite = normalButtonSprite;
        GetComponentInChildren<Text>().gameObject.transform.Translate(0f, 3f, 0f);
        //GetComponent<Button>().image.sprite = normalButtonSprite;
    }
}
