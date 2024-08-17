using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickUpDrop : MonoBehaviour
{
    public bool pressingKey;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;

    public ObjectGrababble objectGrababble;
    public float mouseScrollY;

    void Update()
    {
        if (pressingKey)
        {
            if (objectGrababble == null)
            {
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, Mathf.Infinity, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent<ObjectGrababble>(out objectGrababble))
                    {
                        objectGrababble.Grab(objectGrabPointTransform);
                    }
                }
            } //Que no se pueda levitar a traves de la pared
            else if (objectGrababble != null && Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, Mathf.Infinity, pickUpLayerMask))
            {
                if (objectGrababble.gameObject != raycastHit.collider.gameObject)
                {
                    objectGrababble.Drop();
                    objectGrababble = null;
                }
            }
        }
    }
    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pressingKey = true;
        }
        else
        {
            pressingKey = false;
            if (objectGrababble != null)
            {
                objectGrababble.Drop();
                objectGrababble = null;
            }
        }
    }

    public void OnGrabScroll(InputAction.CallbackContext context)
    {
        mouseScrollY = context.ReadValue<float>();
        if (objectGrababble != null)
        {
            objectGrababble.ChangeOffset(mouseScrollY);
        }
    }
}