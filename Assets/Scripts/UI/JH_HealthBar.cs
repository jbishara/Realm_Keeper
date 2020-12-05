using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JH_HealthBar : MonoBehaviour
{
    public Slider healthBarSlider;              // Connects to HealthBar
    public Gradient healthBarGradient;
    public Image healthBarFill;                 // Connects to HealthBarFill

    // NOTE: Will need to refernce this in JB_HealthComponent Script. 
    // This hasn't been done due to not full understanding how the scrip works or where the correct places to reference this is.

    public void SetMaxHealth(float health)
    {
        healthBarSlider.maxValue = health;
        healthBarSlider.value = health;

        healthBarFill.color = healthBarGradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        healthBarSlider.value = health;

        healthBarFill.color = healthBarGradient.Evaluate(healthBarSlider.normalizedValue);
    }
}
