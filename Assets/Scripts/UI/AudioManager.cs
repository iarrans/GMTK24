using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGMSource;

    public AudioSource MagicSFXSource;

    public AudioSource SFXSource;

    public AudioManager instance;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Audio" + AudioListener.volume);
    }

    public void PlayAudioClip(AudioClip audioclip)
    {
        SFXSource.clip = audioclip;
        SFXSource.Play();
    }
   
}
