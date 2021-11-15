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

    private Transform cameraHolder;
    private Vector3 desiredPosition = new Vector3(0f, 1.75f, -2f);
    private Vector3 initialPoistion;
    

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

        cameraHolder = transform.Find("CameraHolder");
        initialPoistion = cameraHolder.localPosition;

    }

    private void Update()
    {
        if (hitPoints <= 0 && !inRespawnTime)
        {

            inRespawnTime = true;
            
            print("Gata jocu");

            
            // animatie
            PlayerAnimationController.Instance.FallAnimation();
            
            // particule
            
            
            // disable movement
            var cameraLook = transform.GetComponent<CameraLook>();
            var movement = transform.GetComponent<PlayerMovement>();
            var characterController = transform.GetComponent<CharacterController>();
            characterController.enabled = false;
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
            cameraHolder.localPosition =
                Vector3.Lerp(cameraHolder.transform.localPosition, desiredPosition, 2f*Time.deltaTime);
            var rot = Quaternion.Euler(75f, 0f, 0f);
            cameraHolder.localRotation = Quaternion.Lerp(cameraHolder.localRotation, rot, 2f*Time.deltaTime);
            
            time += Time.deltaTime;
            print(time);
            yield return null;
        }

        print("te-ai dus al loc");
        transform.position = checkPoint;
        cameraHolder.localPosition = initialPoistion;
        cameraHolder.localRotation = Quaternion.identity;

        var cameraLook = transform.GetComponent<CameraLook>();
        var movement = transform.GetComponent<PlayerMovement>();
        var characterController = transform.GetComponent<CharacterController>();
        characterController.enabled = true;
        cameraLook.enabled = true;
        movement.enabled = true;


        hitPoints = initialHitPoints;
        inRespawnTime = false;
        

        
    }


    public void DamagePlayer()
    {
        if (!inRespawnTime)
        {
            hitPoints--;
        }
    }


}
