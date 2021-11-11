using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraEffects : MonoBehaviour
{
    public static CameraEffects Instance { get; private set; }


    [SerializeField] private float bobbingEffectSpeed = 1f;
    [SerializeField] private float bobbingEffectAmplitude = 0.05f;

    [SerializeField] private float fieldOfViewMultiplier = 1.2f;

    //[SerializeField] private float cameraShakeAmplitude = 0.05f;
    //[SerializeField] private float cameraShakeTime = 1f;

    private Vector3 initialPosition;
    
    private float timer = 0;
    private float lerpSpeed = 5f;
    private float speedFactor;

    private float initialFOV;

    private Camera mainCamera;
    
    private PlayerMovement playerMovement;

    //private bool cameraShaking;


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
        playerMovement = PlayerMovement.Instance;
        initialPosition = transform.localPosition;

        mainCamera = GetComponent<Camera>();
        initialFOV = mainCamera.fieldOfView;

    }


    private void Update()
    {
        BobbingEffect();
        
        UpdateFieldOfView();
     
    }

    
    public void CameraShake(float amplitude, float time) => StartCoroutine(ShakeCameraCoroutine(amplitude, time));


    private void BobbingEffect()
    {
        speedFactor = Mathf.Clamp(playerMovement.Speed, 1f, 5f);
        
        // player should be moving and be o the ground for this effect
        if (speedFactor > 1f && !playerMovement.Jumping)
        {
            timer += Time.deltaTime * bobbingEffectSpeed * speedFactor;
            Vector3 newPosition = initialPosition +
                                  new Vector3(
                                      Mathf.Sin(timer / 2f) * bobbingEffectAmplitude * speedFactor,
                                      Mathf.Sin(timer) * bobbingEffectAmplitude * 2f * speedFactor
                                  );
            
            //transform.position = newPosition;
            transform.localPosition = Vector3.Lerp(
                transform.localPosition, 
                newPosition, 
                lerpSpeed * Time.deltaTime);
        }
        else
        {
            //transform.position = initialPosition;
            transform.localPosition = Vector3.Lerp(
                transform.localPosition, 
                initialPosition, 
                lerpSpeed * Time.deltaTime);
        }
    }


    private void UpdateFieldOfView()
    {
        // player should be moving faster than normal, either on the ground or midair
        if (playerMovement.Sprinting)
        {
            mainCamera.fieldOfView = Mathf.Lerp( 
                mainCamera.fieldOfView, 
                initialFOV * fieldOfViewMultiplier, 
                lerpSpeed * Time.deltaTime);
        }
        else
        {
            mainCamera.fieldOfView = Mathf.Lerp( 
                mainCamera.fieldOfView, 
                initialFOV, 
                lerpSpeed * Time.deltaTime);
        }
    }


    private IEnumerator ShakeCameraCoroutine(float amplitude, float time)
    {
        float t = 0f;
        while (t < time)
        {
            t += Time.deltaTime;
            var currentPoisiton = transform.localPosition;
            var x = Random.Range(currentPoisiton.x - amplitude, currentPoisiton.x + amplitude);
            var y = Random.Range(currentPoisiton.y - amplitude, currentPoisiton.y + amplitude);
            var desiredPosition = new Vector3(x, y, initialPosition.z);
            transform.localPosition = desiredPosition;
            yield return null;
        }
        transform.localPosition = initialPosition;
    }
    
    
    // needs more work
    private void ShakeCamera(float amplitude, bool shaking)
    {
        if (shaking)
        {
            var currentPosition = transform.localPosition;
            var x = Random.Range(currentPosition.x - amplitude, currentPosition.x + amplitude);
            var y = Random.Range(currentPosition.y - amplitude, currentPosition.y + amplitude);

            var desiredPosition = new Vector3(x, y, currentPosition.z);
            transform.localPosition = desiredPosition;

        }

        if (!shaking)
        {
            transform.localPosition = initialPosition;

        }

    }
    
    
}
