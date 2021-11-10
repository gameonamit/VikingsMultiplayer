using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public void UpdateVolumeSlider(float value)
    {
        audioMixer.SetFloat("cVolume", Mathf.Log10(value) * 20);
    }
}
