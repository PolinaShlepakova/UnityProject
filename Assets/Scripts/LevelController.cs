using UnityEngine;

public class LevelController : MonoBehaviour {
	
	public static LevelController Current;
	
	private Vector3 _startingPosition;
	private int _coins;
	private int _fruits;
	private bool _crystalRed;
	private bool _crystalBlue;
	private bool _crystalGreen;

	void Awake() {
		Current = this;
	}
	
	public void SetStartPosition(Vector3 pos) {
		_startingPosition = pos;
	}
	
	public void OnRabbitDeath(HeroRabbit rabbit) {
		rabbit.Revive();
		// on rabbit death return to the starting position
		rabbit.transform.position = _startingPosition;
	}

	public void AddCoin() {
		_coins++;
	}
	
	public void AddFruit() {
		_fruits++;
	}

	public void AddCrystal(Crystal.CrystalColor color) {
		switch (color) {
			case Crystal.CrystalColor.Red:
				_crystalRed = true;
				break;
			case Crystal.CrystalColor.Blue:
				_crystalBlue = true;
				break;
			case Crystal.CrystalColor.Green:
				_crystalGreen = true;
				break;
		}
	}
	
	public bool CrystalGreen {
		get { return _crystalGreen; }
	}

	public bool CrystalBlue {
		get { return _crystalBlue; }
	}

	public bool CrystalRed {
		get { return _crystalRed; }
	}

	public int Fruits {
		get { return _fruits; }
	}

	public int Coins {
		get { return _coins; }
	}
}
