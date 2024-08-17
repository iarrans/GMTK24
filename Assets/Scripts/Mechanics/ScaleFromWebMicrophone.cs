using TMPro;
using UnityEngine;

namespace UnityWebGLMicrophone
{
    public class ScaleFromWebMicrophone : MonoBehaviour
    {    
        public AudioLoudnessDetection detector;
        public float maxScale = 10f;
        public float minScale = 0.5f;

        void Update()
        {
            float scaleVariation = detector.GetScaleChangeVariation();
            if (scaleVariation == 0 || !Input.GetMouseButton(1)) return;
            Vector3 newScale = transform.localScale + new Vector3(scaleVariation, scaleVariation, scaleVariation);
            if (scaleVariation > 0 && transform.localScale.x > maxScale) newScale = new(maxScale, maxScale, maxScale);
            if (scaleVariation < 0 && transform.localScale.x < minScale) newScale = new(minScale, minScale, minScale);

            Debug.Log(newScale);
            transform.localScale = newScale;
        }
    }
}
