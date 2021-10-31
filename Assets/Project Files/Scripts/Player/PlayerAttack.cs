using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerAnimation playerAnimation;
    private PlayerController playerController;

    public bool attacking = false;
    public bool blocking = false;
    public bool dodging = false;

    [SerializeField] float dodgeForce = 5f;

    [SerializeField] float attackDelay = 0.3f;
    [SerializeField] float heavyAttackDelay = 0.5f;
    [SerializeField] float blockingDelay = 0.5f;
    [SerializeField] float dodgingDelay = 0.5f;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerInput.attack)
        {
            if (attacking == false)
            {
                attacking = true;
                playerAnimation.TriggerAttackAnimation();
                StartCoroutine(ResetAttact(attackDelay));
            }
        }

        if (playerInput.heavyAttack)
        {
            if (attacking == false)
            {
                attacking = true;
                playerAnimation.TriggerHeavyAttackAnimation();
                StartCoroutine(ResetAttact(heavyAttackDelay));
            }
        }

        if (playerInput.block)
        {
            if (blocking == false)
            {
                blocking = true;
                playerAnimation.TriggerBlockAnimation();
                StartCoroutine(ResetBlock(blockingDelay));
            }
        }

        if (playerInput.dodge)
        {
            if (dodging == false)
            {
                dodging = true;
                playerAnimation.TriggerDodgeAnimation();
                playerController.ApplyDodgeForce(dodgeForce);
                StartCoroutine(ResetDodge(dodgingDelay));
            }
        }
    }

    private IEnumerator ResetAttact(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        attacking = false;
    }

    private IEnumerator ResetBlock(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        blocking = false;
    }

    private IEnumerator ResetDodge(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        dodging = false;
    }
}
