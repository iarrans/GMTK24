using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFromMicrophone : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public Vector3 minScale;
    public Vector3 maxScale;
    public AudioLoudnessDetection detector;
    float loudness = 0;

    public float loudnessSensitibility = 100;
    public float loudnessThreshold = 0.1f;

    private void Update()
    {
        loudness = detector.GetLoudnessFromMicrophone() * loudnessSensitibility;

        if (loudness < loudnessThreshold)
        {
            loudness = 0;
        }

        transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }
}
