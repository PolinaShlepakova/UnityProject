public class Bomb : Collectable {
    
	// when rabbit collects mushroom, it disappears
	// if rabbit is small, he dies
	// if he is big, he becomes smaller
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
}
