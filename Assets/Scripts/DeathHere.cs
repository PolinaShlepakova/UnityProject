using UnityEngine;

public class DeathHere : MonoBehaviour {
    
    // function will be called when other object will collide into this one
    void OnTriggerEnter2D(Collider2D other) {   
        // try to get rabbit component
        HeroRabbit rabbit = other.GetComponent<HeroRabbit>();
        
        // other can be not a rabbit
        if (rabbit != null) {
            rabbit.Die();
        }
    }
}
