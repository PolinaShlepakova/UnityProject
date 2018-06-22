using UnityEngine;
using UnityEngine.UI;

public class FruitsLabel : MonoBehaviour {

	public int NFruits = 9;

	private string _trailingText;

	void Start() {
		_trailingText = "/" + NFruits;
	}

	// Update is called once per frame
	void Update () {
		GetComponentInChildren<Text>().text = LevelController.Current.Fruits + _trailingText;
	}
}
