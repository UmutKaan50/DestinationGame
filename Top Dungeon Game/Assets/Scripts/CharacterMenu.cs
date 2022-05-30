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

            if (currentCharacterSelection == GameManager.instance.playerSprites.Count) {
                currentCharacterSelection = 0;

                OnSelectionChange();
            } else {
                currentCharacterSelection--;

                // If we went too far away:
                if (currentCharacterSelection < 0) {
                    currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
                }

                OnSelectionChange();
            }
        }

    }

    public void OnSelectionChange() {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    }

    // Weapon Upgrade:
    public void OnUpgradeClick() {
        if (GameManager.instance.TryUpgradeWeapon()) {
            UpdateMenu();
        }
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
        levelText.text = "not implemented";
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();
        // Xp bar:
        xpText.text = "not implemented";
        xpBar.localScale = new Vector3(0.5f, 0, 0);
    }
}
