using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public string Name
    {
        get => name;
    }
    public AudioClip AudioClip
    {
        get => audioClip;
    }
    public float Volume
    {
        get => volume;
    }
    public float Pitch
    {
        get => pitch;
    }
    public bool Loop
    {
        get => loop;
    }
    public AudioSource AudioSource
    {
        get => audioSource;
        set => audioSource = value;
    }


    [SerializeField] private string name;
    [SerializeField] private AudioClip audioClip;
    
    [Range(0f, 1f)]
    [SerializeField] private float volume = 0.5f;
    [Range(.1f, 3f)]
    [SerializeField] private float pitch = 1f;

    [SerializeField] private bool loop = false;

    private AudioSource audioSource;

}
