using UnityEngine;
using System.Collections;

public class iTweenScaleTest : MonoBehaviour {

	public Vector3 targetScale = new Vector3( 2.0f, 2.0f, 2.0f );
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.S))
		{
			//Hashtable hash = new Hashtable();
			//hash.Add("speed", 3.0f);
			//hash.Add("easetype", iTween.EaseType.easeInOutExpo);
			//hash.Add("x", 2.0f);
			//hash.Add("y", 2.0f);
			//hash.Add("z", 2.0f);
			//hash.Add("looptype", iTween.LoopType.pingPong);
			//iTween.ScaleTo(gameObject, hash);

			float distance = Mathf.Abs(Vector3.Distance(transform.localScale, targetScale));
			float time = distance / 3.0f;

			LeanTween.scale(gameObject, targetScale, time).setEase(LeanTweenType.easeInOutBack).setDelay(0.5f).setLoopPingPong();
			

		}
	}
}
