using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using y01cu;

public class MathsManager : MonoBehaviour {
    public static MathsManager instance;

    public static Action ButtonClicked;
    public Animator passQuestionAnimator;
    public Animator tryButtonAnimator;
    public int firstNumber;
    public bool firstSelected;
    public int secondNumber;
    public bool secondSelected;

    public int unitedNumber;

    // public List<AnswerButton> emptyButtons;

    public List<AnswerButton> unpressedEmptyButtons;
    public List<AnswerButton> pressedEmptyButtons;

    // [SerializeField] private AnswerButton[] answerButtons;

    [SerializeField] private EmptyButtons emptyButtons;

    public bool isQuestionSolved;

    public int controller;
    public string newText = "";

    [SerializeField] private Animator lockPanelAnimator;

    public GameObject selectedButton;

    public Player player;
    public int check;
    public GameObject tryButton;
    public string tryButtonText = "hiding";

    private List<AnswerButton> tempPressedEmptyButtons;
    private List<AnswerButton> tempUnpressedEmptyButtons;

    private void Awake() {
        if (instance != null) return;

        instance = this;

        tempPressedEmptyButtons = pressedEmptyButtons;
        tempUnpressedEmptyButtons = unpressedEmptyButtons;
    }

    private void Start() {
        ButtonClicked += CheckForCalculations;
    }

    private void FixedUpdate() {
        // One button can be clicked at the same time:

        //-----------------------------------------------------------------------------------------------------------------------------
        // foreach (AnswerButton button in unselectedEmptyButtons) {
        //     int selectedButtonCount = 0;
        //     if (button.isSelected) {
        //         selectedButtonCount++;
        //
        //         // foreach below prevent two buttons can be pressed at the same time:
        //         // Change it's sprite and remove every selected button from selected list, add it to unselected list, remove it's text: 
        //         foreach (AnswerButton previouslySelectedButton in selectedEmptyButtons) {
        //             // previouslySelectedButton.ButtonSpriteAlteration();
        //             previouslySelectedButton.GetComponent<Button>().image.sprite = emptyButtons.unpressedSprite;
        //             selectedEmptyButtons.Remove(previouslySelectedButton);
        //             unselectedEmptyButtons.Add(previouslySelectedButton);
        //             newText = "";
        //         }
        //
        //         // Change it's text, remove it from unselected list, add it to selected list:
        //         button.GetComponentInChildren<Text>().text = newText;
        //         button.GetComponent<Button>().image.sprite = emptyButtons.pressedSprite;
        //         unselectedEmptyButtons.Remove(button);
        //         selectedEmptyButtons.Add(button);
        //
        //         // continue;
        //     }
        //
        //     //unselectedEmptyButton.GetComponent<AnswerButton
        // }
        //-----------------------------------------------------------------------------------------------------------------------------

        // foreach (var selectedEmptyButton in selectedEmptyButtons) {
        //     selectedEmptyButton.GetComponentInChildren<Text>().text = newText;
        //     if (!selectedEmptyButton.GetComponent<AnswerButton>().isSelected) {
        //         selectedEmptyButtons.Remove(selectedEmptyButton);
        //         unselectedEmptyButtons.Add(selectedEmptyButton);
        //         // Test succeded : )
        //         newText = "";
        //     }
        // }

        // CheckIfAllTried();
    }

    public void ClearValues() {
        firstNumber = 0;
        secondNumber = 0;
        unitedNumber = 0;
        firstSelected = false;
        secondSelected = false;
    }

    public void InvokeButtonClickedEvent() {
        ButtonClicked?.Invoke();
    }

