using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	public AudioClip ButtonClickSound;
	private AudioSource _clickSoundSource;

	void Awake() {
		_clickSoundSource = gameObject.AddComponent<AudioSource>();
		_clickSoundSource.clip = ButtonClickSound;
	}
	
	// Update is called once per frame
	public void ChangeToScene(string scene) {
		if (SoundManager.Instance.IsSoundOn()) {
			_clickSoundSource.Play();
		}
		SceneManager.LoadScene(scene);
	}

	public void ChangeToSceneAndUnPause(string scene) {
		LevelController.Current.UnPause();
		ChangeToScene(scene);
	}
}
