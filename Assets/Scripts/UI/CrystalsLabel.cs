using UnityEngine;
using UnityEngine.UI;

public class CrystalsLabel : MonoBehaviour {
	public Sprite BlueCrystal;
	public Sprite GreenCrystal;
	public Sprite RedCrystal;
	
	private GameObject[] _crystalHolders;

	private static readonly int NCrystals = 3;

	// Use this for initialization
	void Start () {
		_crystalHolders = new GameObject[NCrystals];
		for (int i = 0; i < NCrystals; i++) {
			_crystalHolders[i] = transform.GetChild(i).gameObject;
		}
	}

	// Update is called once per frame
	void Update () {
		if (LevelController.Current.CrystalBlue) {
			_crystalHolders[0].GetComponent<Image>().sprite = BlueCrystal;
		} 
		if (LevelController.Current.CrystalGreen) {
			_crystalHolders[1].GetComponent<Image>().sprite = GreenCrystal;
		}
		if (LevelController.Current.CrystalRed) {
			_crystalHolders[2].GetComponent<Image>().sprite = RedCrystal;
		}
	}
	
}
