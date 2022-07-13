using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    private void Awake() {
        if (GameManager.instance != null) {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager);
            Destroy(hud);
            Destroy(menu);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
        //DontDestroyOnLoad(gameObject);
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
    public RectTransform hitpointBar;
    public Animator deathMenuAnim;
    public GameObject hud;
    public GameObject menu;
    // Logic: 
    public int money;
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
        if (money >= weaponPrices[weapon.weaponlevel]) {
            money -= weaponPrices[weapon.weaponlevel];
            weapon.UpgradeWeapon();
            return true;
        }
        // Unless player can efford:
        return false;
    }
    // Hitpoint bar:
    public void OnHitpointChange() {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }    


    // Experience system:
    public int GetXpToLevel(int level) {
        int r = 0;
        int xp = 0;

        while (r < level) {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }
    public int GetCurrentLevel() {
        int r = 0;
        int add = 0;

        while (experience >= add) {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) // Max level
                return r;
        }
        return r;
    }
    public void GrantXp(int xp) {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel()) {
            OnLevelUp();
        }
    }
    public void OnLevelUp() {
        Debug.Log("level up!");
        SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.levelUp);
        player.OnLevelUp();
        OnHitpointChange();
    }
    public void OnSceneLoaded(Scene s, LoadSceneMode mode) {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    // Save state:
    /*
    INT preferredSkin
    INT money
    INT experience
    INT weaponLevel
     
     */
    // Death menu and respawn:
    public void Respawn() {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn( );
    }

    public void SaveState() {
        string s = "";

        s += "0" + "|";
        s += money.ToString() + "|";
        s += experience.ToString() + "|"; ;
        s += weapon.weaponlevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }
    public void LoadState(Scene s, LoadSceneMode mode) {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState")) {
            return;
        }
        // Like loading previous session:

        //string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        //// 0|10|15|2
        //// Change player skin
        //money = int.Parse(data[1]);
        //// Experience:
        //experience = int.Parse(data[2]);
        //if (GetCurrentLevel() != 1)
        //    player.SetLevel(GetCurrentLevel());
        //// Change the weapon level: 
        //weapon.SetWeaponLevel(int.Parse(data[3]));

    }

}
