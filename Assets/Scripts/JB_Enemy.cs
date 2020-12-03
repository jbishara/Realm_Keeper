using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JB_Enemy : MonoBehaviour
{
    public Image hpBar;
    public float deathMarkArmourPenalty;

    private float temp;

    private float currentHealth;
    private float maxHealth;

    private HealthComponent healthScript;
    private AbilityInfo abilityInfo;

    private bool isInsideDeathMarkAOE;

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

        // boolean controlled by trigger to adjust this enemy's armour while inside deathmark ability
        if (isInsideDeathMarkAOE)
        {
            healthScript.armour = temp;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SoulDrain")
        {

            abilityInfo = other.gameObject.GetComponent<JB_SoulDrain>().abilityInfo;

            if (healthScript)
            {

                // this enemy inside soul drain ability, taking damage every 0.5 seconds
                InvokeRepeating("SoulDrain", 0f, 0.5f);

            }
        }
        else if(other.gameObject.tag == "DeathMark")
        {
            // this enemy inside death mark ability, taking reduced armour
            isInsideDeathMarkAOE = true;
            temp = healthScript.armour - deathMarkArmourPenalty;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "DeathMark")
        {
         
            // restore this enemy's armour when exiting deathmark ability
            isInsideDeathMarkAOE = false;
            healthScript.armour += deathMarkArmourPenalty;

        }
    }

    

    private void SoulDrain()
    {
        // soul drain ability, apply damage to this enemy
        healthScript.ApplyDamage(abilityInfo);
    }


}
