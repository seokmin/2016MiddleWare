using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class EnemyScript : MonoBehaviour
{
	public ENEMYSTATE state = ENEMYSTATE.IDLE;
	GameObject enemyManager = null;
	Transform target = null;
	PlayerState playerState = null;

	Dictionary<ENEMYSTATE, System.Action> dicState = new Dictionary<ENEMYSTATE, System.Action>();

	float stateTime = 0.0f;
	public float idleStateMaxTime = 2.0f;
	public Animation anim;
	CharacterController characterController = null;

	public float moveSpeed = 5.0f;
	public float rotationSpeed = 10.0f;
	public float attackableRange = 2.0f;
	public GameObject explosionParticle = null;
	public GameObject deadObject = null;

	int hp = 5;

	void OnEnable()
	{
		InitSpider();
	}

	void Awake()
	{
		anim = GetComponent<Animation>();
		characterController = GetComponent<CharacterController>();

		target = GameObject.Find("Player").transform;
		enemyManager = GameObject.Find("EnemyManager");
		playerState = target.GetComponent<PlayerState>();

		dicState[ENEMYSTATE.IDLE] = Idle;
		dicState[ENEMYSTATE.MOVE] = Move;
		dicState[ENEMYSTATE.ATTACK] = Attack;
		dicState[ENEMYSTATE.DAMAGE] = Damage;
		dicState[ENEMYSTATE.DEAD] = Dead;
		dicState[ENEMYSTATE.NONE] = None;

		InitSpider();
	}

	void InitSpider()
	{
		hp = 5;
		state = ENEMYSTATE.IDLE;
		anim.Play("idle");
	}

	void Update()
	{
		dicState[state]();
	}

	void None()
	{

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
			playerState.DamageByEnemy();
		}

	}

	void Damage()
	{
		hp -= 1;
		anim["damage"].speed = 0.5f;
		anim.Play("damage");

		anim.PlayQueued("idle", QueueMode.CompleteOthers);

		stateTime = 0.0f;
		state = ENEMYSTATE.IDLE;

		if (hp <= 0)
			state = ENEMYSTATE.DEAD;
	}

	void Dead()
	{
		var manager = enemyManager.GetComponent<EnemyManager>();
		manager.ReduceEnemyCount();


		StartCoroutine("DeadProcess");
		state = ENEMYSTATE.NONE;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (state == ENEMYSTATE.NONE ||
			state == ENEMYSTATE.DEAD)
			return;
		if (collision.gameObject.name.Contains("Ball") == false)
			return;
		state = ENEMYSTATE.DAMAGE;
	}

	IEnumerator DeadProcess()
	{
		anim["death"].speed = 0.5f;
		anim.Play("death");

		while(anim.isPlaying)
		{
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(1.0f);

		GameObject explosionObj = Instantiate(explosionParticle) as GameObject;
		Vector3 explosionObjPos = transform.position;
		explosionObjPos.y = 0.6f;
		explosionObj.transform.position = explosionObjPos;

		yield return new WaitForSeconds(0.5f);

		// 		GameObject deadObj = Instantiate(deadObject) as GameObject;
		// 		Vector3 deadObjPos = transform.position;
		// 		deadObjPos.y = 0.6f;
		// 		deadObj.transform.position = deadObjPos;
		// 
		// 		float rotationY = Random.Range(-180.0f, 180.0f);
		// 		deadObj.transform.eulerAngles = new Vector3(0.0f, rotationY, 0.0f);

		gameObject.SetActive(false);
	}
}