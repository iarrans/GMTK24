using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject cameraHolder;
    public float speed, senstivity, jumpForce, groundDistance;
    private Vector2 move, look;
    private float lookRotation;
    public float maxForce;
    public float limitX, limitY;
    //Indica si pj está en el suelo
    public bool grounded;
    public LayerMask groundMask;

    public static PlayerController instance;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move() {

        //Target Velocity
        Vector3 currentVelocity = rb.velocity;

        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
        targetVelocity *= speed;

        //Dieccion
        targetVelocity = transform.TransformDirection(targetVelocity);

        //Calculate Forces
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x,0, velocityChange.z);

        //Limit force
        Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    void Look()
    {
        //Turn (LooK x = Mouse X)
        transform.Rotate(Vector3.up * look.x * senstivity);

        //Look
        lookRotation += (-look.y * senstivity);
        lookRotation = Mathf.Clamp(lookRotation, -limitX, limitY);
        cameraHolder.transform.eulerAngles = new Vector3(lookRotation, cameraHolder.transform.eulerAngles.y, cameraHolder.transform.eulerAngles.z);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Vector3 jumpForces = Vector3.zero;
        if (grounded)
        {
            jumpForces = Vector3.up * jumpForce;
        }

        rb.AddForce(jumpForces, ForceMode.VelocityChange);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        instance = this;
    }

    void Update()
    {
        // Verificar si el jugador está en el suelo
        grounded = Physics.Raycast(transform.GetChild(0).position, Vector3.down, 0.75f, groundMask);
    }

    void LateUpdate()
    {
        Look();
    }

}
