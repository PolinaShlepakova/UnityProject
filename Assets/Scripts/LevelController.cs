using UnityEngine;

public class LevelController : MonoBehaviour {
	
	public static LevelController Current;
	
	private Vector3 _startingPosition;	
	
	void Awake() {
		Current = this;
	}
	
	public void SetStartPosition(Vector3 pos) {
		_startingPosition = pos;
	}
	
	public void OnRabbitDeath(HeroRabbit rabbit) {
		// on rabbit death return to the starting position
		rabbit.transform.position = _startingPosition;
	}
}
