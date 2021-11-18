using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCameraTrigger : MonoBehaviour
{

    private Transform cam;

    private PlayerManager player;

    private void Start()
    {
        player = PlayerManager.Instance;
        cam = player.transform.Find("CameraHolder").GetChild(0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.localRotation = Quaternion.Euler(0f, 0f, 180f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.localRotation = Quaternion.identity;
        }
    }
}
