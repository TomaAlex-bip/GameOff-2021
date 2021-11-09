using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{

    [SerializeField] private float minDistanceToTarget = 10f;
    [SerializeField] private float minDistanceToShoot = 8f;

    [SerializeField] private Transform player;


    private float currentDistance;

    private Transform gunAssembly;
    private Transform shootPoint;

    private void Start()
    {
        gunAssembly = transform.Find("Gun");
        shootPoint = gunAssembly.Find("ShootPoint");
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
            gunAssembly.LookAt(player);
        }
    }


    private void Shoot()
    {
        Debug.DrawRay(gunAssembly.position, gunAssembly.TransformDirection(Vector3.forward) * minDistanceToShoot, Color.red);

        Ray ray = new Ray(gunAssembly.position, gunAssembly.TransformDirection(Vector3.forward));

        if (Physics.Raycast(ray, out RaycastHit hitInfo, minDistanceToShoot)) //TO DO: add a mask !!!
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                //print("puscam playeru");
            }
        }
    }
    
    
    
    private void CalculateDistance() =>
        currentDistance = Vector3.Distance(transform.position, player.transform.position);


}
