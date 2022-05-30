using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    private void Awake() {
        if (GameManager.instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    // Resources:
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References:
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    // Logic: 
    public int pesos;
    public int experience;
    // Floating text:
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }
    // Upgrade weapon: 
    public bool TryUpgradeWeapon() {
        // Is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponlevel) {
            return false;
        }
        if (pesos >= weaponPrices[weapon.weaponlevel]) {
            pesos -= weaponPrices[weapon.weaponlevel];
            weapon.UpgradeWeapon();
            return true;
        }
        // Unless player can efford:
        return false;
    }

    // Save state:
    /*
    INT preferredSkin
    INT pesos
    INT experience
    INT weaponLevel
     
     */
    public void SaveState() {
        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|"; ;
        s += weapon.weaponlevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }
    public void LoadState(Scene s, LoadSceneMode mode) {
        if (!PlayerPrefs.HasKey("SaveState")) {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        // 0|10|15|2
        // Change player skin
        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        // Change the weapon level:
        weapon.SetWeaponLevel(int.Parse(data[3]));

        Debug.Log("LoadState");
    }

}
