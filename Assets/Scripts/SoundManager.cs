using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;

    //public AudioSource audioSource;

    private void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //public void PlaySound(AudioClip clip)
    //{
    //    audioSource.PlayOneShot(clip);
    //}

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
}