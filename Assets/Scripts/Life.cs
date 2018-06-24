using UnityEngine;

public class Life : Collectable {
    
	void Start() {
		Audio = GetComponent<AudioSource>();
	}

	protected override void OnRabbitHit(HeroRabbit rabbit) {
		LevelController.Current.AddLife();
		CollectedHide();
	}
}
