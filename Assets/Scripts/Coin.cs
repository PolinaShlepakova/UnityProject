using UnityEngine;

public class Coin : Collectable {
    
    void Start() {
        Audio = GetComponent<AudioSource>();
    }

    // when rabbit collects coin, it disappears and coin count increases
    protected override void OnRabbitHit(HeroRabbit rabbit) {
        LevelController.Current.AddCoin();
        CollectedHide();
    }
}
