using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNpc : MonoBehaviour
{
    public bool isQuestPanelOpened = false;
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Player") {

        }
    }
}
