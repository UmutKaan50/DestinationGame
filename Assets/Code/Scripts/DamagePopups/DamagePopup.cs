using System.Collections;
using System.Collections.Generic;
using Code.Scripts;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

namespace Destination.DamagePopups {
    public class DamagePopup : MonoBehaviour {
        /// <summary>
        /// Creates a damage popup in the world.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="damageAmount"></param>
        /// <param name="colorCode"></param>
        /// <param name="isLeftSide"></param>
        /// <returns></returns>
        public static DamagePopup Create(Vector3 position, string damageAmount, string colorCode, bool isLeftSide) {
            Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);

            DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
            damagePopup.Setup(damageAmount, colorCode, isLeftSide);

            return damagePopup;
        }

        private static int sortingOrder;

        private const float DISAPPEAR_TIMER_MAX = 1.2f; // Will be set to 1f.

        private TextMeshPro textMesh;
        private float disappearTimer;
        private Color textColor;
        private Vector3 moveVector;

        private void Awake() {
            textMesh = transform.GetComponent<TextMeshPro>();
        }

        public void Setup(string damageAmount, string colorCode, bool isLeftSide) {
            textMesh.SetText(damageAmount);

            textColor = UtilsClass.GetColorFromString(colorCode);
            textMesh.color = textColor;
            textMesh.fontSize = 1.2f;

            disappearTimer = DISAPPEAR_TIMER_MAX;
            sortingOrder++;
            textMesh.sortingOrder = sortingOrder;

            // This vector contains move direction as well as speed:

            if (isLeftSide) {
                moveVector = new Vector3(-.5f, 1f);
            }
            else {
                moveVector = new Vector3(.5f, 1f);
            }
            // previously new Vector3(.7f, 1) * 60f;
        }

        private void Update() {
            transform.position += moveVector * Time.deltaTime;
            moveVector -= moveVector * (8f * Time.deltaTime);

            if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f) {
                // First half of the popup:
                float increaseScaleAmount = 1.2f;
                transform.localScale += Vector3.one * (increaseScaleAmount * Time.deltaTime);
            }
            else {
                // Second half of the popup:
                // The thing we need to consider here is that if we alter values in a way that is too much the value dissapering process becomes unwantedly weird.
                float decreaseScaleAmount = 2f;
                float timeMultiplier = 1.2f;

                // while (transform.localScale.x > 0) {
                // }
                transform.localScale -= Vector3.one * (decreaseScaleAmount * Time.deltaTime * timeMultiplier);
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
}