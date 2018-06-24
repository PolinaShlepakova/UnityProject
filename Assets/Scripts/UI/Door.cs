using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

	public int Level;
	public Sprite DoorCheck;
	public Sprite DoorEmptyCheck;
	public Sprite DoorCrystal;
	public Sprite DoorEmptyCrystal;
	public Sprite DoorFruit;
	public Sprite DoorEmptyFruit;
	public Sprite DoorLock;
	public Sprite DoorEmptyLock;

	private bool _available;
	
	// Use this for initialization
	void Start() {
		// get stats
		LevelStat stats = GetStats(Level);
		_available = Level == 1 || GetStats(Level - 1).LevelPassed;
		
		FillHolder("door_lock", _available, DoorEmptyLock, DoorLock);
		FillHolder("door_check", stats.LevelPassed, DoorCheck, DoorEmptyCheck);
		FillHolder("door_crystal", stats.HasCrystals, DoorCrystal, DoorEmptyCrystal);
		FillHolder("door_fruit", stats.HasAllFruits, DoorFruit, DoorEmptyFruit);
	}

	private void FillHolder(string holderName, bool filled, Sprite filledSprite, Sprite emptySprite) {
		GameObject holder = transform.Find(holderName).gameObject;
		holder.GetComponent<SpriteRenderer>().sprite = filled ? filledSprite : emptySprite;
	}

	private LevelStat GetStats(int level) {
		string str = PlayerPrefs.GetString("stats" + level, null);
		LevelStat stats = JsonUtility.FromJson<LevelStat>(str);
		if (stats == null) {
			stats = new LevelStat();
		}

		return stats;
	}

	// for testing
	private void CleanStats(int level) {
		string str = JsonUtility.ToJson(new LevelStat());
		PlayerPrefs.SetString("stats" + level, str);
		PlayerPrefs.Save();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		HeroRabbit rabbit = collider.GetComponent<HeroRabbit>(); 
		if (rabbit != null && _available) {
			SceneManager.LoadScene("Level" + Level);
		}
	}
}
