using UnityEngine;
using UnityEngine.UI;

public class MoneyLabelLevel : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		GetComponentInChildren<Text>().text = LevelController.Current.Coins.ToString("0000");
	}
}
