using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingDoor : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D collider) {
		HeroRabbit rabbit = collider.GetComponent<HeroRabbit>(); 
		if (rabbit != null) {
			LevelController.Current.PassLevel();
			SceneManager.LoadScene("ChooseLevel");
		}
	}
}
