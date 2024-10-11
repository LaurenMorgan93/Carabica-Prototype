using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundScript : MonoBehaviour
{
    public AudioClip[] audioClips;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void PlaySoundEffect(int soundId)
    {
        _audioSource.clip = audioClips[soundId];
        
        _audioSource.Play();
    }
}
