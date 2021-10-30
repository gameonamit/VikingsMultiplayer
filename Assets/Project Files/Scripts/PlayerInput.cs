using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal;
    public float Vertical;
    public bool sprint;

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Sprint")) sprint = true;
        if (Input.GetButtonUp("Sprint")) sprint = false;

        if (Input.GetButtonDown("Fire1"))
        {
            // Attack
        }
    }
}
