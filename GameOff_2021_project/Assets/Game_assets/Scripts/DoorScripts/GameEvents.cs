using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }


    public event Action<int> onDoorTriggerEnter;
    public void DoorTriggerEnter(int id)
    {
        onDoorTriggerEnter?.Invoke(id);
    }

    
    
    public event Action<int> onDoorTriggerExit;
    public void DoorTriggerExit(int id)
    {
        onDoorTriggerExit?.Invoke(id);
    }

}
