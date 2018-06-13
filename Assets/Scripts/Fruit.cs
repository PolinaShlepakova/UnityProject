public class Fruit : Collectable {
    
	// when rabbit collects fruit, it disappears and fruit count increases
	protected override void OnRabbitHit(HeroRabbit rabbit) {
		LevelController.Current.AddFruit();
		CollectedHide();
	}
}