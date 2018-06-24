using UnityEngine;

public class SoundManager {
    
    public static SoundManager Instance = new SoundManager();
    private bool _isSoundOn = true;

    public bool isSoundOn() {
        return _isSoundOn;
    }

    public void setSoundOn(bool val) {
        _isSoundOn = val;
        PlayerPrefs.SetInt("sound", _isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    SoundManager() {
        _isSoundOn = PlayerPrefs.GetInt("sound", 1) == 1;
    }

}