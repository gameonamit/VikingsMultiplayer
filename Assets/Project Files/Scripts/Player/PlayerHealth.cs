using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int CurrentHealth = 100;
    public Slider healthSlider;

    public void IncreaseHealth(int value)
    {
        CurrentHealth += value;
        if (CurrentHealth >= 100)
        {
            CurrentHealth = 100;
        }
        UpdateUI();
    }

    public void DecreaseHealth(int value)
    {
        CurrentHealth -= value;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthSlider.value = CurrentHealth;
    }
}
