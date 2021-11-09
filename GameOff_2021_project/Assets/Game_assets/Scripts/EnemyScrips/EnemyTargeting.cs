using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{

    [SerializeField] private GameObject projectile;
    
    [SerializeField] private float minDistanceToTarget = 10f;
    [SerializeField] private float minDistanceToShoot = 8f;

    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private float reloadTime = 1f;

    [SerializeField] private Transform player;

    [SerializeField] private LayerMask layerForTargeting;

    private float currentDistance;

    private bool reloaded;

    private Transform gunAssembly;
    private Transform invisibleGun;
    private Transform shootPoint;


    private void Start()
    {
        gunAssembly = transform.Find("Gun");
        shootPoint = gunAssembly.Find("ShootPoint");
        invisibleGun = transform.Find("InvisibleGun");

        reloaded = true;
        
    }


    private void Update()
    {
        CalculateDistance();
        
        Target();
        
        Shoot();
    }


    private void Target()
    {
        if (currentDistance <= minDistanceToTarget)
        {
            invisibleGun.LookAt(player);
            //gunAssembly.LookAt(player);
        }

        gunAssembly.transform.rotation = Quaternion.RotateTowards(
            gunAssembly.transform.rotation,
            invisibleGun.transform.rotation, 
            followSpeed * Time.deltaTime);


    }


    private void Shoot()
    {
        Debug.DrawRay(gunAssembly.position, gunAssembly.TransformDirection(Vector3.forward) * minDistanceToShoot, Color.red);

        Ray ray = new Ray(gunAssembly.position, gunAssembly.TransformDirection(Vector3.forward));

        if (Physics.Raycast(ray, out RaycastHit hitInfo, minDistanceToShoot, layerForTargeting)) //TO DO: add a mask !!!
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                //print("puscam playeru");

                if (reloaded)
                {
                    StartCoroutine(Reload());
                    Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);
                    reloaded = false;
                }
                
            }
        }
    }


    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        reloaded = true;
    }
    
    
    private void CalculateDistance() =>
        currentDistance = Vector3.Distance(transform.position, player.transform.position);


  
    

}
