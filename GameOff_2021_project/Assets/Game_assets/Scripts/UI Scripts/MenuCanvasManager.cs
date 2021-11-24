using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvasManager : MonoBehaviour
{

    public static MenuCanvasManager Instance { get; private set; }

    

    public Slider MouseSensitivitySlider
    {
        get => mouseSensitivitySlider;
    }
    public Slider MusicVolumeSlider
    {
        get => musicVolumeSlider;
    }
    public Slider EffectsVolumeSlider
    {
        get => effectsVolumeSlider;
    }
    public Slider VoiceVolumeSlider
    {
        get => voiceVolumeSlider;
    }


    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private Slider voiceVolumeSlider;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }


    private void Start()
    {
        GameManager.Instance.UpdateSliders(mouseSensitivitySlider, musicVolumeSlider, effectsVolumeSlider, voiceVolumeSlider);
    }
}
