using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTimerTrigger : MonoBehaviour
{
    private Transform timer;

    private void Start()
    {
        var p = transform.parent;
        timer = p.transform.Find("Timer");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var t = timer.GetComponent<DoorTimer>();
            t.ActivateTimer();
        }
    }
}
