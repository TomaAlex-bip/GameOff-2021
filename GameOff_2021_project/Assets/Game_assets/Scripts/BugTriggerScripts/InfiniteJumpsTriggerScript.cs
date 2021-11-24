using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteJumpsTriggerScript : MonoBehaviour
{

    private PlayerMovement playerMovement;

    private bool infiniteJumps;

    private void Start()
    {
        playerMovement = PlayerMovement.Instance;
    }


    private void Update()
    {
        playerMovement.InfiniteJumps = infiniteJumps;
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            infiniteJumps = true;
        }
    }
    
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            infiniteJumps = false;
        }
    }
    
    
    
    
    
}
