using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {
    public Transform cameraTransform;
    public GameObject fireObject;
    public Transform firePos;
    [Range(5.0f,50.0f)]
    public float forwardPower = 20.0f;
    public float upPower = 5.0f;
	PlayerState playerState = null;

	void Start()
	{
		playerState = GetComponent<PlayerState>();
	}
	// Update is called once per frame
	void Update ()
    {
		if (playerState.isDead)
			return;
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject obj = Instantiate(fireObject) as GameObject;
            obj.transform.position = firePos.position;
            obj.GetComponent<Rigidbody>().velocity = cameraTransform.forward * forwardPower + Vector3.up * upPower;
            obj.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-180,180),Random.Range(-180,180),Random.Range(-180,180));
        }
	}
}
