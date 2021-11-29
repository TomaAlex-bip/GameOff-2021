using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsAnimationScript : MonoBehaviour
{

    [SerializeField] private float speed;

    [SerializeField] private float finalThankYouDelay;

    private bool done;


    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        var y = transform.localPosition.y;

        if (y >= 3500f && !done)
        {
            done = true;
            if(SoundManager.Instance != null)
            {
                SoundManager.Instance.StopAllSounds();
                SoundManager.Instance.PlaySound("FinalThankYou");
            }
        }

        if (y >= 7200)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.StopAllSounds();
                SoundManager.Instance.PlaySound("m_MainMenu_theme");
            }

            SceneManager.LoadScene(0);
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }



}
