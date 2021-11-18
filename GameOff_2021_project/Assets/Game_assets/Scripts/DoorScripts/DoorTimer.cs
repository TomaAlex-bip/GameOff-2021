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

    private Transform timerTrigger;
    private Transform timerObj;

    private Text timerText;

    private bool timerActivated = false;

    private void Start()
    {
        var canv = transform.Find("Canvas");
        timerObj = canv.transform.Find("TimerText");
        timerText = timerObj.GetComponent<Text>();

        var p = transform.parent;
        timerTrigger = p.transform.Find("TimerTrigger");

        timerText.text = "CLOSED";



    }

    private void Update()
    {
        if (activateTimer && !timerActivated)
        {
            timerActivated = true;
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

        timerTrigger.gameObject.SetActive(false);
        timerText.text = "OPEN";
        GameEvents.Instance.DoorTriggerEnter(id);
    }
    
    
    public void ActivateTimer()
    {
        if (!activateTimer)
        {
            activateTimer = true;
        }
    }
    
    
}
