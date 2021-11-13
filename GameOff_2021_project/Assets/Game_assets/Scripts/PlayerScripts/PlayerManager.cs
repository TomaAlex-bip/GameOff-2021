using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public bool InRespawnTime { get => inRespawnTime; }


    [SerializeField] private int initialHitPoints = 10;

    [SerializeField] private float timeToRespawn = 5f;


    private Vector3 checkPoint;

    private int hitPoints;

    private bool inRespawnTime;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }


    private void Start()
    {
        hitPoints = initialHitPoints;
        checkPoint = transform.position + new Vector3(0f, 2f, 0f);
    }

    private void Update()
    {
        if (hitPoints <= 0 && !inRespawnTime)
        {
            inRespawnTime = true;
            hitPoints = initialHitPoints;
            
            print("Gata jocu");

            // animatie
            // particule
            
            //disable movement

            var cameraLook = transform.GetComponent<CameraLook>();
            var movement = transform.GetComponent<PlayerMovement>();

            cameraLook.enabled = false;
            movement.enabled = false;
            
            StartCoroutine(RespawnCoroutine());

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            var spawnPoint = other.transform.Find("SpawnPoint");
            
            checkPoint = spawnPoint.position + new Vector3(0f, 2f, 0f);
        }
    }



    private IEnumerator RespawnCoroutine()
    {
        var time = 0f;
        while (time <= timeToRespawn)
        {
            time += Time.deltaTime;
            yield return null;
        }

        if (time >= timeToRespawn)
        {
            print("te-ai dus al loc");
        
        
            var cameraLook = transform.GetComponent<CameraLook>();
            var movement = transform.GetComponent<PlayerMovement>();

            cameraLook.enabled = true;
            movement.enabled = true;

            transform.position = checkPoint;
        
            inRespawnTime = false;
        }

        
    }


    public void DamagePlayer()
    {
        if (!inRespawnTime)
        {
            hitPoints--;
        }
    }


}
