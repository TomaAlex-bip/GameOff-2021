using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightBulbRandomRotateScript : MonoBehaviour
{

    [SerializeField] private bool hasToRotate = true;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float minTimeToRotate;
    [SerializeField] private float maxTimeToRotate;

    private bool inCoroutine = false;
    private void Update()
    {
        while (hasToRotate && !inCoroutine)
        {
            inCoroutine = true;
            StartCoroutine(RotateRandom());
        }
    }


    private IEnumerator RotateRandom()
    {
        var rngRot = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

        var timer = 0f;
        var timeToRotate = Random.Range(minTimeToRotate, maxTimeToRotate);
        
        
        while (timer <= timeToRotate)
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rngRot, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        inCoroutine = false;
    }


    
    
}
