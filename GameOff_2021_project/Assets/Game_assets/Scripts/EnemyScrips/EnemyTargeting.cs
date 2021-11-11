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
    private Transform shield;
    private Transform invisibleShield;
    private Transform shootPoint;

    private Transform targetingArea;
    private Transform shootingArea;

    private void Start()
    {
        gunAssembly = transform.Find("Gun");
        shootPoint = gunAssembly.Find("ShootPoint");
        invisibleGun = transform.Find("InvisibleGun");
        shield = transform.Find("Shield");
        invisibleShield = transform.Find("InvisibleShield");

        targetingArea = transform.Find("Targeting Area");
        shootingArea = transform.Find("Shooting Area");
        reloaded = true;

        targetingArea.localScale = Vector3.one * minDistanceToTarget * 2f;
        shootingArea.localScale = Vector3.one * minDistanceToShoot * 2f;

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
            invisibleShield.LookAt(player);
        }

        gunAssembly.transform.rotation = Quaternion.RotateTowards(
            gunAssembly.rotation,
            invisibleGun.rotation, 
            followSpeed * Time.deltaTime);

        var shieldRot = Quaternion.Euler(
            shield.rotation.eulerAngles.x, 
            invisibleShield.rotation.eulerAngles.y,
            shield.rotation.eulerAngles.z);

        shield.rotation = Quaternion.RotateTowards(
            shield.rotation, 
            shieldRot, 
            followSpeed * Time.deltaTime);

    }


    private void Shoot()
    {
        Debug.DrawRay(gunAssembly.position, gunAssembly.TransformDirection(Vector3.forward) * minDistanceToShoot, Color.red);

        Ray ray = new Ray(gunAssembly.position, gunAssembly.TransformDirection(Vector3.forward));

        if (currentDistance <= minDistanceToShoot)
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, minDistanceToShoot,
                layerForTargeting)) //TO DO: add a mask !!!
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    if (reloaded)
                    {
                        StartCoroutine(Reload());
                        Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);

                        //TO DO: instantiate particles

                        reloaded = false;
                    }

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
