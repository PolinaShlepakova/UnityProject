public class Mushroom : Collectable {
    
	// when rabbit collects mushroom, it disappears and rabbit grows (if he is not big already)
	protected override void OnRabbitHit(HeroRabbit rabbit) {
		rabbit.Grow();
		CollectedHide();
	}
}
