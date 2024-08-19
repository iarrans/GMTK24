using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGMSource;

    public AudioSource MagicSFXSource;

    public AudioSource SFXSource;

    public AudioClip magicDropClip;
    public AudioClip doorClip;

    public static AudioManager instance;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    public void MagicDropClip()
    {
        SFXSource.clip = magicDropClip;
        SFXSource.Play();
    }

    public void PlayDoorClip()
    {
        SFXSource.clip = doorClip;
        SFXSource.Play();
    }

}
