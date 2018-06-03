using UnityEngine;

public class HeroFollow : MonoBehaviour {

	public HeroRabbit Rabbit;

	void Update () {
		Transform rabbitTransform = Rabbit.transform;
		Transform cameraTransform = transform;

		Vector3 rabbitPosition = rabbitTransform.position;
		Vector3 cameraPosition = cameraTransform.position;

		// move Camera along X and Y
		cameraPosition.x = rabbitPosition.x;
		cameraPosition.y = rabbitPosition.y;

		// set camera's coordinates
		cameraTransform.position = cameraPosition;
	}
}