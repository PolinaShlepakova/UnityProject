using UnityEngine;

public class Fruit : Collectable {

	private static readonly Color CollectedColor = new Color(1, 1, 1, 0.6f);
	private int _id;

	void Start() {
		_id = ++LevelController.Current.FruitsOverall;
		if (LevelController.Current.IsFruitCollectedBefore(_id)) {
			GetComponent<SpriteRenderer>().color = CollectedColor;
		}
	}
    
	// when rabbit collects fruit, it disappears and fruit count increases
	protected override void OnRabbitHit(HeroRabbit rabbit) {
		LevelController.Current.AddFruit(_id);
		CollectedHide();
	}
}