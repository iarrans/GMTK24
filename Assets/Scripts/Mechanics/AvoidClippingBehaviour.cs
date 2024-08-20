using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidClippingBehaviour : MonoBehaviour
{
    public ObjectGrababble objectGrababble;

    private void OnTriggerEnter(Collider other)
    {
        if(objectGrababble != null) objectGrababble.OnClip();
    }
}
