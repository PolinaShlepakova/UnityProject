using UnityEngine;
using UnityEngine.UI;

public class MoneyLabel : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		int coins = PlayerPrefs.GetInt("coins", 0);
		GetComponentInChildren<Text>().text = coins.ToString("0000");
	}
}