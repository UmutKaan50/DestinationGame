using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerNumber : MonoBehaviour {

    private IEnumerator coroutineNumberSprite;

    public Sprite normalButtonSprite;
    public Sprite pressedButtonSprite;
    private void Awake() {
        GetComponent<Button>().onClick.AddListener(SpriteChange);
    }
    public void SoundManagerPlay(AudioClip audioClip) {
        SoundManager.instance.audioSource.PlayOneShot(audioClip);
    }

    public void SpriteChange() {
        // With this we can call coroutines with parameters I guess.

        SoundManagerPlay(SoundManager.instance.softButtonTap);
        coroutineNumberSprite = NumberSpriteChangeCoroutine(0.3f);
        StartCoroutine(coroutineNumberSprite);
    }
    private IEnumerator NumberSpriteChangeCoroutine(float time) {
        //GetComponent<Button>().image.sprite = pressedButtonSprite;
        GetComponent<Image>().sprite = pressedButtonSprite;
        GetComponentInChildren<Text>().gameObject.transform.Translate(0f, -3f, 0f); 
        yield return new WaitForSeconds(time);
        GetComponent<Image>().sprite = normalButtonSprite;
        GetComponentInChildren<Text>().gameObject.transform.Translate(0f, 3f, 0f);
        //GetComponent<Button>().image.sprite = normalButtonSprite;
    }
}
