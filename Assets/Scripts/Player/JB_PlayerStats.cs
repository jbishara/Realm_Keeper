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
    private static float m_critChance;
    private static float m_critDamageBonus;
    public static bool spikedShoulders;
    public static bool medicineFlask;
    public static bool fireGem;

    public static float critChance         { get { return m_critChance; } set { m_critChance = value; } }
    public static float critDamageBonus    { get { return m_critDamageBonus; } set { m_critDamageBonus = value; } }
    
    public float attackDamage   { get { return m_attackDamage; } set { m_attackDamage = value; } }
    public float attackSpeed    { get { return m_attackSpeed; } set { m_attackSpeed = value; } }
    public float health         { get { return m_health; } set { m_health = value; } }
    public float maxHealth      { get { return m_maxHealth; } }
    public float healthRegen    { get { return m_healthRegen; } set { m_attackDamage = value; } }
    public float armour         { get { return m_armour;  } set { m_armour = value; } }
    //public bool spikedShoulders { get { return m_spikedShoulders; } }

    public float moveSpeed
    { get
        { return m_moveSpeed; }
        set
        {
            m_moveSpeed = value;
            UpdatingMovementSpeed();
        }
    }


    private void Start()
    {
        InvokeRepeating("HealthRegen", 1f, 1f);

        playerController = GetComponent<vThirdPersonController>();

        healthScript = GetComponent<HealthComponent>();

        healthScript.OnHealthChanged += UpdateHealth;

        ResetValues();

        healthScript.health = characterStats.health;
        healthScript.maxHealth = characterStats.maxHealth;
        healthScript.armour = characterStats.armour;

        HealthComponent.Leeching += Lifesteal;

    }

    private void HealthRegen()
    {
        if (healthScript)
        {
            //Debug.Log("invoke reached");
            healthScript.RestoreHealth(m_healthRegen);
        }
            
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

        m_health = characterStats.health;
        m_maxHealth = characterStats.maxHealth;


        m_healthRegen = characterStats.healthRegen;
        m_armour = characterStats.armour;
        m_critChance = 0.02f;
        m_moveSpeed = characterStats.moveSpeed;

        // setting movement speed movement speed
        playerController.freeSpeed.runningSpeed = m_moveSpeed;
        playerController.freeSpeed.sprintSpeed = (m_moveSpeed * 1.3f);

        playerController.strafeSpeed.runningSpeed = m_moveSpeed;
        playerController.strafeSpeed.sprintSpeed = (m_moveSpeed * 1.3f);

        GetComponent<JB_PlayerAbilities>().ToggleLeech(false);
        GetComponent<JB_PlayerAbilities>().ToggleBook(false);

        playerController.jumpHeight = 4f;
        

        spikedShoulders = false;
        medicineFlask = false;
        fireGem = false;
        //HealthChange();

    }

    private void UpdatingMovementSpeed()
    {
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

        healthScript.health = m_health;
        healthScript.maxHealth = characterStats.maxHealth;
        healthScript.armour = characterStats.armour;

        Debug.Log(m_health);
        Debug.Log(m_maxHealth);

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

    public void LuckyDice()
    {
        critChance += 0.1f;
    }

    public void Sword()
    {
        attackDamage += 10;
    }

    public void Dagger()
    {
        attackSpeed -= 0.1f;
    }

    public void Influx()
    {
        critDamageBonus += 0.25f;
    }

    public void KnightsHelmet()
    {
        health += 10f;
        armour += 12f;
    }

    public void Apple()
    {
        health += 40f;
        healthRegen += 0.5f;
    }

    public void VampireFangs()
    {
        // calls function that activates the leech boolean for each ability
        GetComponent<JB_PlayerAbilities>().ToggleLeech(true);
    }

    public void RangerHat()
    {
        moveSpeed *= 1.05f;
        critChance += 0.05f;
    }

    public void SpikedShoulderPads()
    {
        // TODO - CREATE BOOLEAN TO ACTIVATE REFLECT DMG
        spikedShoulders = true;
    }

    public void SpringShoes()
    {
        // TODO - INCREASE JUMP HEIGHT
        playerController.jumpHeight *= 1.2f;
    }

    public void Book()
    {
        // TODO - REDUCE CD BY 1 SECOND WHEN CRIT
        GetComponent<JB_PlayerAbilities>().isBookHeld = true;
    }

    public void MedicineFlask()
    {
        // TODO - PLAYER HAS 20% CHANCE OF NOT BEING AFFECTED BY POISON
        medicineFlask = true;
    }

    public void FireGem ()
    {
        // TODO - PLAYER HAS 20% CHANCE OF NOT BEING AFFECTED BY BURN
        fireGem = true;
    }
}
