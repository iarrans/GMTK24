using TMPro;
using UnityEngine;

namespace UnityWebGLMicrophone
{
    public class ScaleFromWebMicrophone : MonoBehaviour
    {    
        public AudioLoudnessDetection detector;
        public ParticleSystem grabParticles;
        public float maxScale = 10f;
        public float minScale = 0.5f;

        public void Scale()
        {
            float scaleVariation = detector.GetScaleChangeVariation();
            if (scaleVariation == 0) return;
            
            Vector3 newScale = transform.localScale + new Vector3(scaleVariation, scaleVariation, scaleVariation);
            Vector3 newParticleScale = grabParticles.shape.scale + new Vector3(scaleVariation, scaleVariation, scaleVariation);
            if (scaleVariation > 0 && transform.localScale.x > maxScale) {
                newScale = new(maxScale, maxScale, maxScale);
                newParticleScale = grabParticles.shape.scale;
            } else if (scaleVariation < 0 && transform.localScale.x < minScale) {
                newScale = new(minScale, minScale, minScale);
                newParticleScale = grabParticles.shape.scale;
            }

            transform.localScale = newScale;

            var particleShape = grabParticles.shape;
            particleShape.scale = newParticleScale;
        }
    }
}
