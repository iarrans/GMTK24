using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityWebGLMicrophone;

public class AudioLoudnessDetection : MonoBehaviour
{
    public static float microphoneSensitivity = 0.3f;

    // Start is called before the first frame update
    public int sampleWindow = 264;
    private AudioClip microphoneClip;
    private int microphoneIndex;
    public float minScaleThreshold = 0.001f;
    
    public float scaleVelocity = 10f;
    public TextMeshProUGUI microphoneVolume;
    public Slider loudnessSlider;
    public int maxLoudnessSamplesSize = 200;

    private List<float> loudnessSamples = new();

#if UNITY_WEBGL && !UNITY_EDITOR
    void Awake()
    {
        UnityWebGLMicrophone.AudioLoudnessDetectionWebGL.Awake();
    }
#endif

    void Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        MicrophoneToAudioClip();
#endif
    }

    public float GetScaleChangeVariation()
    {
        float loudness;
#if UNITY_WEBGL && !UNITY_EDITOR
        loudness = UnityWebGLMicrophone.AudioLoudnessDetectionWebGL.GetScaleChangeVariation(microphoneIndex);
#endif
#if !UNITY_WEBGL || UNITY_EDITOR
        loudness = GetLoudnessFromMicrophone() * 10;
#endif

        if (loudness < minScaleThreshold) loudness = 0f;
        loudness = Mathf.Clamp01(loudness);
        if (loudness != 0)
        {
            loudnessSamples.Add(loudness);
            if (loudnessSamples.Count > maxLoudnessSamplesSize) loudnessSamples.RemoveAt(0);
            float mean = 0;
            loudnessSamples.ForEach(l => mean += l);
            loudness = mean / loudnessSamples.Count;
        } else
        {
            ClearSamples();
        }
        
        if (microphoneVolume != null) microphoneVolume.text = loudness.ToString();
        if (loudnessSlider != null) loudnessSlider.value = loudness;

        if (loudness == 0f) return 0f;
        if (loudness > microphoneSensitivity)
        {
            return scaleVelocity * Time.deltaTime * (1 + loudness);
        }
        else
        {
            return -scaleVelocity * Time.deltaTime * (1 - loudness);
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
    public void ClearSamples()
    {
        loudnessSamples.Clear();
    }

    private void Update()
    {
        GetScaleChangeVariation();
    }
}

#if UNITY_WEBGL && !UNITY_EDITOR
namespace UnityWebGLMicrophone
{
    public class AudioLoudnessDetectionWebGL
    {
        public static void Awake()
        {
            Microphone.Init();
            Microphone.QueryAudioInput();
        }

        public static float GetScaleChangeVariation(int microphoneIndex) {
            Microphone.Update();
        float[] volumes = Microphone.volumes;
            
        return volumes[microphoneIndex];
        }
    }
}
#endif