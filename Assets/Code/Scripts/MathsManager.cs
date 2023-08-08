using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathsManager : MonoBehaviour {
    public static MathsManager instance;
    public Animator passQuestionAnimator;
    public Animator tryButtonAnimator;
    public int firstNumber;
    public bool firstSelected = false;
    public int secondNumber;
    public bool secondSelected = false;
    public int unitedNumber;
    // public List<AnswerButton> emptyButtons;
    public List<GameObject> unselectedEmptyButtons;
    public List<GameObject> selectedEmptyButtons;

    public bool isQuestionSolved = false;
    private void Awake() {
        if (MathsManager.instance != null) {
            return;
        }
        instance = this;

    }
    public void ClearValues() {
        firstNumber = 0;
        secondNumber = 0;
        unitedNumber = 0;
        firstSelected = false;
        secondSelected = false;
    }

    public int controller = 0;
    public string newText = "";
    private void FixedUpdate() {

        if (firstSelected == true) {
            unitedNumber = firstNumber;
        }
        if (secondSelected == true) {
            unitedNumber = firstNumber * 10 + secondNumber;
        }



        // One button can be clicked at the same time:
        foreach (var unselectedEmptyButton in unselectedEmptyButtons) {
            if (unselectedEmptyButton.GetComponent<AnswerButton>().isPressed) {
                // foreach below prevent two buttons can be pressed at the same time:
                foreach (var selectedEmptyButton in selectedEmptyButtons) {
                    selectedEmptyButton.GetComponent<AnswerButton>().ButtonSpriteAlteration();
                    selectedEmptyButtons.Remove(selectedEmptyButton);
                    unselectedEmptyButtons.Add(selectedEmptyButton);
                    newText = "";
                }
                unselectedEmptyButton.GetComponentInChildren<Text>().text = newText;
                unselectedEmptyButtons.Remove(unselectedEmptyButton);
                selectedEmptyButtons.Add(unselectedEmptyButton);
                continue;
            }
            //unselectedEmptyButton.GetComponent<AnswerButton
        }

        foreach (var selectedEmptyButton in selectedEmptyButtons) {
            selectedEmptyButton.GetComponentInChildren<Text>().text = newText;
            if (!selectedEmptyButton.GetComponent<AnswerButton>().isPressed) {
                selectedEmptyButtons.Remove(selectedEmptyButton);
                unselectedEmptyButtons.Add(selectedEmptyButton);
                // Test succeded : )
                newText = "";
            }
        }

        CheckIfAllTried();


    }


    public GameObject selectedButton;
    public void Control(string numberText) {
        if (numberText == "Try") {
            return;
        }
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
    public PlayerController player;
    public int check;
    public GameObject tryButton;
    public string tryButtonText = "hiding";

    // Checking if answered or not:
    public void CheckIfAllTried() {
        check = 0;
        foreach (var item in unselectedEmptyButtons) {
            if (item.GetComponent<AnswerButton>().isSolved || item.GetComponent<AnswerButton>().isTried) {
                check++;
            }
        }
        if (check == 5) {
            // Make show animation once:
            if (TryButton.instance.isHidden) {
                TryButton.instance.isHidden = false;
                tryButtonAnimator.SetTrigger("show");
                tryButtonAnimator.SetTrigger("stop");

            }
        }

    }
    // Checking for correct answers:
    public void FinishCall() {
        check = 0;
        foreach (var item in unselectedEmptyButtons) {
            if (item.GetComponent<AnswerButton>().isSolved) {
                check++;
            }
        }
        if (check == 5) {
            passQuestionAnimator.SetTrigger("hide");
            SoundController.instance.audioSource.PlayOneShot(SoundController.instance.keyPickUp);
            player.SetHasKey();
            QuestNpc.instance.clickLockingPanel.SetActive(false);
            return;
        }
        SoundController.instance.audioSource.PlayOneShot(SoundController.instance.failedAttempt);

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



}
