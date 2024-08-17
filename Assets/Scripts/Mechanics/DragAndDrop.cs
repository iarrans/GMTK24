using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 mousePosition;
    Rigidbody rb;
    [SerializeField] float mouseObjectDistance = 0;
    public float offset = 0;
    public LayerMask layers;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
        mouseObjectDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        offset = mouseObjectDistance;
    }

    private void OnMouseDrag()
    {
        Ray ray = new Ray(Camera.main.transform.position, transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layers))
        {
            Debug.Log(hit.collider.name);
        }
        else {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + transform.forward * offset);
        }   
    }
}
