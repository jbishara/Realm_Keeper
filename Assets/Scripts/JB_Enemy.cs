using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JB_Enemy : MonoBehaviour
{
    public Image hpBar;

    private float currentHealth;
    private float maxHealth;
    private HealthComponent healthScript;

    private void Start()
    {
        currentHealth = GetComponent<HealthComponent>().health;
        maxHealth = GetComponent<HealthComponent>().maxHealth;
        healthScript = GetComponent<HealthComponent>();
    }

    private void Update()
    {
        currentHealth = healthScript.health;
        
        hpBar.fillAmount = currentHealth / maxHealth;
    }
}
