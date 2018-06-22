using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyLabel : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		GetComponentInChildren<Text>().text = LevelController.Current.Coins.ToString("0000");
	}
}
