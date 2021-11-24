using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRespawn : MonoBehaviour
{
    [SerializeField] private Vector3 minAllowedPos;
    [SerializeField] private Vector3 maxAllowedPos;

    private Vector3 initialPos;


    private void Start()
    {
        initialPos = transform.localPosition;
    }

    private void Update()
    {
        // -1 2 -5 => bun
        
        // print(minAllowedPos);
        // print(maxAllowedPos);
        
        //min -5     -1     -35.5
        //max 15.5    12    -0.5
        if (transform.localPosition.x < minAllowedPos.x
            || transform.localPosition.y < minAllowedPos.y
            || transform.localPosition.z < minAllowedPos.z
            || transform.localPosition.x > maxAllowedPos.x
            || transform.localPosition.y > maxAllowedPos.y
            || transform.localPosition.z > maxAllowedPos.z)
        {
            transform.localPosition = initialPos;
            // print("reset cube");
            // print("x: " + transform.position.x + "<" + minAllowedPos.x);
            // print("x: " + transform.position.x + ">" + maxAllowedPos.x);
            //
            // print("y: " + transform.position.y + "<" + minAllowedPos.y);
            // print("y: " + transform.position.y + ">" + maxAllowedPos.y);
            //
            // print("z: " + transform.position.z + "<" + minAllowedPos.z);
            // print("z: " + transform.position.z + ">" + maxAllowedPos.z);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CubeRespawn"))
        {
            transform.localPosition = initialPos;
        }
    }
}
