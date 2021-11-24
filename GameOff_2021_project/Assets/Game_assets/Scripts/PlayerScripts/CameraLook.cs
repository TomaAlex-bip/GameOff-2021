using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] private float mouseSensitivity;

    [SerializeField] private float minLookDown = 80f;
    [SerializeField] private float maxLookUp = 90f;

    [SerializeField] private Vector3 crouchCameraPoistion = new Vector3(0f, 0.11f, 0.46f);
    
    private Transform cameraHolder;

    private float xRot = 0f;


    private float mouseX;
    private float mouseY;

    private Vector3 initialCamPos;

    private void Start()
    {
        cameraHolder = transform.Find("CameraHolder");

        Cursor.lockState = CursorLockMode.Locked;

        initialCamPos = cameraHolder.localPosition;
    }

    private void Update()
    {
        mouseSensitivity = GameManager.Instance.MouseSensitivity;

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

        cameraHolder.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    public void NormalCameraPosition() => cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, initialCamPos, 5f*Time.deltaTime);

    public void CrouchCameraPoistion() => cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, crouchCameraPoistion, 5f*Time.deltaTime);

}
