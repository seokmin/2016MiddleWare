using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour
{
	public int playerHP = 5;

	public bool isDead = false;

	CameraShake cameraShake = null;

	void Start()
	{
		cameraShake = GetComponentInChildren<CameraShake>();
	}
	void OnGUI()
	{
		float x = (Screen.width / 2.0f) - 100;

		Rect rect = new Rect(x, 10, 200, 25);
		if (isDead)
			GUI.Box(rect, "Game Over!");
		else
			GUI.Box(rect, "My Health : " + playerHP);
	}
	public void DamageByEnemy()
	{
		if (isDead)
			return;
		--playerHP;
		cameraShake.PlayCameraShake();
		if(playerHP <= 0)
			isDead = true;
	}
}