using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundManager : MonoBehaviour
{
    public AudioClip[] crashingClips;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayCrashEffect()
    {
        _audioSource.clip = crashingClips[0];
        _audioSource.Play();
    }
}
