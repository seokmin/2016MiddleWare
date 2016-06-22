using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	Vector3 localPosition = Vector3.zero;

	void Start()
	{
		localPosition = transform.localPosition;
	}

	public void PlayCameraShake()
	{
		StopAllCoroutines();
		StartCoroutine(CameraShakeProcess(1.0f, 0.2f));
	}

	IEnumerator CameraShakeProcess(float shakeTime, float shakeSense)
	{
		yield return null;
		float deltaTime = 0.0f;

		while(deltaTime < shakeTime)
		{
			deltaTime += Time.deltaTime;

			transform.localPosition = localPosition;
			Vector3 pos = Vector3.zero;
			pos.x = Random.Range(-shakeSense, shakeSense);
			pos.y = Random.Range(-shakeSense, shakeSense);
			pos.z = Random.Range(-shakeSense, shakeSense);
			transform.localPosition += pos;

			yield return new WaitForEndOfFrame();
		}
		transform.localPosition = localPosition;
	}
}
