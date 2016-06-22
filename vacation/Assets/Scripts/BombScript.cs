using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour
{
	public GameObject particleGround;
	public GameObject particleAir;
	private int currentCol = 0;
	private float sumDt = 0.0f;
	private bool isOnFloor = false;

	void Boom()
	{
		GameObject obj;
		if(isOnFloor)
			obj = Instantiate(particleGround) as GameObject;
		else
			obj = Instantiate(particleAir) as GameObject;
		obj.transform.position = transform.position;
		Destroy(gameObject);
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name.Contains("Floor"))
			isOnFloor = true;
		else
			isOnFloor = false;
		if (currentCol < 2 && !other.gameObject.name.Contains("Enemy"))
		{
			currentCol++;
			return;
		}
		Boom();
	
	}
	void OnCollisionExit(Collision other)
	{
		isOnFloor = false;
	}

	/*
	void Update()
	{
		sumDt += Time.deltaTime;
		if (sumDt > 3.0f)
			Boom();
	}*/
}