using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFromMicrophone : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    //public Vector3 minScale;
    //public Vector3 maxScale;
    public AudioLoudnessDetection detector;
    float loudness = 0;

    public float loudnessSensitibility = 100;
    public float loudnessThreshold = 0.1f;
    public float scaleThreshold = 10;
    public float scaleVelocity = 10;

    public float minScale = 0.5f;
    public float maxScale = 100f;

    private void Update()
    {
        loudness = detector.GetLoudnessFromMicrophone() * loudnessSensitibility;
        if (loudness < loudnessThreshold)
        {
            loudness = 0;
        }
        Debug.Log(loudness);
        if (loudness < 0.1f) return;
        if (loudness > scaleThreshold)
        {
            
            if (transform.localScale.x < maxScale) transform.localScale = transform.localScale + (new Vector3(scaleVelocity, scaleVelocity, scaleVelocity) * Time.deltaTime * loudness);
        } else
        {
            if (transform.localScale.x > minScale) transform.localScale = transform.localScale - (new Vector3(scaleVelocity, scaleVelocity, scaleVelocity) * Time.deltaTime * (1 - loudness));
        }
    }
}
