using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStats : ScriptableObject
{
    [SerializeField] protected float m_attackDamage;
    [SerializeField] protected float m_attackSpeed;
    [SerializeField] protected float m_moveSpeed;
    [SerializeField] protected float m_health;
    [SerializeField] protected float m_healthRegen;
    [SerializeField] protected float m_armour;


    /// <summary>
    /// Attack Damage - baseline
    /// </summary>
    public float attackDamage { get { return m_attackDamage; } }

    /// <summary>
    /// Attack Speed - baseline
    /// </summary>
    public float attackSpeed { get { return m_attackSpeed; } }

    /// <summary>
    /// Movement Speed
    /// </summary>
    public float moveSpeed { get { return m_moveSpeed; } }

    /// <summary>
    /// Character's Health
    /// </summary>
    public float health { get { return m_health; } }

    /// <summary>
    /// Character's Health Regen
    /// </summary>
    public float healthRegen { get { return m_healthRegen; } }

    /// <summary>
    /// Character's Armour
    /// </summary>
    public float armour { get { return m_armour; } }

    
}
