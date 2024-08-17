using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject doorObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            OpenDoor(other);
        }
    }

    public void OpenDoor(Collider key)
    {
        doorObject.SetActive(false);
        key.gameObject.SetActive(false);
    }
}
