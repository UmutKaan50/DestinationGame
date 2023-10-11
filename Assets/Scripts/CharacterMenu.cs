using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour {
    // Text fields:
    public Text levelText, hitpointText, moneyText, upgradeCostText, xpText;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    public Sprite closedChestSprite; // sprite name: menu_0
    public Sprite openedChestSprite; // sprite name: chest_1
    public Button menuButton;
    public GameObject attackButton;
    public Image currentAttackButtonImage;
    public Sprite attackButtonDownSprite;
    public Sprite attackButtonUpSprite;

    private IEnumerator coroutineAttackButtonSprite;

    // Logic:
    private int currentCharacterSelection;
    public Image currentChestImage; // The function above acted as we can't assign public Image object inside of it.

    private void Awake() {
        InvokeRepeating("FindAndAssignComponentsAndGameObjects", 2f, 1);
        // TODO: Find the best way to detect gameobjects in similar conditions like above and stick to it till you find a better one.
    }

    private CharacterMenu characterMenu;

    private void FindAndAssignComponentsAndGameObjects() {
        characterMenu = GameObject.Find("Canvas_CharacterMenu").GetComponent<CharacterMenu>();
        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();

        currentChestImage = menuButton.GetComponent<Image>();

        menuButton.onClick.AddListener(() => {
            characterMenu.GetComponent<Animator>().SetTrigger("show");
            characterMenu.UpdateMenu();
            characterMenu.ChestSpriteOpen();
        });
    }

    // Character Selection:
    public void OnArrowClick(bool right) {
        if (right) {
            currentCharacterSelection++;
            Debug.Log(currentCharacterSelection + " | " + GameManager.instance.playerSprites.Count);

            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChange();
        }
        else {
            currentCharacterSelection--;

            // If we went too far away:
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;


            OnSelectionChange();
        }
    }

    public void OnSelectionChange() {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weapon Upgrade:
    public void OnUpgradeClick() {
        if (GameManager.instance.TryUpgradeWeapon()) {
            SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.buySound);
            UpdateMenu();
        }
    }

    // Upgrade the character information:
    public void UpdateMenu() {
        // Weapon:
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponlevel];
        if (GameManager.instance.weapon.weaponlevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text =
                GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponlevel].ToString();

        // Meta:
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        moneyText.text = GameManager.instance.money.ToString();
        // Xp bar:
        var currLevel = GameManager.instance.GetCurrentLevel();
        if (currLevel == GameManager.instance.xpTable.Count) {
            xpText.text =
                GameManager.instance.experience + " total experience points."; // Displays total xp.
            xpBar.localScale = Vector3.one;
        }
        else {
            var prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            var currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            var diff = currLevelXp - prevLevelXp;
            var currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            var completionRatio = currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel + " / " + diff;
        }
    }

    public void ChestSpriteOpen() {
        SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.chestOpen);
        menuButton.GetComponent<Button>().interactable = false;
        currentChestImage.sprite = openedChestSprite;
    }

    public void ChestSpriteClose() {
        SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.chestClose);
        menuButton.GetComponent<Button>().interactable = true;
        currentChestImage.sprite = closedChestSprite;
    }

    //public void AttackButtonDownSprite() {

    //    currentAttackButtonImage.sprite = attackButtonDownSprite;
    //    coroutineAttackButtonSprite = AttackButtonSpriteChange(0.7f);
    //    StartCoroutine(coroutineAttackButtonSprite);
    //}

    private IEnumerator AttackButtonSpriteChange(float waitTime) {
        //attackButton.SetActive(false);
        attackButton.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(waitTime);
        attackButton.GetComponent<Button>().interactable = true;
        //attackButton.SetActive(true);
        currentAttackButtonImage.sprite = attackButtonUpSprite;
    }
}