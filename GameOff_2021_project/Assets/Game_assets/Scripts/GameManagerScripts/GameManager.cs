using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public float MouseSensitivity { get => mouseSensitivity; }
    public float MusicVolume { get => musicVolume; }
    public float EffectsVolume { get => effectsVolume; }
    public float VoiceVolume { get => voiceVolume; }

    public bool GameIsOn { get; set; }
    public bool FinalLevel { get; set; }

    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private Slider voiceVolumeSlider;

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float musicVolume;
    [SerializeField] private float effectsVolume;
    [SerializeField] private float voiceVolume;

    [SerializeField] private List<string> musicNames;
    [SerializeField] private List<string> effectsNames;
    [SerializeField] private List<string> voiceNames;


    private List<Sound> sounds;
    

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
        
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        sounds = SoundManager.Instance.sounds;
        
        SoundManager.Instance.PlaySound("m_MainMenu_theme");
        
    }


    private void Update()
    {
        mouseSensitivity = mouseSensitivitySlider.value;
        musicVolume = musicVolumeSlider.value;
        effectsVolume = effectsVolumeSlider.value;
        voiceVolume = voiceVolumeSlider.value;


        
        
        
    }
    
    
    public void UpdateSliders(Slider mouseSensitivitySlider, Slider musicVolumeSlider, Slider effectsVolumeSlider, Slider voiceVolumeSlider)
    {
        this.mouseSensitivitySlider = mouseSensitivitySlider;
        this.musicVolumeSlider = musicVolumeSlider;
        this.effectsVolumeSlider = effectsVolumeSlider;
        this.voiceVolumeSlider = voiceVolumeSlider;

        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            this.mouseSensitivitySlider.value = mouseSensitivity;
            this.musicVolumeSlider.value = musicVolume;
            this.effectsVolumeSlider.value = effectsVolume;
            this.voiceVolumeSlider.value = voiceVolume;
        }
    }
    
}
