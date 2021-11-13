using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorTimer : MonoBehaviour
{
    [SerializeField] private int id;

    [SerializeField] private float timeToOpen = 5f;

    [SerializeField] private bool activateTimer;
    
    private Transform timerObj;

    private Text timerText;
    
    private void Start()
    {
        var canv = transform.Find("Canvas");
        timerObj = canv.transform.Find("TimerText");
        timerText = timerObj.GetComponent<Text>();

        //timerText = timerObj.GetComponent<TextMesh>();

    }

    private void Update()
    {
        if (activateTimer)
        {
            activateTimer = false;
            StartCoroutine(TimerCoroutine());
        }
        
    }


    private IEnumerator TimerCoroutine()
    {
        var time = 0f;
        
        while (time < timeToOpen)
        {
            time += Time.deltaTime;
            var timeLeft = timeToOpen - time;

            timerText.text = timeLeft.ToString("00.00");

            yield return null;
        }

        timerText.text = "OPEN";
        GameEvents.Instance.DoorTriggerEnter(id);
    }
    
    
}
