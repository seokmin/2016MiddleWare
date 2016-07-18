using UnityEngine;
using System.Collections;

public class iTweenRotateTest : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.S))
		{
			Hashtable hash = new Hashtable();
			hash.Add("rotation", transform.eulerAngles + new Vector3(0.0f, 1080.0f, 0.0f));
			hash.Add("time", 3.0f);
			hash.Add("easetype", iTween.EaseType.easeOutExpo);
			hash.Add("looptype", iTween.LoopType.none);

			iTween.RotateTo(gameObject, hash);
		}
	}
}
