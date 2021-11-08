using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip footStepOne;
    [SerializeField] AudioClip footStepTwo;

    public void PlayFootStepOne()
    {
        audioSource.PlayOneShot(footStepOne);
    }

    public void PlayFootStepTwo()
    {
        audioSource.PlayOneShot(footStepTwo);
    }
}
