using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityWebGLMicrophone;

public class PlayerScaleObject : MonoBehaviour
{
    private ScaleFromWebMicrophone objectToScale;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;

    void FixedUpdate()
    {
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, Mathf.Infinity, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent<ScaleFromWebMicrophone>(out objectToScale))
                    {
                        if (Input.GetMouseButtonDown(1)) objectToScale.detector.ClearSamples();
                        if (Input.GetMouseButton(1)) objectToScale.Scale();
            }
                }
            }        
}
