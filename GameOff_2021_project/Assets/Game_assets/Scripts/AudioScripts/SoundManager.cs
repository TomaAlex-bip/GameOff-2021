using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; private set; }


    [SerializeField] public List<Sound> sounds;


    private AudioSource currentAmbientalSound = null;

    private bool stepDone;

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
        
        foreach (var sound in sounds)
        {
            sound.AudioSource = gameObject.AddComponent<AudioSource>();
            
            sound.AudioSource.clip = sound.AudioClip;
            sound.AudioSource.volume = sound.Volume;
            sound.AudioSource.pitch = sound.Pitch;
            sound.AudioSource.loop = sound.Loop;
        }
    }

    private void Start()
    {
        PlaySound("m_MainMenu_theme");
    }

    private void Update()
    {
        foreach (var sound in sounds)
        {
            if (sound.Name[0] == 'm')
            {
                //music
                sound.AudioSource.volume = GameManager.Instance.MusicVolume;
            }
            
            if (sound.Name[0] == 'e')
            {
                //effects
                sound.AudioSource.volume = GameManager.Instance.EffectsVolume;
            }
            
            if (sound.Name[0] == 'v')
            {
                //voice
                sound.AudioSource.volume = GameManager.Instance.VoiceVolume;
            }
        }
    }

    public void PlaySound(string soundName)
    {
        Sound playSound = null;
        foreach (var sound in sounds)
        {
            if (sound.Name == soundName)
            {
                playSound = sound;
                break;
            }
        }

        if (playSound == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
        }
        else
        {
            playSound.AudioSource.Play();
        }
    }
    
    public void PlaySound(string soundName, float delay)
    {
        Sound playSound = null;
        foreach (var sound in sounds)
        {
            if (sound.Name == soundName)
            {
                playSound = sound;
                break;
            }
        }

        if (playSound == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
        }
        else
        {
            playSound.AudioSource.PlayDelayed(delay);
        }
    }


    public void StopSound(string soundName)
    {
        Sound playSound = null;
        foreach (var sound in sounds)
        {
            if (sound.Name == soundName)
            {
                playSound = sound;
                break;
            }
        }

        if (playSound == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
        }
        else
        {
            playSound.AudioSource.Stop();
        }
    }



    private void PlayRandomSound(string soundNamePrefix)
    {
        if (currentAmbientalSound != null)
        {
            if (currentAmbientalSound.isPlaying)
            {
                return;
            }
        }
        
        List<Sound> playSounds = new List<Sound>();
        foreach (var sound in sounds)
        {
            if (sound.Name.Contains(soundNamePrefix))
            {
                playSounds.Add(sound);
            }
        }

        if (playSounds.Count <= 0)
        {
            Debug.LogWarning("Sound " + soundNamePrefix + " not found!");
        }
        else
        {
            var rng = Random.Range(0, playSounds.Count);
            var playSound = playSounds[rng];
            playSound.AudioSource.Play();
            currentAmbientalSound = playSound.AudioSource;
        }
        
    }

    public void PlayRandomAmbientSound() => PlayRandomSound("m_ambiental");
    
    public void StopCurrenAmbientalSound() => currentAmbientalSound.Stop();

    public void PlayRandomStepSound()
    {
        var rng = Random.Range(1, 5);

        var stepSound = "e_step_sound_" + rng;
        
        PlaySound(stepSound);
    }

    public void StopAllSounds()
    {
        foreach (var sound in sounds)
        {
            sound.AudioSource.Stop();
        }
    }


    
    
    
}



