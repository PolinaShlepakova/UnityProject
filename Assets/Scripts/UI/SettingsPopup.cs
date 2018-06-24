using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour {

	public static SettingsPopup Current;
	public Sprite SoundOn;
	public Sprite SoundOff;
	public Sprite MusicOn;
	public Sprite MusicOff;

	// Use this for initialization
	void Start () {
		Current = this;
		gameObject.SetActive(false);
	}

	public void Init() {
		SetSoundImage(SoundManager.Instance.IsSoundOn());
		SetMusicImage(SoundManager.Instance.IsMusicOn());
	}
	
	public void Show() {
		Init();
		gameObject.SetActive(true);
	}
	
	public void Hide() {
		gameObject.SetActive(false);
	}

	public void SetSoundImage(bool soundOn) {
		SetButtonImage("SoundButton", soundOn ? SoundOn : SoundOff);
	}

	public void SetMusicImage(bool musicOn) {
		SetButtonImage("MusicButton", musicOn ? MusicOn : MusicOff);
	}

	private void SetButtonImage(string buttonName, Sprite image) {
		GameObject window = transform.Find("Window").gameObject;
		window.transform.Find(buttonName).gameObject.GetComponent<Image>().sprite = image;
	}
	
}
