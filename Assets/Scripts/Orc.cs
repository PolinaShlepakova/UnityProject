using System.Collections;
using UnityEngine;

// ReSharper disable InconsistentNaming

public abstract class Orc : MonoBehaviour {
    public float NormalSpeed = 2;
    public Vector3 MoveBy = new Vector3(-3, 0, 0);

    protected Rigidbody2D _myBody;
    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    protected BoxCollider2D _boxCollider;
    protected Mode _mode;
    protected Vector3 _pointA;
    protected Vector3 _pointB;

    // if rabbit is higher than orc by at least this amount, the rabbit kills orc
    protected float _rabbitWinHeight;
    protected float _speed;

    protected static readonly float _timeBetweenFlips = 0.3f;
    protected float _lastFlip;


    protected enum Mode {
        GoToA,
        GoToB,
        Attack
    }

    // Use this for initialization
    void Start() {
        _myBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _mode = Mode.GoToA;
        if (MoveBy.x >= 0) {
            _pointA = transform.position;
            _pointB = _pointA + MoveBy;
        }
        else {
            _pointB = transform.position;
            _pointA = _pointB + MoveBy;
        }

        _rabbitWinHeight = GetComponent<BoxCollider2D>().size.y;
        _rabbitWinHeight *= 0.8f;
        _speed = NormalSpeed;
        _lastFlip = Time.time;
    }

    // used for physics calculations
    protected virtual void FixedUpdate() {
        if (!_animator.GetBool("dead")) {
            UpdateMode();
            UpdateHorizontalPosition();
        }
    }

    private void UpdateHorizontalPosition() {
        //[-1, 1]
        float value = GetDirection();

        // update velocity
        if (Mathf.Abs(value) > 0) {
            Vector2 vel = _myBody.velocity;
            vel.x = value * _speed;
            _myBody.velocity = vel;
        }

        UpdateDirection(value);
    }

    private void UpdateDirection(float value) {
        bool flip = _spriteRenderer.flipX;
        bool newFlip = value >= 0;
        if (flip != newFlip && Time.time - _lastFlip > _timeBetweenFlips) {
            _spriteRenderer.flipX = newFlip;
            _lastFlip = Time.time;
        }
    }

    private void UpdateMode() {
        if (IsTargetInZone()) {
            Attack();
        }
        else if (_mode == Mode.Attack) {
            // target is out of the zone, but orc is still in attack mode
            ResumePatrol();
        }

        if (IsArrived()) {
            if (_mode == Mode.GoToA) {
                _mode = Mode.GoToB;
            }
            else if (_mode == Mode.GoToB) {
                _mode = Mode.GoToA;
            }
        }
    }

    protected float GetDirection() {
        switch (_mode) {
            case Mode.GoToA:
                return -1;
            case Mode.GoToB:
                return 1;
            case Mode.Attack:
                return GetAttackDirection();
            default:
                return 0;
        }
    }

    private bool IsArrived() {
        return _mode == Mode.GoToA && transform.position.x <= _pointA.x ||
               _mode == Mode.GoToB && transform.position.x >= _pointB.x;
    }

    protected void Die() {
        _animator.SetBool("dead", true);
        StartCoroutine(AnimateDeath());
    }

    private IEnumerator AnimateDeath() {
        // _animator.SetBool("dead", true);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        HeroRabbit rabbit = collision.collider.GetComponent<HeroRabbit>();
        if (rabbit != null &&
            !_animator.GetBool("dead") &&
            !HeroRabbit.LastRabbit.GetComponent<Animator>().GetBool("dead")) {
                OnRabbitHit(rabbit);
        }
    }

    protected abstract bool IsTargetInZone();

    protected abstract float GetAttackDirection();

    protected abstract void Attack();
    
    protected abstract void ResumePatrol();
    
    protected abstract void OnRabbitHit(HeroRabbit rabbit);
}