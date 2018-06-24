using UnityEngine;

public class Crystal : Collectable {

	public enum CrystalColor {
		Red,
		Blue,
		Green
	};

	public CrystalColor Color;

	void Start() {
		Audio = GetComponent<AudioSource>();
	}

	// when rabbit collects crystal of certain color, it disappears and crystal of that color count increases
	protected override void OnRabbitHit(HeroRabbit rabbit) {
		LevelController.Current.AddCrystal(Color);
		CollectedHide();
	}
}
