using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<Transform> rooms;

    [SerializeField] private List<Transform> triggers;

    private List<RoomTrigger> roomTriggers;
    
    private void Start()
    {
        roomTriggers = new List<RoomTrigger>();
        
        foreach (var level in rooms)
        {
            level.gameObject.SetActive(false);
        }

        foreach (var trigger in triggers)
        {
            roomTriggers.Add(trigger.GetComponent<RoomTrigger>());
        }
        
    }


    private void Update()
    {
        for (var i = 0; i < roomTriggers.Count; i++)
        {
            rooms[i].gameObject.SetActive(roomTriggers[i].PlayerInside);
        }
    }
}
