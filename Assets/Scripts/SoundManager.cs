using UnityEngine;

public class SoundManager {
    
    public static SoundManager Instance = new SoundManager();
    private bool _isSoundOn;
    private bool _isMusicOn;

    public bool IsSoundOn() {
        return _isSoundOn;
    }

    public bool IsMusicOn() {
        return _isMusicOn;
    }

    public void SetSoundOn(bool val) {
        _isSoundOn = val;
        PlayerPrefs.SetInt("sound", _isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetMusicOn(bool val) {
        _isMusicOn = val;
        PlayerPrefs.SetInt("music", _isMusicOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    SoundManager() {
        _isSoundOn = PlayerPrefs.GetInt("sound", 1) == 1;
        _isMusicOn = PlayerPrefs.GetInt("music", 1) == 1;
    }

}