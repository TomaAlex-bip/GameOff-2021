using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertControlsScript : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EnterInvertControlsTrigger(true));
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EnterInvertControlsTrigger(false));
        }
    }


    private IEnumerator EnterInvertControlsTrigger(bool ceva)
    {
        yield return new WaitForSeconds(1f);
        PlayerMovement.Instance.InvertControls = ceva;
    }
    
    
    
}
