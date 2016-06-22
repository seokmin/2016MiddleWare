using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public float sensitivity = 300.0f;
    float rotationX;
    float rotationY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseMoveValueX = Input.GetAxis("Mouse X");
        float mouseMoveValueY = Input.GetAxis("Mouse Y");

        rotationX += mouseMoveValueY * sensitivity * Time.deltaTime;
        rotationY += mouseMoveValueX * sensitivity * Time.deltaTime;

        rotationX %= 360;
        rotationY %= 360;

        if (rotationX < -80.0f)
            rotationX = -80.0f;
        if (rotationX > 80.0f)
            rotationX = 80.0f;
        
        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0.0f);
    }
}