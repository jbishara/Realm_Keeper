using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script used to adjust player a player's stats during runtime
public class JB_PlayerStats : MonoBehaviour
{
    [SerializeField] private CharacterStats characterStats;

    private float m_attackDamage;
    private float m_attackSpeed;
    private float m_moveSpeed;
    private float m_health;
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
    public float healthRegen    { get { return m_healthRegen; } set { m_attackDamage = value; } }
    public float armour         { get { return m_armour;  } set { m_armour = value; } }

    private void Start()
    {
        Invoke("HealthRegen", 1f);


        ResetValues();
    }

    private void HealthRegen()
    {
        if (GetComponent<HealthComponent>())
            GetComponent<HealthComponent>().RestoreHealth(m_healthRegen);
    }

    //private void

    // initialising and resetting values - used for when player loses all items
    public void ResetValues()
    {
        m_attackDamage = characterStats.attackDamage;
        m_attackSpeed = characterStats.attackSpeed;
        m_moveSpeed = characterStats.moveSpeed;
        m_health = characterStats.health;
        m_healthRegen = characterStats.healthRegen;
        m_armour = characterStats.armour;
        m_critChance = 0.02f;




    }
}
