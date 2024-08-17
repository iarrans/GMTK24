using TMPro;
using UnityEngine;

namespace UnityWebGLMicrophone
{
    public class ScaleFromWebMicrophone : MonoBehaviour
    {

        public float loudnessSensitibility = 100;
        public float loudnessThreshold = 0.1f;
        public float scaleThreshold = 10;
        public float scaleVelocity = 10;

        public float minScale = 0.5f;
        public float maxScale = 100f;

        public TextMeshProUGUI microphoneName;
        public TextMeshProUGUI microphoneVolume;

#if UNITY_EDITOR

        public AudioLoudnessDetection detector;
        float loudness = 0;

#endif

#if UNITY_WEBGL && !UNITY_EDITOR
        void Awake()
        {
            Microphone.Init();
            Microphone.QueryAudioInput();
        }
#endif

     void Update()
        {
#if UNITY_WEBGL && !UNITY_EDITOR

            Microphone.Update();
        
#endif

            string[] devices = Microphone.devices;
            microphoneName.text = devices[0];

#if UNITY_WEBGL && !UNITY_EDITOR
            float[] volumes = Microphone.volumes;

             float loudness = volumes[0];

             microphoneVolume.text = volumes[0].ToString();

            if (loudness < loudnessThreshold)
            {
                loudness = 0;
            }
            Debug.Log(loudness);
            if (loudness < 0.0001f) return;
            if (loudness > 0.2f)
            {

                if (transform.localScale.x < maxScale) transform.localScale = transform.localScale + (new Vector3(scaleVelocity, scaleVelocity, scaleVelocity) * Time.deltaTime);
            }
            else
            {
                if (transform.localScale.x > minScale) transform.localScale = transform.localScale - (new Vector3(scaleVelocity, scaleVelocity, scaleVelocity) * Time.deltaTime);
            }

#endif

#if UNITY_EDITOR

            loudness = detector.GetLoudnessFromMicrophone() * loudnessSensitibility;

            microphoneVolume.text = loudness.ToString();


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

#endif

        }
    }
}
