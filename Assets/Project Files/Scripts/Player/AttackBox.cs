using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public bool inRange = false;
    public PlayerAttack otherPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
            otherPlayer = other.GetComponent<PlayerAttack>();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            otherPlayer = null;
        }
    }
}
