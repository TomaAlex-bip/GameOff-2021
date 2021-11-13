using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Range(0f, 5f)]
    [SerializeField] private float mouseSensitivity;

    [SerializeField] private float minLookDown = 80f;
    [SerializeField] private float maxLookUp = 90f;
    
    private Transform cam;

    private float xRot = 0f;


    private float mouseX;
    private float mouseY;


    private void Start()
    {
        cam = transform.Find("CameraHolder");

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
        xRot = Mathf.Clamp(xRot, -maxLookUp, minLookDown);

        cam.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }


}
