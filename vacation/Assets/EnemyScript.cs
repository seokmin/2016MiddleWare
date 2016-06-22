using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class EnemyScript : MonoBehaviour
{
	public ENEMYSTATE state = ENEMYSTATE.IDLE;

	//delegate void Func();
	//Dictionary<ENEMYSTATE, Func> dicState = new Dictionary<ENEMYSTATE, Func>();
	Dictionary<ENEMYSTATE, System.Action> dicState = new Dictionary<ENEMYSTATE, System.Action>();

	float stateTime = 0.0f;
	public float idleStateMaxTime = 2.0f;
	public Animation anim;
	public Transform target = null;
	CharacterController characterController = null;

	public float moveSpeed = 5.0f;
	public float rotationSpeed = 10.0f;
	public float attackableRange = 2.0f;

	void Awake()
	{
		anim = GetComponent<Animation>();
		characterController = GetComponent<CharacterController>();

		dicState[ENEMYSTATE.IDLE] = Idle;
		dicState[ENEMYSTATE.MOVE] = Move;
		dicState[ENEMYSTATE.ATTACK] = Attack;
		dicState[ENEMYSTATE.DAMAGE] = Damage;
		dicState[ENEMYSTATE.DEAD] = Dead;

		InitSpider();
	}

	void InitSpider()
	{
		state = ENEMYSTATE.IDLE;
		anim.Play("idle");
	}

	void Update()
	{
		dicState[state]();
	}

	void Idle()
	{
		stateTime += Time.deltaTime;
		if(stateTime>= idleStateMaxTime)
		{
			stateTime = 0.0f;
			state = ENEMYSTATE.MOVE;
		}
	}

	void Move()
	{
		anim.Play("walk");

		Vector3 dir = target.position - transform.position;

		if (dir.magnitude > attackableRange)
		{
			Debug.Log(dir.magnitude);
			dir.Normalize();
			characterController.SimpleMove(dir * moveSpeed);

			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotationSpeed * Time.deltaTime);
		}
		else
		{
			stateTime = 2.0f;
			state = ENEMYSTATE.ATTACK;
		}
	}

	void Attack()
	{
		stateTime += Time.deltaTime;

		Vector3 dir = target.position - transform.position;

		if (dir.magnitude > attackableRange && stateTime > 2.0f)
		{
			stateTime = 0.0f;
			state = ENEMYSTATE.MOVE;
		}

		if (stateTime>2.0f)
		{
			stateTime = 0.0f;
			anim.Play("attack_Melee");
			anim.PlayQueued("idle", QueueMode.CompleteOthers);
		}

	}

	void Damage()
	{
	}

	void Dead()
	{
	}
}