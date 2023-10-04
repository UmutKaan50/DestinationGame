using UnityEngine;
using UnityEngine.UI;

public class FloatingText {
    public bool active;
    public float duration;
    public GameObject go;
    public float lastShown;
    public Vector3 motion;
    public Text txt;

    public void Show() {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    public void Hide() {
        active = false;
        go.SetActive(active);
    }

    public void UpdateFloatingText() {
        if (!active)
            return;

        //     10     -     7     >     2
        // Showing text long enough:
        if (Time.time - lastShown > duration) Hide();


        //go.transform.position += motion * Time.deltaTime;
        //// Instead of the transforming upwards I can animate it:
        //go.GetComponent<Animator>().SetTrigger("show");
        //go.GetComponent<Animation>().GetClip(go.GetComponent<Animator>().clip).length;
        //AnimatorClipInfo info = go.GetComponent<AnimatorClipInfo>();
        //info.
    }
}