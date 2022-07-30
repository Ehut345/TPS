using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public Slider healthSlider;

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }
    public void GiveFullHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
}
