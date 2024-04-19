using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLooking : MonoBehaviour
{

    public float DPI = 800f;

    public Transform playerBody;

    float xRotation = 0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * DPI * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * DPI * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
