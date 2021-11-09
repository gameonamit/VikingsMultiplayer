using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerAnimation playerAnimation;
    private PlayerController playerController;
    private PlayerStamina playerStamina;
    private PlayerHealth playerHealth;
    private PlayerSoundEffects playerSoundEffects;
    private PlayerRound playerRound;

    [SerializeField] AttackBox attackBox;

    public bool attacking = false;
    public bool blocking = false;
    public bool dodging = false;

    Coroutine resetAttack;
    Coroutine resetBlock;
    Coroutine resetDouge;

    [Header("Attack Damage")]
    [SerializeField] int attackDamage = 5;
    [SerializeField] int heavyAttackDamage = 8;

    [Header("Attack Force")]
    [SerializeField] float attackForce = 2f;
    [SerializeField] float heavyAttackForce = 3f;
    [SerializeField] float dodgeForce = 5f;

    [Header("Attack Delay")]
    [SerializeField] float attackDelay = 0.3f;
    [SerializeField] float heavyAttackDelay = 0.5f;
    [SerializeField] float blockingDelay = 0.5f;
    [SerializeField] float dodgingDelay = 0.5f;

    [Header("Stamina Cost")]
    [SerializeField] int attackStaminaCost = 10;
    [SerializeField] int heavyAttackStaminaCost = 20;
    [SerializeField] int blockingStaminaCost = 20;
    [SerializeField] int dodgingStaminaCost = 20;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerController = GetComponent<PlayerController>();
        playerStamina = GetComponent<PlayerStamina>();
        playerHealth = GetComponent<PlayerHealth>(); 
        playerSoundEffects = GetComponent<PlayerSoundEffects>();
        playerRound = GetComponent<PlayerRound>();
    }

    void Update()
    {
        if (playerInput.attack)
        {
            if (playerStamina.HasEnoughStamina(attackStaminaCost))
            {
                if (attacking == false)
                {
                    playerStamina.DecreaseStamina(attackStaminaCost);
                    attacking = true;
                    playerAnimation.TriggerAttackAnimation();
                    playerController.ApplyForce(attackForce, transform.forward);
                    if (resetAttack != null) StopCoroutine(resetAttack);
                    resetAttack = StartCoroutine(ResetAttact(attackDelay));
                }
            }
        }

        if (playerInput.heavyAttack)
        {
            if (playerStamina.HasEnoughStamina(heavyAttackStaminaCost))
            {
                if (attacking == false)
                {
                    attacking = true;
                    playerStamina.DecreaseStamina(heavyAttackStaminaCost);
                    playerAnimation.TriggerHeavyAttackAnimation();
                    playerController.ApplyForce(heavyAttackForce, transform.forward);
                    if (resetAttack != null) StopCoroutine(resetAttack);
                    resetAttack = StartCoroutine(ResetAttact(heavyAttackDelay));
                }
            }
        }

        if (playerInput.block)
        {
            if (playerStamina.HasEnoughStamina(blockingStaminaCost))
            {
                if (blocking == false)
                {
                    blocking = true;
                    playerStamina.DecreaseStamina(blockingStaminaCost);
                    playerAnimation.TriggerBlockAnimation();
                    if (resetBlock != null) StopCoroutine(resetBlock);
                    resetBlock = StartCoroutine(ResetBlock(blockingDelay));
                }
            }
        }

        if (playerInput.dodge)
        {
            if (playerStamina.HasEnoughStamina(dodgingStaminaCost))
            {
                if (dodging == false)
                {
                    dodging = true;
                    playerStamina.DecreaseStamina(dodgingStaminaCost);
                    playerAnimation.TriggerDodgeAnimation();
                    playerController.ApplyForce(dodgeForce, -transform.forward);
                    if (resetDouge != null) StopCoroutine(resetDouge);
                    resetDouge = StartCoroutine(ResetDodge(dodgingDelay));
                }
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

    #region Attack & Hit
    public void Attack()
    {
        if (attackBox.inRange == true)
        {
            if (attackBox.otherPlayer.playerController.isDead == false)
            {
                attackBox.otherPlayer.Hit(attackDamage);
            }
        }
    }

    public void HeavyAttack()
    {
        if (attackBox.inRange == true)
        {
            attackBox.otherPlayer.Hit(heavyAttackDamage);
        }
    }

    public void Hit(int damage)
    {
        if(blocking == false)
        {
            playerHealth.DecreaseHealth(damage);
            //Hit Animation
            playerAnimation.TriggerHitAnimation();
            if (playerHealth.CurrentHealth <= 0)
            {
                playerAnimation.TriggerDeathAnimation();
                playerController.Kill();
            }
        }
        else
        {
            playerSoundEffects.PlayBlockSFX();
        }
    }
    #endregion
}
