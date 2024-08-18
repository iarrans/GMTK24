using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleFeedbackBehaviour : MonoBehaviour
{
    public Image increaseZone;
    public Color increaseZoneActiveColor;
    public Color increaseZoneNotActiveColor;
    public Image decreaseZone;
    public Color decreaseZoneActiveColor;
    public Color decreaseZoneNotActiveColor;
    public AudioLoudnessDetection audioLoudnessDetection;

    void Update()
    {
        float scaleVariation = audioLoudnessDetection.GetScaleChangeVariation();
        if(scaleVariation > 0 )
        {
            Debug.Log("INCREASE");
            increaseZone.color = increaseZoneActiveColor;
            decreaseZone.color = decreaseZoneNotActiveColor;
        } else if( scaleVariation < 0 )
        {
            Debug.Log("DECREASE");
            increaseZone.color = increaseZoneNotActiveColor;
            decreaseZone.color = decreaseZoneActiveColor;
        } else
        {
            Debug.Log("DEACTIVATING BOTH");
            increaseZone.color = increaseZoneNotActiveColor;
            decreaseZone.color = decreaseZoneNotActiveColor;
        }
    }
}
