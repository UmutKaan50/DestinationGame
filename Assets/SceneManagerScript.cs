using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour {
    public GameObject mainPanel;
    public GameObject lvlPanel;
    public GameObject fadePanel;
    public GameObject lvl2LockedButton;
    public GameObject lvl2UnlockedButton;
    private bool animationActivated;
    private bool animationReverseActivated;
    private float fadeRatio;

    private int lvlMenuorMainMenu; //3= loadAnotherScene 2=lvl menu, 1= mainMenu(for animation)

    // Start is called before the first frame update
    private void Start() {
        if (PlayerPrefs.GetInt("level") > 1) activateLvl2();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (lvlMenuorMainMenu > 0) PlayAnimation();
    }

    public void detectLevel() {
        var a = PlayerPrefs.GetInt("level");
        if (a == 2) activateLvl2();
    }

    public void activateLvl2() {
        lvl2LockedButton.SetActive(false);
        lvl2UnlockedButton.SetActive(true);
    }

    public void LoadLvlMenu() {
        animationActivated = true;
        lvlMenuorMainMenu = 2;
        fadePanel.active = true;
    }

    public void LoadMainMenu() {
        animationActivated = true;
        lvlMenuorMainMenu = 1;
        fadePanel.active = true;
    }

    public void PlayAnimation() {
        if (animationActivated && fadeRatio < 1) {
            fadePanel.GetComponent<Image>().color = new Color(fadePanel.GetComponent<Image>().color.r,
                fadePanel.GetComponent<Image>().color.g, fadePanel.GetComponent<Image>().color.b, fadeRatio);
            fadeRatio += 0.1f;
        }
        else if (fadeRatio >= 1 && !animationReverseActivated) {
            animationReverseActivated = true;
            animationActivated = false;
            if (lvlMenuorMainMenu == 1) {
                mainPanel.active = true;
                lvlPanel.active = false;
            }
            else if (lvlMenuorMainMenu == 2) {
                lvlPanel.active = true;
                mainPanel.active = false;
            }
            else if (lvlMenuorMainMenu == 3) {
                SceneManager.LoadScene("Lvl1");
            }
        }
        else if (animationReverseActivated && fadeRatio > 0) {
            fadePanel.GetComponent<Image>().color = new Color(fadePanel.GetComponent<Image>().color.r,
                fadePanel.GetComponent<Image>().color.g, fadePanel.GetComponent<Image>().color.b, fadeRatio);
            fadeRatio -= 0.1f;
        }
        else if (fadeRatio <= 0 && animationReverseActivated) {
            animationReverseActivated = false;
            fadePanel.active = false;
            lvlMenuorMainMenu = 0;
        }
    }

    public void LoadLvl1() {
        animationActivated = true;
        lvlMenuorMainMenu = 3;
        fadePanel.active = true;
        SceneManager.LoadScene("Lvl1");
    }

    public void LoadLvl2() {
        animationActivated = true;
        lvlMenuorMainMenu = 3;
        fadePanel.active = true;
        SceneManager.LoadScene("Lvl2");
    }

    public void LoadNextLvl() {
        var lvlno = Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value;
        if (int.Parse(lvlno) < 6)
            SceneManager.LoadScene("Lvl" + Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value);
    }
}