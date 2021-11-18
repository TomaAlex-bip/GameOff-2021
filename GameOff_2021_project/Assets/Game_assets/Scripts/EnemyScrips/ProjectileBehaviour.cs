using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TO DO: gaseste chestia aia ca sa verifice sa aiba rigidbody
public class ProjectileBehaviour : MonoBehaviour
{

    [SerializeField] private float speed;

    private Rigidbody rb;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();

        StartCoroutine(Destroy());

    }

    private void Update()
    {
        rb.velocity = transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {

       
        switch (other.tag)
        {
            case "Player":
                
                CameraEffects.Instance.CameraShake(0.05f, 0.1f);
                Destroy(gameObject);
                PlayerManager.Instance.DamagePlayer();
                
                break;
            
            case "Projectile":
                break;
            
            case "EffectsTrigger":
                break;
            
            default:
                print("proiectilu loveste " + other.gameObject);
                Destroy(gameObject);
                break;
        }
        
        
    }


    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
