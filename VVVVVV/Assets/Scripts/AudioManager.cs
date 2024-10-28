using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Debug.Log("Más de un AudioManager en escena");
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
}
