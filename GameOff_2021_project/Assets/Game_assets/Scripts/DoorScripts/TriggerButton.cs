using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButton : MonoBehaviour
{
    [SerializeField] private int id;


    private bool playerEntered;
    private bool cubeEntered;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerEntered = true;
        }
        if(other.CompareTag("Interactable"))
        {
            cubeEntered = true;
        }
        
        if ((playerEntered && !cubeEntered) || (!playerEntered && cubeEntered))
        {
            GameEvents.Instance.DoorTriggerEnter(id);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        
        if(other.CompareTag("Player"))
        {
            playerEntered = false;
        }
        if(other.CompareTag("Interactable"))
        {
            cubeEntered = false;
        }

        if (!playerEntered && !cubeEntered)
        {
            if (GameManager.Instance.GameIsOn)
            {
                GameEvents.Instance.DoorTriggerExit(id);
            }
            
        }



    }
}
