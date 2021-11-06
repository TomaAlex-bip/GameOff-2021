using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Range(0f, 5f)]
    [SerializeField] private float mouseSensitivity;

    private Transform cam;

    private float xRot = 0f;


    private float mouseX;
    private float mouseY;


    private void Start()
    {
        cam = transform.Find("Camera");

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        GetInput();
        
        SimpleRotation();

    }

    private void GetInput()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;
    }
    

    private void SimpleRotation()
    {
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }


}
