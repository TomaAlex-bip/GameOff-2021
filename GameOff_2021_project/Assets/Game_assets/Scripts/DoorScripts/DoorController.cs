using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float doorSpeed = 2f;

    [SerializeField] private int id;
    
    private Vector3 initialPositionL;
    private Vector3 initialPositionR;
    private Vector3 desiredPositionL;
    private Vector3 desiredPositionR;

    private Transform leftDoor;
    private Transform rightDoor;

    
    private void Start()
    {
        leftDoor = transform.Find("left_door");
        rightDoor = transform.Find("right_door");
        
        initialPositionL = leftDoor.localPosition;
        initialPositionR = rightDoor.localPosition;
        desiredPositionL = leftDoor.localPosition + new Vector3(0.99f, 0f, 0f);
        desiredPositionR = rightDoor.localPosition + new Vector3(-0.99f, 0f, 0f);
        
        GameEvents.Instance.onDoorTriggerEnter += OpenDoor;
        GameEvents.Instance.onDoorTriggerExit += CloseDoor;
    }

    private void OpenDoor(int triggerId)
    {
        if (id == triggerId)
        {
            print("open door");
            SoundManager.Instance.PlaySound("e_door_open");
            StopAllCoroutines();
            StartCoroutine(OpenLeftDoorCoroutine());
            StartCoroutine(OpenRightDoorCoroutine());
        }
    }

    private void CloseDoor(int triggerId)
    {
        if (id == triggerId)
        {
            print("close door");
            SoundManager.Instance.PlaySound("e_door_close");
            StopAllCoroutines();
            StartCoroutine(CloseLeftDoorCoroutine());
            StartCoroutine(CloseRightDoorCoroutine());
        }
    }


    private IEnumerator OpenLeftDoorCoroutine()
    {
        while (Vector3.Distance(leftDoor.localPosition, desiredPositionL) > -0.0001f)
        {
            leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, desiredPositionL, doorSpeed * Time.deltaTime);
            yield return null;
        }
        
    }
    
    private IEnumerator OpenRightDoorCoroutine()
    {
        while (Vector3.Distance(rightDoor.localPosition, desiredPositionR) > -0.0001f)
        {
            rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition, desiredPositionR, doorSpeed * Time.deltaTime);
            yield return null;
        }
        
    }
    
    
    private IEnumerator CloseLeftDoorCoroutine()
    {
        while (Vector3.Distance(leftDoor.localPosition, initialPositionL) > 0.0001f)
        {
            leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, initialPositionL, doorSpeed * Time.deltaTime);
            yield return null;
        }

    }
    
    private IEnumerator CloseRightDoorCoroutine()
    {
        while (Vector3.Distance(rightDoor.localPosition, initialPositionR) > 0.0001f)
        {
            rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition, initialPositionR, doorSpeed * Time.deltaTime);
            yield return null;
        }

    }
    
    
    
    
}
