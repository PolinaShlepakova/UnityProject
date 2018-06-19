using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownOrc : Orc {

	public float AttackRadius = 5;
	// carrot game object to create copies from
	public GameObject PrefabCarrot;
	// time between consecutive carrot launches
	public float TimeBetweenCarrotLaunches = 2;
	
	private float _lastCarrotLaunchTime;

	protected override void FixedUpdate() {
		base.FixedUpdate();
		UpdateCarrotLaunch();
	}

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

	private float GetCarrotDirection() {
		// myPosX < rabbitPosX
		return transform.position.x < HeroRabbit.LastRabbit.transform.position.x ? 1 : -1;
	}

	private void UpdateCarrotLaunch() {
		// check launch time
		if(Time.time - _lastCarrotLaunchTime > TimeBetweenCarrotLaunches) {
			_lastCarrotLaunchTime = Time.time;
			Throw();
		}
	}
	
	private void Throw() {
		_animator.SetBool("attack", true);
		StartCoroutine(AnimateThrow());
	}

	private IEnumerator AnimateThrow() {
		yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length / 3);
		LaunchCarrot(GetCarrotDirection());
	}
	
	private void LaunchCarrot(float direction) {
		// create copy of carrot prefab
		Carrot carrot = Instantiate(PrefabCarrot).GetComponent<Carrot>();
		// set initial position (orc's position)
		Vector3 carrotPos = transform.position;
		carrotPos.y += _rabbitWinHeight / 2;
		carrot.transform.position = carrotPos;
		// launch
		carrot.Launch(direction);
	}
}
