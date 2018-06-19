using System.Collections;
using UnityEngine;

public class GreenOrc : MonoBehaviour {
    public float Speed = 2;
    public float RunningSpeed = 3;
    public Vector3 MoveBy = new Vector3(0, -1, 0);

    private Rigidbody2D _myBody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private Mode _mode;
    private Vector3 _pointA;

    private Vector3 _pointB;

    // if rabbit is higher than orc by at least this amount, the rabbit kills orc
    private float _rabbitWinHeight;
    private float _speed;

    public enum Mode {
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
        _mode = Mode.GoToB;
        _pointA = transform.position;
        _pointB = _pointA + MoveBy;
        _rabbitWinHeight = GetComponent<BoxCollider2D>().size.y;
        _speed = Speed;
    }

    // Update is called once per frame
    void Update() {
        if (!_animator.GetBool("dead")) {
            switch (_mode) {
                case Mode.Attack:
                    _animator.SetBool("run", true);
                    break;
                default:
                    _animator.SetBool("run", false);
                    break;
            }
        }
    }

    // used for physics calculations
    private void FixedUpdate() {
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

        // update direction
        if (value < 0) {
            _spriteRenderer.flipX = false;
        }
        else if (value > 0) {
            _spriteRenderer.flipX = true;
        }
    }

    private void UpdateMode() {
        if (IsTargetInZone()) {
            _mode = Mode.Attack;
            _speed = RunningSpeed;
        }
        else if (_mode == Mode.Attack) {
            // target is out of the zone, but orc is still in attack mode
            // change mode
            _mode = Mode.GoToA;
            _speed = Speed;
        }

        if (_mode == Mode.GoToA) {
            if (IsArrived(_pointA)) {
                _mode = Mode.GoToB;
            }
        }
        else if (_mode == Mode.GoToB) {
            if (IsArrived(_pointB)) {
                _mode = Mode.GoToA;
            }
        }
    }

    private float GetDirection() {
        float myPosX = transform.position.x;
        switch (_mode) {
            case Mode.GoToA:
                return GetDirection(myPosX, _pointA.x);
            case Mode.GoToB:
                return GetDirection(myPosX, _pointB.x);
            case Mode.Attack:
                return GetAttackDirection(myPosX);
            default:
                return 0;
        }
    }

    private float GetDirection(float myPosX, float targetPosX) {
        return myPosX < targetPosX ? 1 : -1;
    }

    private float GetAttackDirection(float myPosX) {
        if (_boxCollider.IsTouching(HeroRabbit.LastRabbit.GetComponent<BoxCollider2D>())) {
            return 0;
        }

        return GetDirection(myPosX, HeroRabbit.LastRabbit.transform.position.x);
    }

    private bool IsArrived(Vector3 target) {
        Vector3 pos = transform.position;
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }

    private bool IsTargetInZone() {
        Vector3 rabbitPos = HeroRabbit.LastRabbit.transform.position;
        if (rabbitPos.x > Mathf.Min(_pointA.x, _pointB.x) && rabbitPos.x < Mathf.Max(_pointA.x, _pointB.x)) {
            return true;
        }

        return false;
    }

    private void OnRabbitHit(HeroRabbit rabbit) {
        float myPosY = transform.position.y;
        float rabbitPosY = HeroRabbit.LastRabbit.transform.position.y;
        if (rabbitPosY - myPosY >= _rabbitWinHeight) {
            // rabbit is higher, orc dies and rabbit jumps
            Die();
            rabbit.Jump();
        }
        else {
            // orc attacks
            Attack();
            // rabbit dies
            rabbit.Die();
        }
    }

    public void Attack() {
        _animator.SetTrigger("attack");
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

    void OnCollisionEnter2D(Collision2D collision) {
        HeroRabbit rabbit = collision.collider.GetComponent<HeroRabbit>();
        if (rabbit != null) {
            OnRabbitHit(rabbit);
        }
    }
}