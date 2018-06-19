using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrc : MonoBehaviour {
	
	public float Speed = 1;
	public Vector3 MoveBy = new Vector3(0, -1, 0);

	private Rigidbody2D _myBody;
	private Animator _animator;
	private SpriteRenderer _spriteRenderer;
	private Mode _mode;
	private Vector3 _pointA;
	private Vector3 _pointB;
	
	public enum Mode {
		GoToA,
		GoToB,
		Attack
	}

	// Use this for initialization
	void Start () {
		_myBody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_mode = Mode.GoToA;
		if (MoveBy.x > 0) {
			_pointA = transform.position;
			_pointB = _pointA + MoveBy;
		}
		else {
			_pointB = transform.position;
			_pointA = _pointB + MoveBy;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// used for physics calculations
	private void FixedUpdate() {
		// if (!_animator.GetBool("dead")) {
			UpdateDirection();
			UpdateVelocity();
		// }
	}
	
	private void UpdateVelocity() {
		//[-1, 1]
		float value = GetDirection();

		// update velocity
		if (Mathf.Abs(value) > 0) {
			Vector2 vel = _myBody.velocity;
			vel.x = value * Speed;
			_myBody.velocity = vel;
		}
	}

	private void UpdateDirection() {
		Vector3 myPos = transform.position;
		Vector3 target = _mode == Mode.GoToA ? _pointA : _pointB;

		if (isArrived(myPos, target)) {
			ChangeDirection();
		}
	}
	
	private float GetDirection() {
		switch (_mode) {
			case Mode.GoToA:
				return -1;
			case Mode.GoToB:
				return 1;
			default:
				return 0;
		}
	}

	private void ChangeDirection() {
		switch (_mode) {
			case Mode.GoToA:
				_mode = Mode.GoToB;
				break;
			case Mode.GoToB:
				_mode = Mode.GoToA;
				break;
		}
		float value = GetDirection();
		
		if (value < 0) {
			_spriteRenderer.flipX = false;
		}
		else if (value > 0) {
			_spriteRenderer.flipX = true;
		}
	}
	
	private bool isArrived(Vector3 pos, Vector3 target) {
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance(pos, target) < 0.02f;
	}
	
	public void Die() {
		_animator.SetBool("dead", true);
		StartCoroutine(AnimateDeath());
	}
	
	private IEnumerator AnimateDeath() {
		// _animator.SetBool("dead", true);
		yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
		Destroy(gameObject);
	}
	
}
