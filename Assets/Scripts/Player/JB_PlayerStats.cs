using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

// script used to adjust player a player's stats during runtime
public class JB_PlayerStats : MonoBehaviour
{

    [SerializeField] private CharacterStats characterStats;

    private HealthComponent healthScript;

    private vThirdPersonController playerController;

    private float m_attackDamage;
    private float m_attackSpeed;
    private float m_moveSpeed;
    private float m_health;
    private float m_maxHealth;
    private float m_healthRegen;
    private float m_armour;
    private float m_critChance;
    private float m_critDamageBonus;

    public float critChance         { get { return m_critChance; } }
    public float critDamageBonus    { get { return m_critDamageBonus; } }
    
    public float attackDamage   { get { return m_attackDamage; } set { m_attackDamage = value; } }
    public float attackSpeed    { get { return m_attackSpeed; } set { m_attackSpeed = value; } }
    public float moveSpeed      { get { return m_moveSpeed; } set { m_moveSpeed = value; } }
    public float health         { get { return m_health; } set { m_health = value; } }
    public float maxHealth      { get { return m_maxHealth; } }
    public float healthRegen    { get { return m_healthRegen; } set { m_attackDamage = value; } }
    public float armour         { get { return m_armour;  } set { m_armour = value; } }

    

    private void Start()
    {
        Invoke("HealthRegen", 1f);

        playerController = GetComponent<vThirdPersonController>();

        healthScript = GetComponent<HealthComponent>();

        healthScript.OnHealthChanged += UpdateHealth;

        ResetValues();

        healthScript.health = characterStats.health;
        healthScript.maxHealth = characterStats.maxHealth;
        healthScript.armour = characterStats.armour;

        HealthComponent.leeching += Lifesteal;

    }

    private void HealthRegen()
    {
        if (healthScript)
            healthScript.RestoreHealth(m_healthRegen);
    }

    private void UpdateHealth(HealthComponent healthScript, float newHealth, float delta)
    {
        m_health = healthScript.health;
        m_maxHealth = healthScript.maxHealth;
    }
    

    // initialising and resetting values - used for when player loses all items
    public void ResetValues()
    {
        m_attackDamage = characterStats.attackDamage;
        m_attackSpeed = characterStats.attackSpeed;
        

        m_healthRegen = characterStats.healthRegen;
        m_armour = characterStats.armour;
        m_critChance = 0.02f;
        m_moveSpeed = characterStats.moveSpeed;

        // setting movement speed movement speed
        playerController.freeSpeed.runningSpeed = m_moveSpeed;
        playerController.freeSpeed.sprintSpeed = (m_moveSpeed * 1.3f);

        playerController.strafeSpeed.runningSpeed = m_moveSpeed;
        playerController.strafeSpeed.sprintSpeed = (m_moveSpeed * 1.3f);

    }

    private void Lifesteal(float leechAmount)
    {
        Debug.Log("leeching!! :)");
        // heals player based off leech amount
        healthScript.RestoreHealth(leechAmount);
    }

    private void HealthChange()
    {
        // finding percentage of current hp to adjust new hp accordingly
        float currentHP = healthScript.health;
        float maxHp = healthScript.maxHealth;
        
        m_maxHealth = characterStats.maxHealth;

        // calculation for obtaining new health
        float newHealth = (currentHP / maxHp) * m_maxHealth;

        m_health = newHealth;

    }

    /// <summary>
    /// Temporarily increase attack speed
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public IEnumerator AttackSpeedBuff(float amount, float duration)
    {
        float temp = m_attackSpeed;
        float percentDifference = 1 - amount;

        m_attackSpeed *= percentDifference;

        yield return new WaitForSeconds(duration);

        m_attackSpeed = temp;
    }
}
