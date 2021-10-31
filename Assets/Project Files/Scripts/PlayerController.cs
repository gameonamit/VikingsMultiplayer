using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;
    private PlayerInput playerInput;

    private Vector3 forceDirection = Vector3.zero;
    private Vector2 inputDirection;

    [Header("Grounded")]
    [SerializeField] private float groundDistance = 0.25f;

    [Header("Rotation")]
    [SerializeField] private float turnSmoothTime = 0.1f;

    [Header("Movement")]
    public float moveAcceleration = 15f;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float sprintSpeed;

    private void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        CalculateDirection();
        ApplyMovement();
        GravityModifier();
        LookAt();
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * groundDistance, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
            return true;
        else
            return false;
    }

    private Vector3 GetCamerRight(Camera mainCamera)
    {
        Vector3 right = mainCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private Vector3 GetCamerForward(Camera mainCamera)
    {
        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private void CalculateDirection()
    {
        forceDirection += playerInput.Vertical * GetCamerForward(mainCamera) * moveAcceleration;
        forceDirection += playerInput.Horizontal * GetCamerRight(mainCamera) * moveAcceleration;

        inputDirection = new Vector2(playerInput.Horizontal, playerInput.Vertical);
    }

    private void GravityModifier()
    {
        if (rb.velocity.y < 0f)
            rb.velocity -= Vector3.down * Physics.gravity.y * 2 * Time.fixedDeltaTime;
    }

    private void ApplyMovement()
    {
        if (inputDirection.sqrMagnitude > 0.1f)
        {
            rb.AddForce(forceDirection, ForceMode.Impulse);
            forceDirection = Vector3.zero;

            Vector3 horizontalVelocity = rb.velocity;
            horizontalVelocity.y = 0;
            if (playerInput.sprint == false)
            {
                if (horizontalVelocity.sqrMagnitude > normalSpeed * normalSpeed)
                {
                    rb.velocity = horizontalVelocity.normalized * normalSpeed + Vector3.up * rb.velocity.y;
                }
            }
            else
            {
                if (horizontalVelocity.sqrMagnitude > sprintSpeed * sprintSpeed)
                {
                    rb.velocity = horizontalVelocity.normalized * sprintSpeed + Vector3.up * rb.velocity.y;
                }
            }
        }
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (inputDirection.sqrMagnitude > 0.1 && direction.sqrMagnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            //this.rb.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSmoothTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSmoothTime);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
}
