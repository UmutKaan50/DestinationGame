using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using y01cu;

public class FinishScript : Collideable {
    [SerializeField] private GameObject finishCanvasContainer;

    [SerializeField] private GameObject pfMessagePanel;

    private QuestNpc questNpc;

    private void Awake() {
        questNpc = GameObject.Find("QuestNPC").GetComponent<QuestNpc>();
    }

    protected override void OnCollide(Collider2D coll) {
        bool isReadyToFinish = coll.name == "Player" && questNpc.isPuzzleSolved == true;
        bool isPuzzleNotSolved = coll.name == "Player" && questNpc.isPuzzleSolved == false;
        if (isReadyToFinish) {
            finishCanvasContainer.SetActive(true);
        }

        if (isPuzzleNotSolved) {
            MessagePanel messagePanel =
                Instantiate(pfMessagePanel, pfMessagePanel.transform.position, Quaternion.identity)
                    .GetComponent<MessagePanel>();
            messagePanel.SetMessage(
                "You need to solve the puzzle before finishing this demo!");
        }
    }
}