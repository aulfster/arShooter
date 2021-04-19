using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonARCameraMovement : MonoBehaviour
{
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Update is called once per frame
    void Update()
    {
        yaw += horizontalSpeed * Input.GetAxis("Mouse X");
        pitch -= verticalSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        
        float xAxisValue = Input.GetAxis("Horizontal") * Time.deltaTime;
        float zAxisValue = Input.GetAxis("Vertical") * Time.deltaTime;

        transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue));
    }
}
