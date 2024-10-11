using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRevSounds : MonoBehaviour
{
    public AudioClip carRevSound;
    private AudioSource _audioSource;

    public CarController carController;

    private float _speed;
    public Vector2 minMaxRevSoundSpeed;
    public Vector2 minMaxCarSpeed;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }
    void Update()
    {
        _speed = carController.travelSpeed;

        _audioSource.pitch = MathEva.map(_speed, minMaxCarSpeed.x, minMaxCarSpeed.y, minMaxRevSoundSpeed.x, minMaxRevSoundSpeed.y);
        
    }
}

public class MathEva
{
    static public float map(float value, 
        float istart, 
        float istop, 
        float ostart, 
        float ostop) {
        return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
    }
}
