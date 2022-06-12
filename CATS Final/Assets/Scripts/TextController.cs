using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public int life;
    public int score;

    public GameObject lifeText;
    public GameObject scoreText;

    private void Update() {
        lifeText.GetComponent<Text>().text = "Can: " + life;
        scoreText.GetComponent<Text>().text = "Skor: " + score;
    }
}
