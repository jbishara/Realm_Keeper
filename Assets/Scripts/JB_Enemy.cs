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
    private AbilityInfo soulDrainInfo;
    private AbilityInfo deadlyCloudInfo;
    private AbilityInfo coldSteelInfo;

    private bool isInsideDeathMarkAOE;
    private bool isInsideDeadlyCloud;

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

        if (isInsideDeadlyCloud)
        {
            DeadlyPoisonDmg();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SoulDrain")
        {
            // this enemy inside soul drain ability, taking damage every 0.5 seconds
            soulDrainInfo = other.gameObject.GetComponent<JB_SoulDrain>().abilityInfo;
            InvokeRepeating("SoulDrain", 0f, 0.5f);
        }
        else if(other.gameObject.tag == "DeathMark")
        {
            // this enemy inside death mark ability, taking reduced armour
            isInsideDeathMarkAOE = true;
            temp = healthScript.armour - deathMarkArmourPenalty;
        }
        else if(other.gameObject.tag == "DeadlyCloud")
        {
            // applying poison damage inside this poison cloud
            deadlyCloudInfo = other.gameObject.GetComponent<JB_DeadlyCloud>().deadlyCloudInfo;
            isInsideDeadlyCloud = true;

        }
        else if(other.gameObject.tag == "ColdSteel")
        {
            // applying damage inside cold steel ability
            coldSteelInfo = other.gameObject.GetComponent<JB_ColdSteelAoe>().coldSteelInfo;
            InvokeRepeating("ColdSteelDmg", 0f, 0.5f);
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
        else if(other.gameObject.tag == "DeadlyCloud")
        {
            // stop applying poison damage to enemy when exiting cloud
            isInsideDeadlyCloud = false;
        }
        
    }

    

    private void SoulDrain()
    {
        // soul drain ability, apply damage to this enemy
        healthScript.ApplyDamage(soulDrainInfo);

        // leeching
        float healAmount = soulDrainInfo.damage * 0.1f;
        healthScript.ApplyLeech(healAmount);
    }

    private void DeadlyPoisonDmg()
    {
        // deadly cloud ability, applying poison damage to this enemy
        healthScript.ApplyDamage(deadlyCloudInfo);
    }

    private void ColdSteelDmg()
    {
        // cold steel aoe dmg
        healthScript.ApplyDamage(coldSteelInfo);

        // leeching
        float healAmount = coldSteelInfo.damage * 0.2f;
        healthScript.ApplyLeech(healAmount);
    }
}
