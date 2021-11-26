using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsAnimationScript : MonoBehaviour
{

    // start -700
    // end 6000

    [SerializeField] private float speed;

    [SerializeField] private float finalThankYouDelay;

    private void Start()
    {
        StartCoroutine(FinalTankYouCoroutine());
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.StopAllSounds();
            SoundManager.Instance.PlaySound("FinalThankYou", 12.5f);
        }
    }

    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

    }


    private IEnumerator FinalTankYouCoroutine()
    {
        yield return new WaitForSeconds(finalThankYouDelay);
        SceneManager.LoadScene(0);
        if(SoundManager.Instance != null)
            SoundManager.Instance.PlaySound("m_MainMenu_theme");
        print("HAAATZ");
    }


}
