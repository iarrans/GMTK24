using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UnityWebGLMicrophone
{
    public class AudioLoudnessDetection : MonoBehaviour
    {
        // Start is called before the first frame update
        public int sampleWindow = 64;
        private AudioClip microphoneClip;
        private int microphoneIndex;
        public float minScaleThreshold = 0.001f;
        public float scaleThreshold = 0.1f;
        public float scaleVelocity = 10f;
        public TextMeshProUGUI microphoneVolume;

#if UNITY_WEBGL && !UNITY_EDITOR
    void Awake()
    {
        Microphone.Init();
        Microphone.QueryAudioInput();
    }
#endif

        void Start()
        {
#if !UNITY_WEBGL
            MicrophoneToAudioClip();
#endif
        }

        public float GetScaleChangeVariation()
        {
            float loudness;
#if UNITY_WEBGL && !UNITY_EDITOR
        Microphone.Update();
        float[] volumes = Microphone.volumes;
            
        loudness = volumes[microphoneIndex];
#endif
#if !UNITY_WEBGL || UNITY_EDITOR
            loudness = GetLoudnessFromMicrophone() * 10;
#endif

            microphoneVolume.text = loudness.ToString();
            if (loudness < minScaleThreshold) return 0f;
            if (loudness > scaleThreshold)
            {
                return scaleVelocity * Time.deltaTime;
            }
            else
            {
                return -scaleVelocity * Time.deltaTime;
            }
        }
#if !UNITY_WEBGL || UNITY_EDITOR
        public void MicrophoneToAudioClip()
        {
            //First microphone in device list
            string microphoneName = Microphone.devices[0];
            microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
        }

        public float GetLoudnessFromMicrophone()
        {
            return GetLoudnessFromAudioClips(Microphone.GetPosition(Microphone.devices[microphoneIndex]), microphoneClip);
        }

        public void ChangeMicrophoneIndex(int index)
        {
            microphoneIndex = index;
        }

        public float GetLoudnessFromAudioClips(int clipPosition, AudioClip clip)
        {
            int startPosition = clipPosition - sampleWindow;

            if (startPosition < 0)
            {
                return 0;
            }

            float[] waveData = new float[sampleWindow];
            clip.GetData(waveData, startPosition);

            //compute loudness
            float totalLoudness = 0;

            for (int i = 0; i < sampleWindow; i++)
            {
                totalLoudness += Mathf.Abs(waveData[i]);
            }

            return totalLoudness / sampleWindow;
        }
#endif
    }
}