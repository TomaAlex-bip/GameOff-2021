using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoomTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // next scene
        }
    }
}
