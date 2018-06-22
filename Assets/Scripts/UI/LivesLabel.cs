using UnityEngine;
using UnityEngine.UI;

public class LivesLabel : MonoBehaviour {
	public Sprite Life;
	public Sprite EmptyLife;
	public int MaxLives = 3; 

	private GameObject[] _lifeHolders;


	// Use this for initialization
	void Start () {
		_lifeHolders = new GameObject[MaxLives];
		for (int i = 0; i < MaxLives; i++) {
			_lifeHolders[i] = transform.GetChild(i).gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		int lives = LevelController.Current.Lifes;
		Debug.Assert(lives >= 0 && lives <= MaxLives);
		
		// fill full lives
		for (int i = 0; i < lives; i++) {
			_lifeHolders[i].GetComponent<Image>().sprite = Life;
		}
		// fill empty lives
		for (int i = lives; i < MaxLives; i++) {
			_lifeHolders[i].GetComponent<Image>().sprite = EmptyLife;
		}
	}
}
