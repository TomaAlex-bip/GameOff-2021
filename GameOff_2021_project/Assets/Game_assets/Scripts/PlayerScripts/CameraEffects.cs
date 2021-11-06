using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CameraEffects : MonoBehaviour
{
    [SerializeField] private float bobbingEffectSpeed = 1f;
    [SerializeField] private float bobbingEffectAmplitude = 0.05f;


    private Vector3 initialPosition;
    
    private float timer = 0;
    private float lerpSpeed = 5f;
    private float speedFactor;
    
    private PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = PlayerMovement.Instance;
        initialPosition = transform.localPosition;
    }


    private void Update()
    {
        BobbingEffect();
    }


    private void BobbingEffect()
    {
        speedFactor = Mathf.Clamp(playerMovement.Speed, 1f, 5f);
        
        if (speedFactor > 1f)
        {
            timer += Time.deltaTime * bobbingEffectSpeed * speedFactor;
            Vector3 newPosition = initialPosition +
                                  new Vector3(
                                      Mathf.Sin(timer / 2f) * bobbingEffectAmplitude * speedFactor,
                                      Mathf.Sin(timer) * bobbingEffectAmplitude * 2f * speedFactor
                                  );
            
            //transform.position = newPosition;
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, lerpSpeed * Time.deltaTime);
        }
        else
        {
            //transform.position = initialPosition;
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, lerpSpeed * Time.deltaTime);
        }
    }
    
    
    
}
