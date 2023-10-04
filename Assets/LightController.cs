using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour {
    public Light2D explosionLight;
    public float explosionLightIntensity;

    private void Start() {
        DOVirtual.Float(0, explosionLightIntensity, .05f, ChangeLight)
            .OnComplete(() => DOVirtual.Float(explosionLightIntensity, 0, .1f, ChangeLight));
    }

    private void ChangeLight(float x) {
        explosionLight.intensity = x;
    }
}