using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseLevel : MonoBehaviour {

	public static LoseLevel Current;

	// Use this for initialization
	void Start () {
		Current = this;
		gameObject.SetActive(false);
	}
	
	public void Show() {
		gameObject.SetActive(true);
	}

	public void OnCloseButtonClicked() {
		OnMenuButtonClicked();
	}

	public void OnReplayButtonClicked() {
		LevelController.Current.UnPause();
		SceneManager.LoadScene("Level" + LevelController.Current.Level);
	}

	public void OnMenuButtonClicked() {
		LevelController.Current.UnPause();
		SceneManager.LoadScene("ChooseLevel");
	}
}
