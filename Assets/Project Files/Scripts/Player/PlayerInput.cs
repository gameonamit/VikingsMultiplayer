using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInput : MonoBehaviourPun
{
    public float Horizontal;
    public float Vertical;
    public bool sprint;
    public bool attack;
    public bool heavyAttack;
    public bool block;
    public bool dodge;
    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = LevelManager.instance;
    }

    void Update()
    {
        if (photonView.IsMine == true && levelManager.isPaused == false)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");

            if (Input.GetButtonDown("Sprint")) sprint = true;
            if (Input.GetButtonUp("Sprint")) sprint = false;

            if (Input.GetButtonDown("Fire1")) attack = true;
            if (Input.GetButtonUp("Fire1")) attack = false;

            if (Input.GetButtonDown("Fire2")) heavyAttack = true;
            if (Input.GetButtonUp("Fire2")) heavyAttack = false;

            if (Input.GetButtonDown("Block")) block = true;
            if (Input.GetButtonUp("Block")) block = false;

            if (Input.GetButtonDown("Dodge")) dodge = true;
            if (Input.GetButtonUp("Dodge")) dodge = false;
        }
    }
}
