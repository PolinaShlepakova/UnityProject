using UnityEngine;

public class Collectable : MonoBehaviour {
    protected virtual void FixedUpdate() {
        
    }

    protected virtual void OnRabbitHit(HeroRabbit rabbit) {
        CollectedHide();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        // if (!this.hideAnimation) {
        HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
        if (rabbit != null) {
            OnRabbitHit(rabbit);
        }
        // }
    }

    public void CollectedHide() {
        Destroy(gameObject);
    }
}