    public void CheckForCalculations() {
        #region LatestAttempt

        // if (firstSelected == true) {
        //     unitedNumber = firstNumber;
        // }
        //
        // if (secondSelected == true) {
        //     unitedNumber = firstNumber * 10 + secondNumber;
        // }
        //
        // // if (Input.GetKey(KeyCode.Y)) {
        // //     lockPanelAnimator.Play("LockPanel_hidden");
        // //     lockPanelAnimator.SetBool("isCoveringNumbers", false);
        // // }
        // //
        // // if (Input.GetKey(KeyCode.U)) {
        // //     lockPanelAnimator.Play("LockPanel_Covering");
        // //     lockPanelAnimator.SetBool("isCoveringNumbers", true);
        // // }
        //
        // foreach (AnswerButton pressedEmptyButton in tempPressedEmptyButtons) {
        //     if (pressedEmptyButtons.Count > 0) {
        //     }
        // }
        //
        // foreach (AnswerButton button in tempUnpressedEmptyButtons) {
        //     bool isNewButtonPressed = button.GetComponent<AnswerButton>().isSelected;
        //     int counter = 0;
        //     if (isNewButtonPressed) {
        //         counter++;
        //         Debug.Log($"{counter} - new button is pressed.");
        //         lockPanelAnimator.Play("LockPanel_hidden");
        //         lockPanelAnimator.SetBool("isCoveringNumbers", false);
        //
        //         if (lockPanelAnimator.GetBool("isCoveringNumbers")) {
        //         }
        //
        //         // First update pressed buttons list
        //
        //         foreach (AnswerButton previouslyPressedEmptyButton in tempPressedEmptyButtons) {
        //             previouslyPressedEmptyButton.GetComponent<Button>().image.sprite = emptyButtons.unpressedSprite;
        //             pressedEmptyButtons.Remove(previouslyPressedEmptyButton);
        //             unpressedEmptyButtons.Add(previouslyPressedEmptyButton);
        //             // ?
        //             newText = "";
        //         }
        //
        //         button.GetComponent<Button>().image.sprite = emptyButtons.pressedSprite;
        //         button.GetComponentInChildren<Text>().text = newText;
        //         unpressedEmptyButtons.Remove(button);
        //         pressedEmptyButtons.Add(button);
        //     }
        // }

        #endregion

        // One button can be clicked at the same time:
        foreach (var unselectedEmptyButton in unpressedEmptyButtons)
            if (unselectedEmptyButton.GetComponent<AnswerButton>().isPressed) {
                // foreach below prevent two buttons can be pressed at the same time:
                foreach (var selectedEmptyButton in pressedEmptyButtons) {
                    selectedEmptyButton.GetComponent<AnswerButton>().ButtonSpriteAlteration();
                    pressedEmptyButtons.Remove(selectedEmptyButton);
                    unpressedEmptyButtons.Add(selectedEmptyButton);
                    newText = "";
                }

                unselectedEmptyButton.GetComponentInChildren<Text>().text = newText;
                unpressedEmptyButtons.Remove(unselectedEmptyButton);
                pressedEmptyButtons.Add(unselectedEmptyButton);
            }

        //unselectedEmptyButton.GetComponent<AnswerButton
        foreach (var selectedEmptyButton in pressedEmptyButtons) {
            selectedEmptyButton.GetComponentInChildren<Text>().text = newText;
            if (!selectedEmptyButton.GetComponent<AnswerButton>().isPressed) {
                pressedEmptyButtons.Remove(selectedEmptyButton);
                unpressedEmptyButtons.Add(selectedEmptyButton);
                // Test succeded : )
                newText = "";
            }
        }

        CheckIfAllTried();
    }

    public void Control(string numberText) {
        if (numberText == "Try") return;

        newText = numberText;

        //foreach (GameObject obj in unselectedEmptyButtons) {
        //    if (obj.GetComponent<AnswerButton>().isPressed) {
        //        unselectedEmptyButtons.Remove(obj);
        // selectedButton = obj;
        // gameObject.GetComponentInChildren<Text>().text;
        //        unselectedEmptyButtons.Add(selectedButton);
        //        selectedButton = null;
        //    }
        //}
    }

    // Checking if answered or not:
    public void CheckIfAllTried() {
        check = 0;
        foreach (var item in unpressedEmptyButtons)
            if (item.GetComponent<AnswerButton>().isInputCorrect ||
                item.GetComponent<AnswerButton>().isInputIncorrect)
                check++;

        if (check == 5)
            // Make show animation once:
            if (TryButton.instance.isHidden) {
                TryButton.instance.isHidden = false;
                tryButtonAnimator.SetTrigger("show");
                tryButtonAnimator.SetTrigger("stop");
            }
    }

    // Checking for correct answers:
    public void FinishCall() {
        check = 0;
        // foreach (var item in unselectedEmptyButtons) {
        //     if (item.GetComponent<AnswerButton>().isInputCorrect) {
        //         check++;
        //     }
        // }

        if (check == 5) {
            passQuestionAnimator.SetTrigger("hide");
            SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.keyPickUp);
            player.SetHasKey();
            QuestNpc.instance.clickLockingPanel.SetActive(false);
            return;
        }

        SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.failedAttempt);
    }

    /* // Previous finish call:
        foreach (var item in emptyButtons) {
            if (item.GetComponent<AnswerButton>().isPressed) {

                controller++;
            }

        }

        if (controller == 5) {
            controller = 0;
            animator.SetTrigger("hide");
        }
        for (int i = 0; i < 100; i++) {
            Debug.Log(controller);

        }
                    foreach (var unselected in unselectedEmptyButtons) {
                        unselected.GetComponent<AnswerButton>().ButtonSpriteAlteration();
                    }
                }
     */

    public static bool IsAnyButtonSelected() {
        var isAnyButtonSelected = false;

        // if(selectedEmptyButtons.Count > 0) {
        //     isAnyButtonSelected = true;
        // }

        return isAnyButtonSelected;
    }
}