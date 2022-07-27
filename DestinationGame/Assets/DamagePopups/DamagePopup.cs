using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class DamagePopup : MonoBehaviour {
    // Create a damage popup:
    public static DamagePopup Create(Vector3 position, int damageAmount, string colorCode, bool isLeftSide) {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, colorCode, isLeftSide);



        return damagePopup;
    }
    private static int sortingOrder;



    private const float DISAPPEAR_TIMER_MAX = 1f; // Will be set to 1f.

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;
    private void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int damageAmount, string colorCode, bool isLeftSide) {

        textMesh.SetText(damageAmount.ToString());

        textColor = UtilsClass.GetColorFromString(colorCode);
        textMesh.fontSize = .7f;

        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        // This vector contains move direction as well as speed:

        if (isLeftSide) {
            moveVector = new Vector3(-.5f, 1f);
        } else {
            moveVector = new Vector3(.5f, 1f);
        }
           
        
        // previously new Vector3(.7f, 1) * 60f;
    }

    private void Update() {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f) {
            // First half of the popup:
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        } else {
            // Second half of the popup:
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }


        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0) {
            // Start disappearing:
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0) {
                Destroy(gameObject);
            }
        }
    }
}
