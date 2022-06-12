using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIContollerX : MonoBehaviour {

    public int goal;
    public int health;

    public GameObject healthText;
    public GameObject goalText;

    private void Update() {
        healthText.GetComponent<Text>().text = "Can: " + health;
        goalText.GetComponent<Text>().text = "Gol: " + goal;


    }

}
