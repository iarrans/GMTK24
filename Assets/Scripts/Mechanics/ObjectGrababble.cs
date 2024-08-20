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
    public ParticleSystem grabParticles;
    public bool canBeGrabbed = true;

    private PlayerPickUpDrop playerPickUpDrop;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Grab(Transform objectGrabPointTransform, PlayerPickUpDrop player)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        rb.useGravity = false;
        offset = Vector3.Distance(transform.position, objectGrabPointTransform.position);
        grabParticles.gameObject.SetActive(true);
        AudioManager.instance.MagicSFXSource.Play();
        playerPickUpDrop = player;
    }

    public void Drop()
    {
        objectGrabPointTransform = null;
        rb.useGravity = true;
        grabParticles.gameObject.SetActive(false);
        AudioManager.instance.MagicSFXSource.Stop();
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

        if (offset < 2) offset = 2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerPickUpDrop == null) return;
        Drop();
        playerPickUpDrop.objectGrababble = null;
        canBeGrabbed = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(DelayCanBeGrabbed());
    }

    IEnumerator DelayCanBeGrabbed()
    {
        yield return new WaitForSeconds(1f);
        canBeGrabbed = true;
    }
}
