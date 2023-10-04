using UnityEngine;

// [RequireComponent(typeof(BoxCollider2D))]
public class Collideable : MonoBehaviour {
    public ContactFilter2D filter;
    private readonly Collider2D[] hits = new Collider2D[10];
    private BoxCollider2D boxCollider;

    protected virtual void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update() {
        // Collision work:
        boxCollider.OverlapCollider(filter, hits);
        for (var i = 0; i < hits.Length; i++) {
            if (hits[i] == null) continue;

            OnCollide(hits[i]);
            // The array is not cleaned up, so we do it ourself:
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll) {
        // Debug.Log("OnCollide was not implemented in " + this.name);
    }
}