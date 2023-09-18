namespace Destination.Enemies {
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class EnemyHealthVisual : MonoBehaviour {
        private EnemyNew enemy;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image uiEnemyImageArea;
        [SerializeField] private Sprite enemyPortait;
        [SerializeField] private GameObject enemyVisuals;


        private void Awake() {
            enemy = GetComponent<EnemyNew>();
        }

        private IEnumerator Start() {
            float initialWaitingTime = 1f;
            yield return new WaitForSeconds(initialWaitingTime);
            SetSliderInitialValues();

            enemy.OnRecieveDamage += EnableEnemyVisuals;
            enemy.OnRecieveDamage += UpdateHealthSlider;
            enemy.OnRecieveDamage += UpdateEnemyImageArea;
            
            // TODO: Disable enemy visuals when needed.
            
        }

        private void EnableEnemyVisuals() {
            enemyVisuals.SetActive(true);
        }

        private void UpdateHealthSlider() {
            Debug.Log("Enemy health: " + enemy.GetHitpoint());
            healthSlider.value = enemy.GetHitpoint();
            Debug.Log("Enemy health: " + enemy.GetHitpoint());
        }

        private void UpdateEnemyImageArea() {
            uiEnemyImageArea.sprite = enemyPortait;
        }

        private void SetSliderInitialValues() {
            healthSlider.maxValue = enemy.GetHitpoint();
            healthSlider.minValue = 0;
            healthSlider.value = enemy.GetHitpoint();
        }
    }
}