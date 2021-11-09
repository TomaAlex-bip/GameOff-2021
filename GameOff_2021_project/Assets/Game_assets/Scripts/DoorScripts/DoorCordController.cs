using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class DoorCordController : MonoBehaviour
{

    [SerializeField] private int id;

    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;
    
    
    private List<Transform> cords = new List<Transform>();


    private void Start()
    {
        var numberOfChilds = transform.childCount;

        //print("number of childs: " + numberOfChilds);
        for (int i = 0; i < numberOfChilds; i++)
        {
            cords.Add(transform.GetChild(i));
            //print(transform.GetChild(i));
        }
        
        

        GameEvents.Instance.onDoorTriggerEnter += PowerOnCords;
        GameEvents.Instance.onDoorTriggerExit += PowerOffCords;

        PowerOffCords(id);
        
    }


    private void PowerOnCords(int id)
    {
        if (this.id == id)
        {
            foreach (var cord in cords)
            {
                var mat = cord.GetComponent<Renderer>().material;
                if (mat != null)
                {
                    mat.color = onColor;
                }
            }
        }

        
        
    }

    private void PowerOffCords(int id)
    {
        if (this.id == id)
        {
            foreach (var cord in cords)
            {
                var mat = cord.GetComponent<Renderer>().material;
                if (mat != null)
                {
                    mat.color = offColor;
                }
            }
        }
        
        

        
    }
    
    
}
