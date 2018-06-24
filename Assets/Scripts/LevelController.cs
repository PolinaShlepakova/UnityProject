using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	public int MaxLives = 3;
	public int Level;
	public AudioClip WinLevelSound;
	public AudioClip LoseLevelSound;
	
	public static LevelController Current;
	
	private Vector3 _startingPosition;
	private int _coins;
	private int _fruits;
	private bool _crystalRed;
	private bool _crystalBlue;
	private bool _crystalGreen;
	private int _lives = 3;
	private LevelStat _stats;
	private int _fruitsOverall;
	private string _statsKey;

	private float _savedTimeScale;

	private AudioSource _winLevelSource;
	private AudioSource _loseLevelSource;

	public int FruitsOverall {
		get { return _fruitsOverall; }
		set { _fruitsOverall = value; }
	}

	void Awake() {
		Current = this;
		_statsKey = "stats" + Level;
		string str = PlayerPrefs.GetString(_statsKey, null);
		_stats = JsonUtility.FromJson<LevelStat>(str);
		if (_stats == null) {
			_stats = new LevelStat();
		}

		_fruits = _stats.CollectedFruits.Count;
		
		_winLevelSource = gameObject.AddComponent<AudioSource>();
		_winLevelSource.clip = WinLevelSound;
		_loseLevelSource = gameObject.AddComponent<AudioSource>();
		_loseLevelSource.clip = LoseLevelSound;
	}
	
	public void SetStartPosition(Vector3 pos) {
		_startingPosition = pos;
	}
	
	public void OnRabbitDeath(HeroRabbit rabbit) {
		if (--_lives > 0) {
			rabbit.Revive();
			// on rabbit death return to the starting position
			rabbit.transform.position = _startingPosition;
		}
		else {
			FailLevel();
		}
	}

	public void AddCoin() {
		_coins++;
	}
	
	public void AddFruit(int id) {
		if (!IsFruitCollectedBefore(id)) {
			_fruits++;
			_stats.CollectedFruits.Add(id);
		}
	}

	public bool IsFruitCollectedBefore(int id) {
		return _stats.CollectedFruits.Contains(id);
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

	public void PassLevel() {
		if (SoundManager.Instance.IsSoundOn()) {
			_winLevelSource.Play();
		}
		// save coins
		int prevCoins = PlayerPrefs.GetInt("coins", 0);
		PlayerPrefs.SetInt("coins", _coins + prevCoins);
		
		// update stats
		_stats.LevelPassed = true;
		_stats.HasCrystals = _crystalBlue && _crystalGreen && _crystalRed || _stats.HasCrystals;
		_stats.HasAllFruits = _stats.CollectedFruits.Count == FruitsOverall;
		// save stats
		string str = JsonUtility.ToJson(_stats);
		PlayerPrefs.SetString(_statsKey, str);
		
		PlayerPrefs.Save();
		
		Pause();
		WinLevel.Current.Show();
	}
	
	public void FailLevel() {
		if (SoundManager.Instance.IsSoundOn()) {
			_loseLevelSource.Play();
		}
		Pause();
		LoseLevel.Current.Show();
	}
	
	public void Pause() {
		_savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		// AudioListener.pause = true;
	}
  
	public void UnPause() {
		Time.timeScale = _savedTimeScale;
		// AudioListener.pause = false;
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

	public int Lifes {
		get { return _lives; }
	}
}
