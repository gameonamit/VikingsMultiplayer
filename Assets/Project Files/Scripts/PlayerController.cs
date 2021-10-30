using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    //private CharacterController characterController;
    private Camera mainCamera;
    private PlayerInput playerInput;

    private Vector3 inputDirection;
    private Vector3 Direction;

    [Header("Grounded")]
    public bool isGrounded = false;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [Header("Rotation")]
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity = 10f;

    [Header("Movement")]
    public float moveAcceleration = 15f;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float sprintSpeed;
    public float gravity = 9.8f;

    private void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        //characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        CalculateDirection();
    }


    private void FixedUpdate()
    {
        Gravity();
        if (inputDirection.magnitude > 0) 
        { 
            Rotate();
            ApplyMovement();
        }
        else
        {
            
        }
        //Debug.Log(rb.velocity.magnitude);
    }

    private void CalculateDirection()
    {
        inputDirection = new Vector3(playerInput.Horizontal, 0f, playerInput.Vertical);
        Direction = mainCamera.transform.TransformDirection(inputDirection);
        Direction.y = 0f;
        Direction = Direction.normalized;
    }

    private void Gravity()
    {
       
    }

    private void ApplyMovement()
    {
        if (playerInput.sprint == false)
        {
            if (rb.velocity.magnitude < normalSpeed)
                rb.AddForce(Direction * moveAcceleration);
        }
        else
        {
            if (rb.velocity.magnitude < sprintSpeed)
                rb.AddForce(Direction * moveAcceleration);
        }

    }

    private void Rotate()
    {
        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        //Direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        Debug.Log("Rotating");
    }
}
