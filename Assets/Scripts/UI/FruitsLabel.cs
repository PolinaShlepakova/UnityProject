using UnityEngine;
using UnityEngine.UI;

public class FruitsLabel : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		string text = LevelController.Current.Fruits + "/" + LevelController.Current.FruitsOverall;
		GetComponentInChildren<Text>().text = text;
	}
}
