using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrababble : MonoBehaviour
{
    private Rigidbody rb;
    private Transform objectGrabPointTransform;
    [SerializeField] private float lerpSpeed = 10;
    public float offset = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        rb.useGravity = false;
        offset = Vector3.Distance(transform.position, objectGrabPointTransform.position);
    }

    public void Drop()
    {
        objectGrabPointTransform = null;
        rb.useGravity = true;
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position + objectGrabPointTransform.forward * offset, Time.deltaTime * lerpSpeed);
            rb.MovePosition(newPosition);
        } 
    }

    internal void ChangeOffset(float mouseScrollY)
    {
        if (mouseScrollY < 0)
        {
            offset -= 1;
        } else if (mouseScrollY > 0)
        {
            offset += 1;
        }
    }
}
