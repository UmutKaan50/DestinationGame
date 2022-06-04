using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour {
    // Text fields:
    public Text levelText, hitpointText, pesosText, upgradeCostText, xpText;

    // Logic:
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // Character Selection:
    public void OnArrowClick(bool right) {
        if (right) {
            currentCharacterSelection++;
            Debug.Log(currentCharacterSelection + " | " + GameManager.instance.playerSprites.Count);

            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChange();
        } else {
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
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();

    }

    // Upgrade the character information:
    public void UpdateMenu() {
        // Weapon:
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponlevel];
        if (GameManager.instance.weapon.weaponlevel == GameManager.instance.weaponPrices.Count) {
            upgradeCostText.text = "MAX";
        } else {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponlevel].ToString();
        }
        // Meta:
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();
        // Xp bar:
        int currLevel = GameManager.instance.GetCurrentLevel();
        if (currLevel == GameManager.instance.xpTable.Count) {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points."; // Displays total xp.
            xpBar.localScale = Vector3.one;
        } else {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + " / " + diff;
        }
    }

    public Sprite closedChestSprite; // sprite name: menu_0
    public Sprite openedChestSprite; // sprite name: chest_1
    private Image currentChestImage; // The function above acted as we can't assign public Image object inside of it.
    public GameObject menuButton;
    public Image currentAttackButtonImage;
    public Sprite attackButtonDownSprite;
    public Sprite attackButtonUpSprite;


    private void Awake() {
        menuButton = GameObject.Find("MenuButton");
        currentChestImage = GameObject.Find("MenuButton").gameObject.GetComponent<Image>();


    }

    public void ChestSpriteOpen() {
        currentChestImage.sprite = openedChestSprite;
        menuButton.GetComponent<Button>().interactable = false;

    }

    public void ChestSpriteClose() {
        menuButton.GetComponent<Button>().interactable = true;
        currentChestImage.sprite = closedChestSprite;
    }
    private IEnumerator coroutineAttackButtonSprite;

    public void AttackButtonDownSprite() {
        currentAttackButtonImage.sprite = attackButtonDownSprite;
        coroutineAttackButtonSprite = AttackButtonSpriteChange(0.8f);
        StartCoroutine(coroutineAttackButtonSprite);
    }
    
    private IEnumerator AttackButtonSpriteChange(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        currentAttackButtonImage.sprite = attackButtonUpSprite;
    }
}
