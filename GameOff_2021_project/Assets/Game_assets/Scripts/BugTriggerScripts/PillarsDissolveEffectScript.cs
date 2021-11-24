using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarsDissolveEffectScript : MonoBehaviour
{

    [SerializeField] private float dissolveSpeedStep = 0.01f;

    [SerializeField] private List<GameObject> objectsToDissolve;

    private List<Material> materials = new List<Material>();


    private const float DISSOLVE_START_POINT = -0.5f;
    private const float DISSOLVE_END_POINT = 1.5f;


    private bool dissolved = false;
    
    
    private void Start()
    {
        foreach (var obj in objectsToDissolve)
        {
            var rend = obj.GetComponent<Renderer>();
            var mat = rend.material;
            materials.Add(mat);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!dissolved)
        {
            dissolved = true;
            StartCoroutine(StartDissolving());
        }
    }



    private IEnumerator StartDissolving()
    {
        var dissolveStatus = materials[0].GetFloat("Dissolve_Factor");
        
        while (dissolveStatus >= DISSOLVE_START_POINT && dissolveStatus <= DISSOLVE_END_POINT)
        {
            dissolveStatus += dissolveSpeedStep;
            foreach (var material in materials)
            {
                material.SetFloat("Dissolve_Factor", dissolveStatus);
            }
            
            yield return null;
        }
    }
    
}
