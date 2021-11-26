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

    [SerializeField] private float finalThankYouDelay = 12f;


    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        // print(transform.position.y);

        if (transform.position.y >= 2000f)
        {
            SoundManager.Instance.PlaySound("FinalThankYou");
            // print("ceva");
            StartCoroutine(FinalTankYouCoroutine());
        }
        
        
    }


    private IEnumerator FinalTankYouCoroutine()
    {
        // print("aproape gata");
        yield return new WaitForSeconds(finalThankYouDelay);
        // print("gata");
        SceneManager.LoadScene(0);
    }


}
