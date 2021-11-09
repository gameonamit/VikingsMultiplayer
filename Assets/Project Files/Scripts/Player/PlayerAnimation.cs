using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if(rb.velocity.sqrMagnitude >= 0)
            anim.SetFloat("Velocity", rb.velocity.sqrMagnitude);
    }

    public void TriggerAttackAnimation()
    {
        int ran = Random.Range(1, 5);
        if(ran == 1)
             anim.SetTrigger("Attack1");
        else if(ran == 2)
            anim.SetTrigger("Attack2");
        else if (ran == 3)
            anim.SetTrigger("Attack3");
        else if (ran == 4)
            anim.SetTrigger("Attack4");
    }

    public void TriggerHeavyAttackAnimation()
    {
        anim.SetTrigger("HeavyAttack");
    }

    public void TriggerBlockAnimation()
    {
        anim.SetTrigger("Block");
    }

    public void TriggerDodgeAnimation()
    {
        anim.SetTrigger("Dodge");
    }

    public void TriggerHitAnimation()
    {
        anim.SetTrigger("Hit");
    }

    public void TriggerDeathAnimation()
    {
        anim.SetBool("Death", true);
    }

    public void DisableDeathAnimation()
    {
        anim.SetBool("Death", false);
    }
}
