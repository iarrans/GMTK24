using System.Linq;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace UnityWebGLMicrophone
{
    public class ScaleFromWebMicrophone : MonoBehaviour
    {    
        public AudioLoudnessDetection detector;
        public ParticleSystem grabParticles;
        public float maxScale = 10f;
        public float minScale = 0.5f;
        public Material blinkShaderMaterial;
        public MeshRenderer meshRenderer;

        private void Update()
        {
            if(Input.GetMouseButtonUp(1)) RemoveBlinkShader();
        }

        public void Scale()
        {
            float scaleVariation = detector.GetScaleChangeVariation();
            if (scaleVariation == 0) return;
            
            Vector3 newScale = transform.localScale + new Vector3(scaleVariation, scaleVariation, scaleVariation);
            Vector3 newParticleScale = grabParticles.shape.scale + new Vector3(scaleVariation, scaleVariation, scaleVariation);
            if (newScale.x > maxScale) {
                newScale = new(maxScale, maxScale, maxScale);
                newParticleScale = grabParticles.shape.scale;
            } else if (newScale.x < minScale) {
                newScale = new(minScale, minScale, minScale);
                newParticleScale = grabParticles.shape.scale;
            }

            List<Material> materials = meshRenderer.materials.ToList();
            if (newScale.x >= maxScale || newScale.x <= minScale)
            {
                
                if (materials.Count == 1)
                {
                    materials.Add(blinkShaderMaterial);
                    meshRenderer.materials = materials.ToArray();
                }
            } else if(materials.Count == 2)
            {
                materials.RemoveAt(1);
                meshRenderer.materials = materials.ToArray();
            }

            transform.localScale = newScale;

            var particleShape = grabParticles.shape;
            particleShape.scale = newParticleScale;
        }

        public void RemoveBlinkShader()
        {
            List<Material> materials = meshRenderer.materials.ToList();
            if (materials.Count == 2)
            {
                materials.RemoveAt(1);
                meshRenderer.materials = materials.ToArray();
            }
        }
    }
}
