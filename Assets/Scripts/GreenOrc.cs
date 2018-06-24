using UnityEngine;

public class GreenOrc : Orc {
    public float RunningSpeed = 3;

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

    protected override void Attack() {
        _mode = Mode.Attack;
        _speed = RunningSpeed;
    }

    protected override void ResumePatrol() {
        _mode = _spriteRenderer.flipX ? Mode.GoToB : Mode.GoToA;
        _speed = NormalSpeed;
    }

    protected override float GetAttackDirection() {
        if (_boxCollider.IsTouching(HeroRabbit.LastRabbit.GetComponent<BoxCollider2D>())) {
            return 0;
        }
        // myPos < rabbitPos
        return transform.position.x < HeroRabbit.LastRabbit.transform.position.x ? 1 : -1;
    }

    protected override bool IsTargetInZone() {
        Vector3 rabbitPos = HeroRabbit.LastRabbit.transform.position;
        Vector3 myPos = transform.position;
        if (rabbitPos.x > _pointA.x && rabbitPos.x < _pointB.x && 
            Mathf.Abs(rabbitPos.y - myPos.y) < _rabbitWinHeight) {
            return true;
        }
        return false;
    }

    protected override void OnRabbitHit(HeroRabbit rabbit) {
        float myPosY = transform.position.y;
        float rabbitPosY = HeroRabbit.LastRabbit.transform.position.y;
        if (rabbitPosY - myPosY >= _rabbitWinHeight) {
            // rabbit is higher, orc dies and rabbit jumps
            Die();
            rabbit.Jump();
        }
        else {
            // orc attacks
            Kill();
            // rabbit dies
            rabbit.Die();
        }
    }

    public void Kill() {
        _animator.SetTrigger("kill");
    }
}