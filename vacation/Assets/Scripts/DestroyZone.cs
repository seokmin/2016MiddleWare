using UnityEngine;
using System.Collections;

public class DestroyZone : MonoBehaviour {
	public Transform spawningZone;
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name.Contains("Ball"))
		{
			Destroy(other.gameObject);
		}
		else if( other.gameObject.name.Contains("Player"))
		{
			other.transform.position = spawningZone.position;
		}
	}
}
