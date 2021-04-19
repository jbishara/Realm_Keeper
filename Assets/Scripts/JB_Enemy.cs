using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JB_Enemy : MonoBehaviour
{
    // Event that alerts the player stats script if damage applied has leech
    public delegate void DeactivateShieldProtection();
    public static event DeactivateShieldProtection DeactivateShield;

    public Image hpBar;
    public GUI hpBarParentRef;
    public float deathMarkArmourPenalty;

    private float temp;

    private float currentHealth;
    private float maxHealth;

    private HealthComponent healthScript;
    private AbilityInfo soulDrainInfo;
    private AbilityInfo deadlyCloudInfo;
    private AbilityInfo coldSteelInfo;
    private AbilityInfo smokeTossInfo;
    private AbilityInfo pestilenceInfo;

    private bool isInsideDeathMarkAOE;
    private bool isInsideDeadlyCloud;


    private void Start()
    {
        currentHealth = GetComponent<HealthComponent>().health;
        maxHealth = GetComponent<HealthComponent>().maxHealth;
        healthScript = GetComponent<HealthComponent>();
<<<<<<< Updated upstream
        if (gameObject.name.Contains("BOSS"))
        {
            hpBar = GameObject.FindGameObjectWithTag("BossHP").GetComponent<Image>();
        }
=======
        if (this.gameObject.name.Contains("BOSS"))
        {
            hpBar = GameObject.FindGameObjectWithTag("BossHP").GetComponent<Image>();
        }
        
>>>>>>> Stashed changes
    }

    private void Update()
    {
        currentHealth = healthScript.health;

        if (hpBar != null)
        {
            hpBar.fillAmount = currentHealth / maxHealth;
        }

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
            StartCoroutine(ColdSteelCoroutine(coldSteelInfo));
        }
        else if(other.gameObject.tag == "ShieldProtection")
        {
            // sends an event signal to deactivate shield protection on player
            DeactivateShield();
        }
        else if(other.gameObject.tag == "SmokeToss")
        {
            // take dmg from hitting smoke cloud
            // slow this enemy - TODO
            smokeTossInfo = other.gameObject.GetComponent<JB_SmokeToss>().smokeTossInfo;
            ApplyDamage(smokeTossInfo);
        }
        else if(other.gameObject.tag == "PestilenceTouch")
        {
            pestilenceInfo = other.gameObject.GetComponent<JB_PestilenceTouch>().pestilenceInfo;
            ApplyDamage(pestilenceInfo);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("reaching trigger exit");
        if (other.gameObject.tag == "DeathMark")
        {
            // restore this enemy's armour when exiting deathmark ability
            isInsideDeathMarkAOE = false;
            healthScript.armour += deathMarkArmourPenalty;

        }

        if (other.gameObject.tag == "DeadlyCloud")
        {
            // stop applying poison damage to enemy when exiting cloud
            isInsideDeadlyCloud = false;
        }

        if (other.gameObject.tag == "ColdSteel")
        {
            Debug.Log("reaching trigger exit");
            CancelInvoke("ColdSteelDmg");
        }
        
    }

    

    private void SoulDrain()
    {
        // soul drain ability, apply damage to this enemy
        healthScript.ApplyDamage(soulDrainInfo);

        // leeching - calls an event that sends health to the player
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

        // leeching - calls an event that sends health to the player
        float healAmount = coldSteelInfo.damage * 0.2f;
        healthScript.ApplyLeech(healAmount);
    }

    private void ApplyDamage(AbilityInfo ability)
    {
        healthScript.ApplyDamage(ability);
    }

    IEnumerator ColdSteelCoroutine(AbilityInfo coldSteel)
    {
        yield return new WaitForSeconds(coldSteel.dmgDuration);
        CancelInvoke("ColdSteelDmg");
    }
}
