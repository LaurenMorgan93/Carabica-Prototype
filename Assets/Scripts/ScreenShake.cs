using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    
    // this script is attached to the Dashboard Cam
    [Header("Shake Settings")]
    public float shakeTime = 0.5f; // Duration
    public float shakeStrength = 0.5f; // Magnitude 
    public bool shakeOnAwake = false; // shake on start flag 

    private Vector3 originalPosition; 
    void Start()
    {
       // store the original position 
        originalPosition = transform.localPosition;

        // check shake on start 
        if (shakeOnAwake)
        {
            StartCoroutine(Shake(shakeTime, shakeStrength));
        }
    }

    // Call this function to shake
    public void TriggerShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    // Coroutine  yippee : D
    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // shake up / down 
            float y = Random.Range(-1f, 1f) * magnitude;

            // update local pos (x remains original, only y is changed)
            transform.localPosition = new Vector3(originalPosition.x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Reset position
        transform.localPosition = originalPosition;
    }
}