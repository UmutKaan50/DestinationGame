using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour {

    public Sprite normalButtonSprite;
    public Sprite pressedButtonSprite;

    public bool isPressed = false;
    private void Awake() {
        GetComponent<Button>().onClick.AddListener(ButtonSpriteAlteration);
    }

    public void ButtonSpriteAlteration() {
        if (isPressed == false) {
            isPressed = true; 
            GetComponent<Button>().image.sprite = pressedButtonSprite;
            SoundManagerPlay(SoundManager.instance.buttonTap);
            
        } else if (isPressed == true) {
            isPressed = false;
            GetComponent<Button>().image.sprite = normalButtonSprite;
            SoundManagerPlay(SoundManager.instance.buttonTap);
        }
    }

    public void SoundManagerPlay(AudioClip audioClip) {
        SoundManager.instance.audioSource.PlayOneShot(audioClip);
    }

}
