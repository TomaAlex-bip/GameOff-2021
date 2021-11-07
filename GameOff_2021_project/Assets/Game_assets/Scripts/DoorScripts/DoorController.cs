using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float doorSpeed = 2f;

    [SerializeField] private int id;
    
    private Vector3 initialPosition;
    private Vector3 desiredPosition;

    
    private void Start()
    {
        initialPosition = transform.localPosition;
        desiredPosition = transform.localPosition + new Vector3(0f, transform.localScale.y - 0.5f, 0f);
        
        GameEvents.Instance.onDoorTriggerEnter += OpenDoor;
        GameEvents.Instance.onDoorTriggerExit += CloseDoor;
    }

    private void OpenDoor(int triggerId)
    {
        if (id == triggerId)
        {
            StopAllCoroutines();
            StartCoroutine(OpenDoorCoroutine());
        }
    }

    private void CloseDoor(int triggerId)
    {
        if (id == triggerId)
        {
            StopAllCoroutines();
            StartCoroutine(CloseDoorCoroutine());
        }
    }


    private IEnumerator OpenDoorCoroutine()
    {
        while (Vector3.Distance(transform.localPosition, desiredPosition) > 0.0001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPosition, doorSpeed * Time.deltaTime);
            //print("corutina cu OPEN");
            yield return null;
        }
        
    }
    
    
    private IEnumerator CloseDoorCoroutine()
    {
        while (Vector3.Distance(transform.localPosition, initialPosition) > 0.0001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, doorSpeed * Time.deltaTime);
            //print("corutina cu CLOSE");
            yield return null;
        }

    }
    
    
    
    
}
