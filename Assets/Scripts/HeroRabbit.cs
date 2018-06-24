using System.Collections;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {
    public float Speed = 1;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;

    public static HeroRabbit LastRabbit;
    
    private static readonly float Growth = 0.5f;
    private static readonly Vector3 UsualScale = new Vector3(1, 1, 0);
    private static readonly Color UsualColor = Color.white;
    private static readonly Color BombInvulnerableColor = new Color(1f, 0.6f, 0.6f); 
    private static readonly float BombInvulnerableTime = 4f;

    private Rigidbody2D _myBody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Transform _heroParent;
    private bool _isGrounded;
    private bool _jumpActive;
    private float _jumpTime;
    private bool _isBig;
    private bool _isBombInvulnerable;

    public bool IsBig {
        get { return _isBig; }
    }

    public bool IsBombInvulnerable {
        get { return _isBombInvulnerable; }
    }

    // Use this for initialization
    private void Start() {
        // save rabbit's starting position
        LevelController.Current.SetStartPosition(transform.position);
        _myBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // save standard parent GameObject
        _heroParent = transform.parent;
        _isBig = false;
        LastRabbit = this;
    }


    // Update is called once per frame (used for animations)
    private void Update() {
        if (!_animator.GetBool("dead")) {
            AnimateRun();
            AnimateJump();
        }
    }

    // used for physics calculations
    private void FixedUpdate() {
        if (!_animator.GetBool("dead")) {
            UpdateHorizontalPosition();
            UpdateGroundedStatus();
        }
    }

    private void AnimateRun() {
        float value = Input.GetAxis("Horizontal");
        if (Mathf.Abs(value) > 0) {
            _animator.SetBool("run", true);
        }
        else {
            _animator.SetBool("run", false);
        }
    }

    private void AnimateJump() {
        if (_isGrounded) {
            _animator.SetBool("jump", false);
        }
        else {
            _animator.SetBool("jump", true);
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
        if (value < 0) {
            _spriteRenderer.flipX = true;
        }
        else if (value > 0) {
            _spriteRenderer.flipX = false;
        }
    }


    private void UpdateGroundedStatus() {
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layerId = 1 << LayerMask.NameToLayer("Ground");

        // check if line goes through Collider with layer "Ground"
        RaycastHit2D hit = Physics2D.Linecast(from, to, layerId);
        if (hit) {
            _isGrounded = true;
            // check if grounded to a platform
            if (hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null) {
                // stick to platform by setting it as parent
                SetNewParent(transform, hit.transform);
            }
        }
        else {
            _isGrounded = false;
            // unstick from platform
            SetNewParent(transform, _heroParent);
        }

        // if button was just pressed
        if (Input.GetButtonDown("Jump") && _isGrounded) {
            _jumpActive = true;
        }

        if (_jumpActive) {
            // if button is still held
            if (Input.GetButton("Jump")) {
                Jump();
            }
            else {
                _jumpActive = false;
                _jumpTime = 0;
            }
        }
    }

    public void Jump() {
        _jumpActive = true;
        _jumpTime += Time.fixedDeltaTime;
        if (_jumpTime < MaxJumpTime) {
            Vector2 vel = _myBody.velocity;
            vel.y = JumpSpeed * (1.0f - _jumpTime / MaxJumpTime);
            _myBody.velocity = vel;
        }
    }

    public void Grow() {
        if (!_isBig) {
            _myBody.transform.localScale += new Vector3(Growth, Growth, 0);
            _isBig = true;
        }
    }

    public void Diminish() {
        if (_isBig) {
            _myBody.transform.localScale = UsualScale;
            _isBig = false;
            MakeBombInvulnerable();
            Invoke("MakeBombVulnerable", BombInvulnerableTime);
        }
    }
    
    public void MakeBombInvulnerable() {
        if (!_isBombInvulnerable) {
            _isBombInvulnerable = true;
            _spriteRenderer.color = BombInvulnerableColor;
        }
    }

    public void MakeBombVulnerable() {
        if (_isBombInvulnerable) {
            _isBombInvulnerable = false;
            _spriteRenderer.color = UsualColor;
        }
    }

    public void Die() {
        _jumpActive = false;
        _jumpTime = 0;
        _animator.SetBool("run", false);
        _animator.SetBool("jump", false);
        _animator.SetBool("dead", true);
        StartCoroutine(AnimateDeath());
    }

    public void Revive() {
        _animator.SetBool("dead", false);
        if (_isBig) {
            _myBody.transform.localScale = UsualScale;
            _isBig = false;
        }
    }

    private IEnumerator AnimateDeath() {
        // _animator.SetBool("dead", true);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        LevelController.Current.OnRabbitDeath(this);
    }

    static void SetNewParent(Transform obj, Transform newParent) {
        if (obj.transform.parent != newParent) {
            // save position in global coordinates
            Vector3 pos = obj.transform.position;
            // set new parent
            obj.transform.parent = newParent;
            // after setting new parent, rabbit will have other coordinates (relative to other parent)
            // return rabbit to saved global coordinates
            obj.transform.position = pos;
        }
    }
}