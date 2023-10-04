using UnityEngine;

public class Sensor_Bandit : MonoBehaviour {
    private int m_ColCount;

    private float m_DisableTimer;

    private void Update() {
        m_DisableTimer -= Time.deltaTime;
    }

    private void OnEnable() {
        m_ColCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        m_ColCount++;
    }

    private void OnTriggerExit2D(Collider2D other) {
        m_ColCount--;
    }

    public bool State() {
        if (m_DisableTimer > 0)
            return false;
        return m_ColCount > 0;
    }

    public void Disable(float duration) {
        m_DisableTimer = duration;
    }
}