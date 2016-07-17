using UnityEngine;
using System.Collections;



public class CharactorController : MonoBehaviour
{
	public Rigidbody2D rigidBody;
	public float jumpPower = 100;

	Animator anim;

	private bool isInAir = false;
	public AudioSource jumpSound = null;

	void Start()
	{
		anim = GetComponent<Animator>();
	}
	void Update()
	{
		if (Input.GetKey(KeyCode.UpArrow) && isInAir == false)
		{
			if (jumpSound.isPlaying == false)
			{
				jumpSound.Play(0);
				rigidBody.AddForce(new Vector2(0, jumpPower));
			}
		}
		if (Input.GetKeyDown(KeyCode.Z))
			anim.SetTrigger("Attack");
		if (Input.GetKeyDown(KeyCode.DownArrow))
			anim.SetBool("isDefense", true);
		if (Input.GetKeyUp(KeyCode.DownArrow))
			anim.SetBool("isDefense", false);

	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Floor")
		{
			Debug.Log("바닥에 닿았다.");
			isInAir = false;
			anim.SetBool("isOnFloor", true);
			gameObject.layer = 8;
			
		}
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Floor")
		{
			Debug.Log("바닥에서 떨어졌다.");
			isInAir = true;
			anim.SetBool("isOnFloor", false);
			gameObject.layer = 9;
		}
	}
}