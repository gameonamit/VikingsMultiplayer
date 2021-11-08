using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] attackSounds;
    [SerializeField] AudioClip[] heavyAttackSounds;
    [SerializeField] AudioClip blockSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAttackSFX()
    {
        int ran = Random.Range(0, attackSounds.Length);
        audioSource.PlayOneShot(attackSounds[ran]);
    }

    public void PlayHeavyAttackSFX()
    {
        int ran = Random.Range(0, heavyAttackSounds.Length);
        audioSource.PlayOneShot(heavyAttackSounds[ran]);
    }

    public void PlayBlockSFX()
    {
        audioSource.PlayOneShot(blockSound);
    }
}
