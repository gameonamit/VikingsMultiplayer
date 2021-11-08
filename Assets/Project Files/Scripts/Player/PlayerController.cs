using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;
    private PlayerInput playerInput;
    private PlayerAttack playerAttack;

    private Vector3 forceDirection = Vector3.zero;
    private Vector2 inputDirection;

    [Header("Step Offset")]
    [SerializeField] private float stepOffset = 0.25f;
    [SerializeField] private float stepForce = 3f;
    [SerializeField] private float stepYOffSet = 0.5f;
    bool climbed = false;

    [Header("Rotation")]
    [SerializeField] private float turnSmoothTime = 0.1f;

    [Header("Movement")]
    public float moveAcceleration = 15f;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float sprintSpeed;

    public bool isDead = false;

    private void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void FixedUpdate()
    {
        if (isDead == false)
        {
            CalculateDirection();
            ApplyMovement();

            GravityModifier();
            LookAt();

            AutoClimb();
        }
    }

    private void AutoClimb()
    {
        if (inputDirection.sqrMagnitude > 0.1)
        {
            if (climbed == false)
            {
                //For Debugging
                Debug.DrawLine(this.transform.position + new Vector3(0, stepYOffSet, 0),
                this.transform.position + new Vector3(0, stepYOffSet, 0) + transform.forward * stepOffset, Color.red);

                Ray ray = new Ray(this.transform.position + new Vector3(0, stepYOffSet, 0), transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, stepOffset))
                {
                    climbed = true;
                    rb.AddForce(stepForce * transform.up, ForceMode.Impulse);
                    StartCoroutine(ClimbReset());
                }
            }
        }
    }

    IEnumerator ClimbReset()
    {
        yield return new WaitForSeconds(1f);
        climbed = false;
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
        if (playerAttack.attacking == false && playerAttack.blocking == false && playerAttack.dodging == false)
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
        else
        {
            forceDirection = Vector3.zero;
        }
    }

    private void LookAt()
    {
        if (playerAttack.dodging == false)
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

    public void ApplyForce(float dodge, Vector3 direction)
    {
        rb.AddForce(direction * dodge, ForceMode.Impulse);
    }
}
