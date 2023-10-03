using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using y01cu;

public class AnswerButton : MonoBehaviour {
    public static AnswerButton instance;
    [SerializeField] private EmptyButtons emptyButtons;

    [FormerlySerializedAs("isCorrect")] [FormerlySerializedAs("isSolved")]
    public bool isInputCorrect = false;

    [FormerlySerializedAs("isTried")] public bool isInputIncorrect = false;

    public int requiredNumber;

    //public int requiredDigit;
    public int playersTry;

    // public Sprite normalButtonSprite;
    // public Sprite pressedButtonSprite;

    //public int firstDigit;
    //public int secondDigit;

    //public bool firstSelected;
    //public bool secondSelected;

    [FormerlySerializedAs("isSelected")] public bool isPressed = false;
    public GameObject lockPanel;

    [SerializeField] private MathsManager mathsManager;

    private void FixedUpdate() {
        bool isTextFieldEmpty = GetComponentInChildren<Text>().text == "";

        if (!isTextFieldEmpty) {
            bool isInputMatchingWithRequiredNumber =
                requiredNumber.ToString() == GetComponentInChildren<Text>().text;

            if (isInputMatchingWithRequiredNumber) {
                isInputCorrect = true;
            }

            int upperNumber;
            int lowerNumber;
            bool isInputOneDigitAndPositive =
                (int.TryParse(GetComponentInChildren<Text>().text, out upperNumber) && upperNumber < 10) &&
                (int.TryParse(GetComponentInChildren<Text>().text, out lowerNumber) && lowerNumber >= 0);
            if (isInputOneDigitAndPositive) {
                isInputIncorrect = true;
            }
        }
    }

    private void Awake() {
        if (AnswerButton.instance != null) {
            return;
        }

        instance = this;
    }

    private void Start() {
        Invoke("AddListeners", 2f);
    }

    private void AddListeners() {
        MathsManager.ButtonClicked += ButtonSpriteAlteration;
        GetComponent<Button>().onClick.AddListener(mathsManager.InvokeButtonClickedEvent);
        // GetComponent<Button>().onClick.AddListener(ButtonSpriteAlteration);
    }


    /// <summary>
    /// Called when button is clicked. Updates button sprite and plays sound effect.
    /// </summary>
    public void ButtonSpriteAlteration() {
        // I selected 4.4f for more realistic experience.
        // I couldn't handle when button press process happens consecutively.
        // (?)

        if (isPressed == false) {
            isPressed = true;
            GetComponent<Button>().image.sprite = emptyButtons.pressedSprite;
            // SoundManagerPlay(SoundManager.instance.buttonTap);
            // If empty button is pressed, hide lock panel:


            // if (!MathsManager.IsAnyButtonSelected()) {

            GetComponentInChildren<Text>().transform.Translate(0, -4.4f, 0);

            // if (mathsManager.selectedEmptyButtons.Count == 2) {
            //     // return;
            // }

            // lockPanel.GetComponent<Animator>().SetTrigger("hide");
            // Here we're hiding the lock panel, not answer buttons.
        }
        else if (isPressed == true) {
            isPressed = false;
            GetComponent<Button>().image.sprite = emptyButtons.unpressedSprite;
            // If empty button is released, show lock panel:

            GetComponentInChildren<Text>().transform.Translate(0, 4.4f, 0);

            // lockPanel.GetComponent<Animator>().SetTrigger("show");
        }

        SoundManagerPlay(SoundManager.instance.buttonTap);
    }


    public void SoundManagerPlay(AudioClip audioClip) {
        SoundManager.instance.audioSource.PlayOneShot(audioClip);
    }
}