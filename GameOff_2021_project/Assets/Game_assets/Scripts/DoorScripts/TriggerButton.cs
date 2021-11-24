using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButton : MonoBehaviour
{
    [SerializeField] private int id;
    
    private void OnTriggerEnter(Collider other)
    {
        //GameEvents.Instance.DoorTriggerEnter(id);
    }

    private void OnTriggerStay(Collider other)
    {
        GameEvents.Instance.DoorTriggerEnter(id);
    }

    private void Update()
    {
        //GameEvents.Instance.DoorTriggerExit(id);
    }

    private void OnTriggerExit(Collider other)
    {
        if (GameManager.Instance.GameIsOn)
        {
            GameEvents.Instance.DoorTriggerExit(id);
        }
    }
}
