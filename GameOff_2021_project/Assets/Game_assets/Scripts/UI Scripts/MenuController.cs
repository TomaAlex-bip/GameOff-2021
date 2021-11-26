using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class MenuController : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 5f;
    
    [SerializeField] private Image mainPanel;
    [SerializeField] private Image infoPanel;
    [SerializeField] private Image settingsPanel;
    [SerializeField] private Image exitPanel;
    [SerializeField] private Image fadePanel;

    [SerializeField] private Object firstLevel;

    private Image currentPanel;

    private bool inTransition = false;

    private bool menuOn = false;
    
    private void Start()
    {
        currentPanel = mainPanel;

        infoPanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(false);
        exitPanel.gameObject.SetActive(false);
        fadePanel.gameObject.SetActive(false);

        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            mainPanel.gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            fadePanel.gameObject.SetActive(false);

            
            if (!GameManager.Instance.FinalLevel)
            {
                
                
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    menuOn = !menuOn;
                    var player = PlayerManager.Instance.gameObject;
                    var camLook = player.GetComponent<CameraLook>();
                    camLook.enabled = !menuOn;
                    mainPanel.gameObject.SetActive(menuOn);

                    Cursor.lockState = menuOn ? CursorLockMode.None : CursorLockMode.Locked;

                    GameManager.Instance.GameIsOn = !menuOn;
                    //Time.timeScale = menuOn ? 0f : 1f;

                }
            }


        }
    }


    private IEnumerator ChangePanel(Image newPanel)
    {
        inTransition = true;
        
        newPanel.gameObject.SetActive(true);
        
        var newGroup = newPanel.GetComponent<CanvasGroup>();
        var currentGroup = currentPanel.GetComponent<CanvasGroup>();
        while (currentGroup.alpha > 0.0001f)
        {
            //print("da");
            currentGroup.alpha = Mathf.Lerp(currentGroup.alpha, 0f, fadeSpeed * Time.deltaTime);
            newGroup.alpha = 1 - Mathf.Lerp(currentGroup.alpha, 0f, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        currentPanel.gameObject.SetActive(false);
        currentPanel = newPanel;

        inTransition = false;
    }


    private IEnumerator StartFadeEffect()
    {
        inTransition = true;
        fadePanel.gameObject.SetActive(true);
        currentPanel.gameObject.SetActive(false);

        var fadePanelGroup = fadePanel.GetComponent<CanvasGroup>();
        while (fadePanelGroup.alpha < 0.999f)
        {
            fadePanelGroup.alpha = Mathf.Lerp(fadePanelGroup.alpha, 1f, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        
        SceneManager.LoadScene(firstLevel.name);
        SoundManager.Instance.StopSound("m_MainMenu_theme");
        inTransition = false;
    }


    public void OnInfoButton()
    {
        if(!inTransition)
            StartCoroutine(ChangePanel(infoPanel));
    }

    public void OnSettingsButton()
    {
        if(!inTransition)
            StartCoroutine(ChangePanel(settingsPanel));
    }

    public void OnCloseButton()
    {
        if(!inTransition)
            StartCoroutine(ChangePanel(mainPanel));
    }

    public void OnStartButton()
    {
        StartCoroutine(StartFadeEffect());
    }

    public void OnExitButton()
    {
        if(!inTransition)
            StartCoroutine(ChangePanel(exitPanel));
    }

    public void OnYesButton()
    {
        Application.Quit();
    }
    
    public void OnNoButton()
    {
        if(!inTransition)
            StartCoroutine(ChangePanel(mainPanel));
    }

    public void OnYesMenuButton()
    {
        SceneManager.LoadScene(0);
        SoundManager.Instance.StopCurrenAmbientalSound();
        SoundManager.Instance.StopAllSounds();
        SoundManager.Instance.PlaySound("m_MainMenu_theme");
    }

    public void OnContinueButton()
    {
        menuOn = false;
        var player = PlayerManager.Instance.gameObject;
        var camLook = player.GetComponent<CameraLook>();
        camLook.enabled = !menuOn;
        mainPanel.gameObject.SetActive(menuOn);
        
        Cursor.lockState = menuOn ? CursorLockMode.None : CursorLockMode.Locked;
        
        GameManager.Instance.GameIsOn = !menuOn;
        
        //Time.timeScale = menuOn ? 0f : 1f;
    }

}
