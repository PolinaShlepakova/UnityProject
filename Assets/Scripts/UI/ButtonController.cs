using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

	public AudioClip ButtonClickSound;
	
	private float _savedTimeScale;
	
	private AudioSource _clickSoundSource;

	void Awake() {
		_clickSoundSource = gameObject.AddComponent<AudioSource>();
		_clickSoundSource.clip = ButtonClickSound;
	}
	
	// Update is called once per frame
	public void ChangeToScene(string scene) {
		PlayClickSound();
		SceneManager.LoadScene(scene);
	}

	public void ChangeToSceneAndUnPause(string scene) {
		LevelController.Current.UnPause();
		ChangeToScene(scene);
	}

	public void SettingsOpened() {
		PlayClickSound();
		_savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		SettingsPopup.Current.Show();
	}

	public void SettingsClosed() {
		PlayClickSound();
		Time.timeScale = _savedTimeScale;
		SettingsPopup.Current.Hide();
	}

	public void SoundClicked() {
		PlayClickSound();
		bool soundOn = !SoundManager.Instance.IsSoundOn();
		SoundManager.Instance.SetSoundOn(soundOn);
		SettingsPopup.Current.SetSoundImage(soundOn);
	}
	
	public void MusicClicked() {
		PlayClickSound();
		bool musicOn = !SoundManager.Instance.IsMusicOn();
		SoundManager.Instance.SetMusicOn(musicOn);
		SettingsPopup.Current.SetMusicImage(musicOn);
		if (musicOn) {
			BackgroundMusicController.Current.Play();
		}
		else {
			BackgroundMusicController.Current.Pause();
		}
	}

	private void PlayClickSound() {
		if (SoundManager.Instance.IsSoundOn()) {
			_clickSoundSource.Play();
		}
	}
}
