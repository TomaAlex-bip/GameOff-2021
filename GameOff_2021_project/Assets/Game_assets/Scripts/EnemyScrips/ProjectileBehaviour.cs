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
        //print("hatz");
        if (other.CompareTag("Player"))
        {
            //print("te-am lovit fraiere");

            CameraEffects.Instance.CameraShake(0.05f, 0.1f);
            
            Destroy(gameObject);
        }
        else if (other.CompareTag("Projectile"))
        {
            //print("se dau cap in cap");
        }
        else
        {
            Destroy(gameObject);
            //print("am dat de perete");
        
        }
    }


    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
