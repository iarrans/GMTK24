using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MicrophoneSelectorBehaviour : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    
    public void Start()
    {
        dropdown.ClearOptions();
        foreach(var microphoneName in Microphone.devices)
        {
            TMP_Dropdown.OptionData optionData = new (microphoneName);
            dropdown.options.Add(optionData);
        }
        dropdown.value = 0;
    }
}
