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
        
        public TextMeshProUGUI microphoneVolume;

        private int microphoneIndex = 0;

#if UNITY_EDITOR

        public AudioLoudnessDetection detector;

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
            string[] devices = Microphone.devices;

            float loudness = GetMicrophoneLoudness();
            microphoneVolume.text = loudness.ToString();
            if (loudness < 0.1f) return;
            if (loudness > scaleThreshold)
            {
                if (transform.localScale.x < maxScale) transform.localScale = transform.localScale + (new Vector3(scaleVelocity, scaleVelocity, scaleVelocity) * Time.deltaTime * loudness);
            } else
            {
                if (transform.localScale.x > minScale) transform.localScale = transform.localScale - (new Vector3(scaleVelocity, scaleVelocity, scaleVelocity) * Time.deltaTime * (1 - loudness));
            }

        }

        private float GetMicrophoneLoudness()
        {
            float loudness = 0;
#if UNITY_WEBGL && !UNITY_EDITOR
            Microphone.Update();
            float[] volumes = Microphone.volumes;
            
            loudness = volumes[microphoneIndex];
#endif
#if !UNITY_WEBGL
            loudness = detector.GetLoudnessFromMicrophone(microphoneIndex) * 10;
#endif
            return loudness;
        }

        public void changeMicrophoneIndex(int index)
        {
            microphoneIndex = index;
        }
    }
}
