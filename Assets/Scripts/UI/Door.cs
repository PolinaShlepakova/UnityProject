using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour {

	public int Level;
	public Sprite DoorCheck;
	public Sprite DoorEmptyCheck;
	public Sprite DoorCrystal;
	public Sprite DoorEmptyCrystal;
	public Sprite DoorFruit;
	public Sprite DoorEmptyFruit;
	
	// Use this for initialization
	void Start() {
		// get stats
		string str = PlayerPrefs.GetString("stats" + Level, null);
		LevelStat stats = JsonUtility.FromJson<LevelStat>(str);
		if (stats == null) {
			stats = new LevelStat();
		}

		FillHolder("door_check", stats.LevelPassed, DoorCheck, DoorEmptyCheck);
		FillHolder("door_crystal", stats.HasCrystals, DoorCrystal, DoorEmptyCrystal);
		FillHolder("door_fruit", stats.HasAllFruits, DoorFruit, DoorEmptyFruit);
	}

	private void FillHolder(string holderName, bool filled, Sprite filledSprite, Sprite emptySprite) {
		GameObject holder = transform.FindChild(holderName).gameObject;
		holder.GetComponent<Image>().sprite = filled ? filledSprite : emptySprite;
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		HeroRabbit rabbit = collider.GetComponent<HeroRabbit>(); 
		if (rabbit != null) {
			SceneManager.LoadScene("Level" + Level);
		}
	}
}
