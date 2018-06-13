public class Coin : Collectable {
    
    // when rabbit collects coin, it disappears and coin count increases
    protected override void OnRabbitHit(HeroRabbit rabbit) {
        LevelController.Current.AddCoin();
        CollectedHide();
    }
}
