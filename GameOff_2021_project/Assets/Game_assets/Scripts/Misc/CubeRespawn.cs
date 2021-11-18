using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRespawn : MonoBehaviour
{

    private Vector3 initialPos;


    private void Start()
    {
        initialPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CubeRespawn"))
        {
            transform.position = initialPos;
        }
    }
}
