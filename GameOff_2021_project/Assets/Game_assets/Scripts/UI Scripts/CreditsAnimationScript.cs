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


    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;


        if (transform.position.y >= 6000f)
        {
            SceneManager.LoadScene(0);
        }
        
        
    }
    
    
    
}
