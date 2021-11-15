using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipFadeIn : MonoBehaviour
{

    [SerializeField] private int id;

    [SerializeField] private float activeTime;
    [SerializeField] private float fadeSpeed;


    private CanvasGroup canvasGroup;
    
    private void Start()
    {
        GameEvents.Instance.onTipTriggerEnter += TipAnimation;

        canvasGroup = transform.GetComponent<CanvasGroup>();

    }

    private void TipAnimation(int idceva)
    {
        if (id == idceva)
        {
            StartCoroutine(FadeIn());
        }
    }


    private IEnumerator FadeIn()
    {
        var t = 0f;
        while (t <= activeTime)
        {
            t += Time.deltaTime;

            canvasGroup.alpha = Mathf.Clamp01(t);
            
            yield return null;

        }

        yield return new WaitForSeconds(2f);
        canvasGroup.alpha = 0;
    }
    
}
