using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownOrc : Orc {

	public float AttackRadius = 5;

	protected override bool IsTargetInZone() {
		float rabbitPosX = HeroRabbit.LastRabbit.transform.position.x;
		float myPosX = transform.position.x;
		// rabit is within Attack radius from orc
		return Mathf.Abs(rabbitPosX - myPosX) <= AttackRadius;
	}

	protected override float GetAttackDirection() {
		if (_mode == Mode.Attack) {
			return 0;
		}
		// myPosX < rabbitPosX
		return transform.position.x < HeroRabbit.LastRabbit.transform.position.x ? 1 : -1;
	}

	protected override void Attack() {
		_mode = Mode.Attack;
		// turn to rabbit
		// rabbitPosX < myPosX
		if (HeroRabbit.LastRabbit.transform.position.x < transform.position.x) {
			_spriteRenderer.flipX = false;
		}
		else {
			_spriteRenderer.flipX = true;
		}
		_animator.SetBool("idle", true);
	}

	protected override void ResumePatrol() {
		_mode = _spriteRenderer.flipX ? Mode.GoToB : Mode.GoToA;
		_animator.SetBool("idle", false);
	}

	protected override void OnRabbitHit(HeroRabbit rabbit) {
		float myPosY = transform.position.y;
		float rabbitPosY = HeroRabbit.LastRabbit.transform.position.y;
		if (rabbitPosY - myPosY >= _rabbitWinHeight) {
			// rabbit is higher, orc dies and rabbit jumps
			Die();
			rabbit.Jump();
		}
	}
}
