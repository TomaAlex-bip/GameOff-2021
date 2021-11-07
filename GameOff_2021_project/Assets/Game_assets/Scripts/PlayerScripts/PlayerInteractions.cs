using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    [SerializeField] private LayerMask pickableObjectsLayerMask;
    [SerializeField] private float minDistanceToPickObject = 1.5f;



    private bool hasObject;

    private Transform objectHolder;
    private Transform mainCamera;

    private Transform objectInHand;
    
    void Start()
    {
        objectHolder = transform.Find("ObjectHolder");
        mainCamera = transform.Find("CameraHolder");
    }

    void Update()
    {
        CheckForPickableObjects(); // checks if the player wants to pick an object or to release it
        
        UpdateObjectInHand(); // updates the properties of the picked object
    }


    private void CheckForPickableObjects()
    {
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward) * minDistanceToPickObject, Color.green);

        if (!hasObject && Input.GetKeyDown(KeyCode.E))
        {
            var ray = new Ray(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward));
            if (Physics.Raycast(ray, out RaycastHit hitInfo, minDistanceToPickObject, pickableObjectsLayerMask))
            {
                //print("il ciordim pe gigel");
                objectInHand = hitInfo.transform;
                hasObject = true;
            }
            
        }
        else if (hasObject && Input.GetKeyDown(KeyCode.E))
        {
            //print("il lasam pe gigel");
            hasObject = false;
        }
    }

    private void UpdateObjectInHand()
    {
        if (hasObject && objectInHand != null)
        {
            var col = objectInHand.GetComponent<BoxCollider>();
            if (col != null)
            {
                col.enabled = false;
            }
                
            var rb = objectInHand.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            objectInHand.SetParent(objectHolder);
            objectInHand.localPosition = Vector3.zero;
            objectInHand.localRotation = Quaternion.identity;
        }

        if (!hasObject && objectInHand != null)
        {
            objectInHand.SetParent(null);
            
            var col = objectInHand.GetComponent<BoxCollider>();
            if (col != null)
            {
                col.enabled = true;
            }

            var rb = objectInHand.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }
    
}
