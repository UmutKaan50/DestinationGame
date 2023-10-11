/*
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class UI_Assistant : MonoBehaviour {
    private TextMeshProUGUI messageText;
    private TextWriter.TextWriterSingle textWriterSingle;
    private AudioSource talkingAudioSource;
    private int messageIndexCounter = 0;

    private void Awake() {
        messageText = transform.Find("message").Find("messageText").GetComponent<TextMeshProUGUI>();
        talkingAudioSource = transform.Find("talkingSound").GetComponent<AudioSource>();

        transform.Find("message").GetComponent<Button_UI>().ClickFunc = () => {
            if (textWriterSingle != null && textWriterSingle.IsActive()) {
                // Currently active TextWriter
                textWriterSingle.WriteAllAndDestroy();
            }
            else {
                string[] messageArray = new string[] {
                    "This is the assistant speaking, hello and goodbye, see you next time!",
                    "Hey there!",
                    "This is a really cool and useful effect",
                    "Let's learn some code and make awesome games!",
                    "Check out Battle Royale Tycoon on Steam!",
                };

                // string message = messageArray[Random.Range(0, messageArray.Length)];
                string message = messageArray[messageIndexCounter];
                messageIndexCounter++;
                StartTalkingSound();
                textWriterSingle =
                    TextWriter.AddWriter_Static(messageText, message, .02f, true, true, StopTalkingSound);
            }
        };
    }

    private void StartTalkingSound() {
        talkingAudioSource.Play();
    }

    private void StopTalkingSound() {
        talkingAudioSource.Stop();
    }

    private void Start() {
        //TextWriter.AddWriter_Static(messageText, "This is the assistant speaking, hello and goodbye, see you next time!", .1f, true);
    }
}