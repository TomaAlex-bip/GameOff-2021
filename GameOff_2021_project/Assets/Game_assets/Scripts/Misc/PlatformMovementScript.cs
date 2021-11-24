using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementScript : MonoBehaviour
{

    [SerializeField] private float upMovementDistance;

    [SerializeField] private float speed = 0.5f;

    [SerializeField] private bool goingUp;


    private float maxY;
    private float minY;
    

    private void Start()
    {
        goingUp = true;

        minY = transform.position.y;
        maxY = minY + upMovementDistance;
    }

    private void Update()
    {
        var y = transform.position.y;
        if (goingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x, maxY, transform.position.z), 
                speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x, minY, transform.position.z), 
                speed * Time.deltaTime);
        }

        if (Mathf.Abs(y - maxY) < 0.01f)
        {
            goingUp = false;
        }
        
        if (Mathf.Abs(y - minY) < 0.01f)
        {
            goingUp = true;
        }
        
        
        
    }
}
