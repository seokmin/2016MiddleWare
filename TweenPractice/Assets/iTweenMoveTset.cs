using UnityEngine;
using System.Collections;
using DG.Tweening;


public class iTweenMoveTset : MonoBehaviour {
	public Transform moveTarget;
	// Update is called once per frame
	void Update () {
	if(Input.GetKeyDown(KeyCode.S))
		{

			//Hashtable hash = new Hashtable();
			//hash.Add("position", moveTarget);
			//hash.Add("time", 5.0f);
			//hash.Add("easetype", iTween.EaseType.easeInOutExpo);
			//hash.Add("orienttopath", true);
			//hash.Add("looktime", 2.0f);
			//iTween.MoveTo(gameObject, hash);

			transform.DOLookAt(moveTarget.position, 2.0f).SetEase(Ease.OutCirc);
			transform.DOMove(moveTarget.position, 5.0f).SetEase(Ease.InOutExpo);

			//LeanTween.move(gameObject, moveTarget, 5.0f).setEase(LeanTweenType.easeInOutExpo);
			//
			//Quaternion origin = transform.rotation;
			//transform.LookAt(moveTarget);
			//Vector3 euler = transform.eulerAngles;
			//transform.rotation = origin;
			//
			//LeanTween.rotate(gameObject, euler, 2.0f).setEase(LeanTweenType.easeOutCirc);

		}
	}
}
