using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class EndGameTriggerScript : MonoBehaviour
{

    [SerializeField] private float timer;

    [SerializeField] private Object creditsScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.FinalLevel = true;
            
            StartCoroutine(FinishGame());
        }
    }

    private IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(timer);

        SceneManager.LoadScene(creditsScene.name);
        GameManager.Instance.FinalLevel = false;
        SoundManager.Instance.StopAllSounds();
    }
    
}
