using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using y01cu;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    // Resources:
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References:
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    private RectTransform hitpointBar;
    public Animator deathMenuAnim;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject Menu;

    // Logic: 
    public int money;

    public int experience;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager);
            Destroy(HUD);
            Destroy(Menu);
            return;
        }

        FindAndAssignSomeGameObjectsAndComponents();

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;

        //DontDestroyOnLoad(gameObject);


        Invoke("SpawnFullScreenMessage", 7f);
        Invoke("SpawnFullScreenMessage", 11f);
    }

    private void FindAndAssignSomeGameObjectsAndComponents() {
        HUD = GameObject.Find("HUD");
        hitpointBar = HUD.transform.Find("HealthBar").transform.Find("Health").GetComponent<RectTransform>();
        Menu = GameObject.Find("Menu");
        player = GameObject.Find("Player").GetComponent<Player>();
        deathMenuAnim = HUD.transform.Find("DeathMenu").GetComponent<Animator>();
    }

    // Floating text:
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // Upgrade weapon: 
    public bool TryUpgradeWeapon() {
        // Is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponlevel) return false;

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
        var ratio = player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }


    // Experience system:
    public int GetXpToLevel(int level) {
        var r = 0;
        var xp = 0;

        while (r < level) {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }

    public int GetCurrentLevel() {
        var r = 0;
        var add = 0;

        while (experience >= add) {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) // Max level
                return r;
        }

        return r;
    }

    public void GrantXp(int xp) {
        var currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel()) OnLevelUp();
    }

    public void OnLevelUp() {
        Debug.Log("level up!");
        SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.levelUp);
        player.OnLevelUp();
        OnHitpointChange();
    }

    public void OnSceneLoaded(Scene s, LoadSceneMode mode) {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        FindAndAssignSomeGameObjectsAndComponents();
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.Respawn();
    }

    [SerializeField] private GameObject pfMessagePanel;

    private void SpawnFullScreenMessage() {
        MessagePanel messagePanel =
            Instantiate(pfMessagePanel, pfMessagePanel.transform.position, Quaternion.identity)
                .GetComponent<MessagePanel>();
        messagePanel.SetMessage("Don't forget to toggle on fullscreen in order to play the game properly.");
    }

    public void SaveState() {
        var s = "";

        s += "0" + "|";
        s += money + "|";
        s += experience + "|";
        ;
        s += weapon.weaponlevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode) {
        // SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState")) return;


        // Like loading previous session:

        var data = PlayerPrefs.GetString("SaveState").Split('|');
        // 0|10|15|2
        // Change player skin
        money = int.Parse(data[1]);
        // Experience:
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        // Change the weapon level: 
        weapon.SetWeaponLevel(int.Parse(data[3]));
    }
}