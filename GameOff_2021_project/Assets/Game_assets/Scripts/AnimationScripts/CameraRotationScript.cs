using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraRotationScript : MonoBehaviour
{
    [SerializeField] private float speed;

    private bool finished;

    private void Start()
    {
        finished = true;
    }

    private void Update()
    {
        if(finished)
        {
            finished = false;
            StartCoroutine(RandomRotation());
        }
    }

    private IEnumerator RandomRotation()
    {
        var rot = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        while(transform.rotation != rot)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, speed * Time.deltaTime);
            yield return null;
        }
        finished = true;
    }
}
