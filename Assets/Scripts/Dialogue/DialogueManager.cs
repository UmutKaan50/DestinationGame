//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;
using UnityEngine.SceneManagement;

namespace y01cu {
    public class DialogueManager : MonoBehaviour {
        private TextMeshProUGUI messageText;
        private TextWriter.TextWriterSingle textWriterSingle;
        private AudioSource talkingAudioSource;
        private int messageIndexCounter = 0;
        private Player player;
        private QuestNpc questNPC;
        private GameObject buttonHolder;
        private bool isInitialConversationStarted = false;

        private void Awake() {
            FindSomeRequiredGameObjectsAndComponents();
        }

        private void FindSomeRequiredGameObjectsAndComponents() {
            messageText = transform.Find("DialogueBox").Find("Text").GetComponent<TextMeshProUGUI>();
            talkingAudioSource = transform.Find("DialogueSound").GetComponent<AudioSource>();
            player = GameObject.Find("Player").GetComponent<Player>();
            questNPC = GameObject.Find("QuestNPC").GetComponent<QuestNpc>();

            // TODO: Try if gameobject.find("ButtonHolder") directly works when you have free time.
            buttonHolder = GameObject.Find("DialogueCanvas").transform.Find("DialogueManager").Find("ButtonHolder")
                .gameObject;
        }

        private int GetCurrentSceneIndex() {
            // if it is 1-> lvl1  ||| if it is 2-> lvl2
            return SceneManager.GetActiveScene().buildIndex;
        }


        public void SetupText() {
            // Let's store conversation text in questNPC. Since there'll be only one questNPC per scene we can get that text by using GameObject.Find("QuestNPC").GetComponent<QuestNPC>().conversationText approach for example. Consider a case where we want to stick to this way of handling we can position different npcs in different scene. Consider a scenario where marketNPC stands in another scene and questNPC stands in another one. 

            // TODO: Add another NPC that will assign it's text when questNPC is missing in that scene.

            QuestNPC_Conversations questNPCConversations = new QuestNPC_Conversations();

            List<string> conversations = new List<string>();

            bool isLevelOne = GetCurrentSceneIndex() == 1;
            bool isLevelTwo = GetCurrentSceneIndex() == 2;
            if (isLevelOne) {
                conversations = questNPCConversations.GetLevelOneInitialConversation();
            }
            else if (isLevelTwo) {
                conversations = questNPCConversations.GetLevelTwoConversation();
            }

            transform.Find("DialogueBox").GetComponent<Button_UI>().ClickFunc = () => {
                bool isTextWriterCurrentlyActive = textWriterSingle != null && textWriterSingle.IsActive();
                if (isTextWriterCurrentlyActive) {
                    textWriterSingle.WriteAllAndDestroy();
                }
                else {
                    string message = "";

                    if (messageIndexCounter == conversations.Count) {
                        StopTalkingSound();
                        ActivateChoices();
                        return;
                    }

                    if (!isInitialConversationStarted && messageIndexCounter < conversations.Count) {
                        message = conversations[messageIndexCounter];
                    }

                    StartTalkingSound();
                    textWriterSingle =
                        TextWriter.AddWriterWithSpeed_Static(messageText, message, .02f, true, true, StopTalkingSound);

                    messageIndexCounter++;
                }
            };
        }

        private void ActivateChoices() {
            buttonHolder.SetActive(true);
            buttonHolder.GetComponent<Animator>().Play("Entrance");
            Button yesButton = buttonHolder.transform.Find("YesButton").GetComponent<Button>();
            Button noButton = buttonHolder.transform.Find("NoButton").GetComponent<Button>();

            yesButton.onClick.AddListener(() => {
                questNPC.ActivatePuzzlePanel();
                GetComponent<Animator>().Play("Exit");
                buttonHolder.GetComponent<Animator>().Play("Exit");
                StartCoroutine(DeactivateGivenGameObjectAfterSomeTime(buttonHolder, 0.5f));
            });
            noButton.onClick.AddListener(() => {
                messageIndexCounter = 0;
                GetComponent<Animator>().Play("Exit");
                buttonHolder.GetComponent<Animator>().Play("Exit");
                StartCoroutine(DeactivateGivenGameObjectAfterSomeTime(buttonHolder, 0.5f));
                player.IsAnInterruptionOccuring = false;
                Invoke("DeactivateItself", 2f);
            });
        }

        private IEnumerator DeactivateGivenGameObjectAfterSomeTime(GameObject gameObj, float timeDelay) {
            yield return new WaitForSeconds(timeDelay);
            gameObj.SetActive(false);
        }

        private void DeactivateItself() {
            gameObject.SetActive(false);
        }

        private void StartTalkingSound() {
            talkingAudioSource.Play();
        }

        private void StopTalkingSound() {
            talkingAudioSource.Stop();
        }
    }
}