using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthVisual : MonoBehaviour
{
    private EnemyNew enemy;
    [SerializeField] private Slider healthSlider;

    private void Awake()
    {
        enemy = GetComponent<EnemyNew>();
    }

    private IEnumerator Start()
    {
        float initialWaitingTime = 1f;
        yield return new WaitForSeconds(initialWaitingTime);
        SetSliderInitialValues();

        enemy.OnRecieveDamage += UpdateHealthSlider;
    }

    private void UpdateHealthSlider()
    {
        Debug.Log("Enemy health: " + enemy.GetHitpoint());
        healthSlider.value = enemy.GetHitpoint();
        Debug.Log("Enemy health: " + enemy.GetHitpoint());
    }

    private void SetSliderInitialValues()
    {
        healthSlider.maxValue = enemy.GetHitpoint();
        healthSlider.minValue = 0;
        healthSlider.value = enemy.GetHitpoint();
    }
}