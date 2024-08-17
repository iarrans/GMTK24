using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Audio" + AudioListener.volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
