using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLevel : MonoBehaviour {

	public static WinLevel Current;
	
	public Sprite BlueCrystal;
	public Sprite GreenCrystal;
	public Sprite RedCrystal;

	// Use this for initialization
	void Start () {
		Current = this;
		gameObject.SetActive(false);
	}

	private void Init() {
		GameObject window = transform.Find("Window").gameObject;
		// init crystals
		GameObject crystalsHolder = window.transform.Find("CrystalsRibbon").gameObject;
		if (LevelController.Current.CrystalBlue) {
			crystalsHolder.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = BlueCrystal;	
		}
		if (LevelController.Current.CrystalGreen) {
			crystalsHolder.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = GreenCrystal;	
		}
		if (LevelController.Current.CrystalRed) {
			crystalsHolder.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = RedCrystal;	
		}
		// init fruits
		string fruits = LevelController.Current.Fruits + "/" + LevelController.Current.FruitsOverall;
		window.transform.Find("FruitsLabel").gameObject.GetComponentInChildren<Text>().text = fruits;
		// init coins
		string coins = "+" + LevelController.Current.Coins;
		window.transform.Find("CoinsLabel").gameObject.GetComponentInChildren<Text>().text = coins;
		
	}
	
	public void Show() {
		Init();
		gameObject.SetActive(true);
	}

	public void OnCloseButtonClicked() {
		OnMenuButtonClicked();
	}

	public void OnReplayButtonClicked() {
		SceneManager.LoadScene("Level" + LevelController.Current.Level);
	}

	public void OnMenuButtonClicked() {
		SceneManager.LoadScene("ChooseLevel");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
