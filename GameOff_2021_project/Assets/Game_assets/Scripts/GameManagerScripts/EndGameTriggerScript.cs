using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTriggerScript : MonoBehaviour
{

    [SerializeField] private float timer;


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

        SceneManager.LoadScene(0);
        GameManager.Instance.FinalLevel = false;
    }
    
}
