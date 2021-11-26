using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceTriggerScript : MonoBehaviour
{

    [SerializeField] private string soundName;

    [SerializeField] private bool useDelay;
    [SerializeField] private float delay;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!useDelay)
            {
                SoundManager.Instance.PlaySound(soundName);
            }
            else
            {
                StartCoroutine(PlaySoundCoroutine());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.StopSound(soundName);
        }
    }



    private IEnumerator PlaySoundCoroutine()
    {
        yield return new WaitForSeconds(delay);
        SoundManager.Instance.PlaySound(soundName);
    }
    
    
    
}
