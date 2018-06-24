using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {
	
	// Update is called once per frame
	public void ChangeToScene(string scene) {
		SceneManager.LoadScene(scene);
	}

	public void ChangeToSceneAndUnPause(string scene) {
		LevelController.Current.UnPause();
		ChangeToScene(scene);
	}
}
