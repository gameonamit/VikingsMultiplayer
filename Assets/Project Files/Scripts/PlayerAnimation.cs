using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    //CharacterController characterController;
    PlayerController playerController;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        //characterController = GetComponent<CharacterController>();
    }

    private void LateUpdate()
    {
        Vector2 velocity = new Vector2(rb.velocity.x, rb.velocity.z);
        float velocityMagnitude = velocity.magnitude;
        anim.SetFloat("Velocity", velocityMagnitude);

        anim.SetBool("IsGrounded", playerController.isGrounded);
    }
}
