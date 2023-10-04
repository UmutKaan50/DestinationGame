using CodeMonkey.Utils;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour {
    private const float DISAPPEAR_TIMER_MAX = 1f; // Will be set to 1f.
    private static int sortingOrder;
    private float disappearTimer;
    private Vector3 moveVector;
    private Color textColor;

    private TextMeshPro textMesh;

    private void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    private void Update() {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f) {
            // First half of the popup:
            var increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else {
            // Second half of the popup:
            var decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }


        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0) {
            // Start disappearing:
            var disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0) Destroy(gameObject);
        }
    }

    // Create a damage popup:
    public static DamagePopup Create(Vector3 position, string damageAmount, string colorCode, bool isLeftSide) {
        var damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);

        var damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, colorCode, isLeftSide);


        return damagePopup;
    }

    public void Setup(string damageAmount, string colorCode, bool isLeftSide) {
        textMesh.SetText(damageAmount);

        textColor = UtilsClass.GetColorFromString(colorCode);
        textMesh.fontSize = .7f;

        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        // This vector contains move direction as well as speed:

        if (isLeftSide)
            moveVector = new Vector3(-.5f, 1f);
        else
            moveVector = new Vector3(.5f, 1f);


        // previously new Vector3(.7f, 1) * 60f;
    }
}