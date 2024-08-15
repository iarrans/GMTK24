using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSliderBehaviour : MonoBehaviour
{
    void Start()
    {
        GetComponent<Slider>().value = AudioListener.volume;
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}
