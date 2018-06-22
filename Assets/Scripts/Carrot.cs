using System.Collections;
using UnityEngine;

public class Carrot : Collectable {

    public float Lifespan = 3;
    public float Speed = 1;
    
    void Start() {
        StartCoroutine(DestroyLater());
    }

    private IEnumerator DestroyLater() {
        yield return new WaitForSeconds(Lifespan);
        CollectedHide();
    }

    protected override void OnRabbitHit(HeroRabbit rabbit) {
        if (!rabbit.IsBombInvulnerable) {
            if (rabbit.IsBig) {
                rabbit.Diminish();
            }
            else {
                rabbit.Die();
            }
            CollectedHide();
        }
    }

    public void Launch(float direction) {
        if (direction < 0) {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction * Speed, 0);
    }
}