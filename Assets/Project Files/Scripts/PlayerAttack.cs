using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerAnimation playerAnimation;
    private bool reset = false;

    [SerializeField] float attackDelay = 0.3f;
    [SerializeField] float heavyAttackDelay = 0.5f;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.attack)
        {
            if (reset == false)
            {
                reset = true;
                playerAnimation.TriggerAttackAnimation();
                StartCoroutine(ResetAttact(attackDelay));
            }
        }

        if (playerInput.heavyAttack)
        {
            if (reset == false)
            {
                reset = true;
                playerAnimation.TriggerHeavyAttackAnimation();
                StartCoroutine(ResetAttact(heavyAttackDelay));
            }
        }
    }

    private IEnumerator ResetAttact(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        reset = false;
    }
}
