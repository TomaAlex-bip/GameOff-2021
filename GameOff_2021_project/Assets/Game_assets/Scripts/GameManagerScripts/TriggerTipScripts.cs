using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTipScripts : MonoBehaviour
{
    [SerializeField] private int id;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEvents.Instance.TipTriggerEnter(id);
            gameObject.SetActive(false);
            SoundManager.Instance.PlaySound("e_info_sound");
        }
    }
}


