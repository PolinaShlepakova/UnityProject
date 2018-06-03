using UnityEngine;

public class HeroRabbit : MonoBehaviour
{
	public float Speed = 1;
	private Rigidbody2D _myBody;
	
	private bool _isGrounded = false;
	private bool _jumpActive = false;
	private float _jumpTime = 0f;
	
	public float MaxJumpTime = 2f;
	public float JumpSpeed = 2f;

	// Use this for initialization
	void Start () {
		// save rabbit's starting position
		LevelController.Current.SetStartPosition(transform.position);
		_myBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame (used for animations)
	void Update () {
		Animator animator = GetComponent<Animator>();
		AnimateRun(animator);
		AnimateJump(animator);
	}

	// used for physics calculations
	void FixedUpdate() {
		UpdateHorizontalPosition();
		UpdateJumpStatus();
	}

	private void AnimateRun(Animator animator) {
		float value = Input.GetAxis("Horizontal");		
		if (Mathf.Abs(value) > 0) {
			animator.SetBool("run", true);
		} else {
			animator.SetBool("run", false);
		}
	}

	private void AnimateJump(Animator animator) {	
		if (_isGrounded) {
			animator.SetBool("jump", false);
		} else {
			animator.SetBool("jump", true);
		}
	}

	private void UpdateHorizontalPosition() {
		//[-1, 1]
		float value = Input.GetAxis("Horizontal");
		
		// update velocity
		if (Mathf.Abs(value) > 0) {
			Vector2 vel = _myBody.velocity;
			vel.x = value * Speed;
			_myBody.velocity = vel;
		}
		
		// update direction
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if (value < 0) {
			sr.flipX = true;
		} else if(value > 0) {
			sr.flipX = false;
		}
	}

	private void UpdateJumpStatus() {
		Vector3 from = transform.position + Vector3.up * 0.3f;
		Vector3 to = transform.position + Vector3.down * 0.1f;
		int layerId = 1 << LayerMask.NameToLayer("Ground");
		
		// check if line goes through Collider with layer "Ground"
		RaycastHit2D hit = Physics2D.Linecast(from, to, layerId);
		if (hit) {
			_isGrounded = true;
		} else {
			_isGrounded = false;
		}
		
		// if button was just pressed
		if(Input.GetButtonDown("Jump") && _isGrounded) {
			_jumpActive = true;
		}
		
		if (_jumpActive) {
			// if button is still held
			if (Input.GetButton("Jump")) {
				_jumpTime += Time.deltaTime;
				if (_jumpTime < MaxJumpTime) {
					Vector2 vel = _myBody.velocity;
					vel.y = JumpSpeed * (1.0f - _jumpTime / MaxJumpTime);
					_myBody.velocity = vel;
				}
			} else {
				_jumpActive = false;
				_jumpTime = 0;
			}
		}
	}
}
