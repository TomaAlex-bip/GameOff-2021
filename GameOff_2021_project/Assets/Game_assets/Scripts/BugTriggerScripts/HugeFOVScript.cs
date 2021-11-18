using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HugeFOVScript : MonoBehaviour
{
    public bool enableFovBug;

    [SerializeField] private Volume volume;
    
    [SerializeField] private float initialFOV = 60f;

    [SerializeField] private GameObject playerMesh;
    
    private PlayerManager playerManager;

    private Camera playerCamera;


    private void Start()
    {
        playerManager = PlayerManager.Instance;
        var cam = playerManager.transform.Find("CameraHolder").GetChild(0); 
        playerCamera = cam.GetComponent<Camera>();
        
    }

    private void Update()
    {
        if (enableFovBug)
        {
            var fov = CameraEffects.Instance.FieldOfViewBugEffect();
            playerCamera.fieldOfView = fov;

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enableFovBug = true;
            var rend = playerMesh.GetComponent<Renderer>();
            rend.enabled = false;

            var cameraFx = CameraEffects.Instance;
            cameraFx.ChangableFov = false;

            VolumeProfile profile = volume.sharedProfile;

            if (profile.TryGet<PaniniProjection>(out var paniniProjection))
            {
                paniniProjection.active = true;
            }

        }
    }
    
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enableFovBug = false;
            playerCamera.fieldOfView = initialFOV;
            var rend = playerMesh.GetComponent<Renderer>();
            rend.enabled = true;
            
            var cameraFx = CameraEffects.Instance;
            cameraFx.ChangableFov = true;
            
            VolumeProfile profile = volume.sharedProfile;

            if (profile.TryGet<PaniniProjection>(out var paniniProjection))
            {
                paniniProjection.active = false;
            }
            
            
        }
    }
    
    
    
    
}
