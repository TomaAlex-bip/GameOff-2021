using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class FinalRoomTrigger : MonoBehaviour
{
    [SerializeField] private Object nextScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.StopAllSounds();
            SceneManager.LoadScene(nextScene.name);
        }
    }
}
