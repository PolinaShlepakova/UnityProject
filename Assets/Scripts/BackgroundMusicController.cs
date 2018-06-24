using UnityEngine;

public class BackgroundMusicController : MonoBehaviour {
	public static BackgroundMusicController Current;
	public AudioClip BgMusic;
	private AudioSource _bgMusicSource;

	// Use this for initialization
	void Awake() {
		Current = this;
		_bgMusicSource = gameObject.AddComponent<AudioSource>();
		_bgMusicSource.clip = BgMusic;
		_bgMusicSource.loop = true;
		if (SoundManager.Instance.IsMusicOn()) {
			_bgMusicSource.Play();
		}
	}

	public void Play() {
		_bgMusicSource.Play();
	}

	public void Pause() {
		_bgMusicSource.Pause();
	}
}
