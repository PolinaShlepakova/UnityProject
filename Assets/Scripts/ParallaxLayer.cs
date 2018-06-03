using UnityEngine;

public class ParallaxLayer : MonoBehaviour {
	
	//[0, 1] : 	if 0, background doesn't move (like platforms)
	//			if 1, background moves like rabbit
	public float Slowdown = 0.5f;
	Vector3 _lastPosition;
	
	void Awake() {
		_lastPosition = Camera.main.transform.position;
	}
	
	void LateUpdate() {
		// update position
		Vector3 newPosition = Camera.main.transform.position;
		Vector3 diff = newPosition - _lastPosition;
		_lastPosition = newPosition;
		
		Vector3 myPos = transform.position;
		// move background in the same direction, but with slowdown
		myPos += Slowdown * diff;
		transform.position = myPos;
	}
}