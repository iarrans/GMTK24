using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
    public Slider loudnessSlider;
    public int maxLoudnessSamplesSize = 200;

    private List<float> loudnessSamples = new List<float>();

#if UNITY_WEBGL && !UNITY_EDITOR
    void Awake()
    {
        AudioLoudnessDetectionWebGL.Awake();
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
        loudness = AudioLoudnessDetectionWebGL.GetScaleChangeVariation();
#endif
#if !UNITY_WEBGL || UNITY_EDITOR
        loudness = GetLoudnessFromMicrophone() * 10;
#endif

        if (loudness < minScaleThreshold) loudness = 0f;
        loudness = Mathf.Clamp01(loudness);
        loudnessSamples.Add(loudness);
        if (loudnessSamples.Count > maxLoudnessSamplesSize) loudnessSamples.RemoveAt(0);
        float mean = 0;
        loudnessSamples.ForEach(l => mean += l);
        Debug.Log(mean / loudnessSamples.Count);
        loudness = mean / loudnessSamples.Count;
        microphoneVolume.text = loudness.ToString();
        loudnessSlider.value = loudness;

        if (loudness == 0f) return 0f;
        if (loudness > scaleThreshold)
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

        public static float GetScaleChangeVariation() {
            Microphone.Update();
        float[] volumes = Microphone.volumes;
            
        return loudness = volumes[microphoneIndex];
        }
    }
}
#endif