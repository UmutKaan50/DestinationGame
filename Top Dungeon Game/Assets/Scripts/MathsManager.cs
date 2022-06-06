using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathsManager : MonoBehaviour {
    public static MathsManager instance;

    public int firstNumber;
    public bool firstSelected = false;
    public int secondNumber;
    public bool secondSelected = false;
    public int unitedNumber;

    public List<GameObject> unselectedEmptyButtons;
    public List<GameObject> selectedEmptyButtons;
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
    
    private void FixedUpdate() {
        if (firstSelected == true) {
            unitedNumber = firstNumber;
        }
        if (secondSelected == true) {
            unitedNumber = firstNumber * 10 + secondNumber;
        }

        

        foreach (var unselectedEmptyButton in unselectedEmptyButtons) {
            if (unselectedEmptyButton.GetComponent<AnswerButton>().isPressed) {
                foreach (var selectedEmptyButton in selectedEmptyButtons) {
                    selectedEmptyButton.GetComponent<AnswerButton>().ButtonSpriteAlteration();
                    selectedEmptyButtons.Remove(selectedEmptyButton);
                    unselectedEmptyButtons.Add(selectedEmptyButton);
                }
                unselectedEmptyButtons.Remove(unselectedEmptyButton);
                selectedEmptyButtons.Add(unselectedEmptyButton);
                continue;
            }
            //unselectedEmptyButton.GetComponent<AnswerButton>().
            
        }
    }

    /*
     
                    
                    
                    foreach (var unselected in unselectedEmptyButtons) {
                        unselected.GetComponent<AnswerButton>().ButtonSpriteAlteration();
                    }
                }
     */



}
