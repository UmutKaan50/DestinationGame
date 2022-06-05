using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNpc : Collideable {
    public static QuestNpc instance;


    public Animator assuranceQuestionAnimator;
    public Animator mathQuestAnimator;
    public bool isQuestAssurancePanelOpened = false;
    public bool isMathQuestAssurancePanelOpened = false;
    public GameObject AttackButton;
    public GameObject Joystick;




    private float cooldown = 3.0f;
    private float lastShout = -3.0f; // Instant reply at the beginning.

    // Hmm
    private void Awake() {
        if (QuestNpc.instance != null) {
            return;
        }
        instance = this;
    }
    protected override void OnCollide(Collider2D coll) {

        if (coll.gameObject.name == "Player" && !isQuestAssurancePanelOpened) {
            if (Time.time - lastShout > cooldown) {
                lastShout = Time.time;
                isQuestAssurancePanelOpened = true;
                assuranceQuestionAnimator.SetTrigger("show");

            }
        }
    }

    public void SoundManagerPlay(AudioClip audioClip) {
        SoundManager.instance.audioSource.PlayOneShot(audioClip);
    }
    public void CloseQuestAssurancePanel() {
        isQuestAssurancePanelOpened = false;
        assuranceQuestionAnimator.SetTrigger("hide");
        SoundManagerPlay(SoundManager.instance.buttonTap);
    }

    public void OpenMathQuestPanel() {
        isMathQuestAssurancePanelOpened = true;
        //isQuestAssurancePanelOpened = false;
        assuranceQuestionAnimator.SetTrigger("hide");
        mathQuestAnimator.SetTrigger("show");
        SoundManagerPlay(SoundManager.instance.buttonTap);
    }


}
