﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A set of properties relating to abilities - damage or utility, not the damage to apply itself
/// </summary>
[CreateAssetMenu]
public class AbilityInfo : ScriptableObject
{
    private string m_abilityName;                               // Name of ability
    private float m_damage = 10f;                               // Amount of damage to apply
    private float m_critChance;                                 // Critical Strike Chance
    [SerializeField] protected float m_damageMultiplier = 1f;   // Multiplier to damage based off ability
    [SerializeField] protected DamageType m_damageType;         // Type of damage done, normal, fire or poison
    [SerializeField] protected int m_damageDuration;            // Duration for damage over time abilities
    [SerializeField] protected float m_cooldown;                // Cooldown of ability
    [SerializeField] protected float m_stunDuration;            // Stun duration
    [SerializeField] protected AbilityType m_abilityType;       // Type of this ability
    [SerializeField] protected CharacterClass m_characterClass; // Character that can use this ability
    [SerializeField] protected bool m_needChallenge;            // Determines whether or not to activate this ability first or requires challenge

    /// <summary>
    /// Damage to mulitply base attack daamage for each character
    /// </summary>
    public float damageMultiplier { get { return m_damageMultiplier; } }

    /// <summary>
    /// A boolean to determine whether or not this ability needs a challenge to acquire
    /// </summary>
    public bool needChallenge { get { return m_needChallenge; } }

    /// <summary>
    /// Character that can use this ability
    /// </summary>
    public CharacterClass characterClass { get { return m_characterClass; } }

    /// <summary>
    /// Type of ability
    /// </summary>
    public AbilityType abilityType { get { return m_abilityType; } }

    /// <summary>
    /// Name of ability
    /// </summary>
    public string abilityName { get { return m_abilityName; } }

    /// <summary>
    /// Amount of damage to apply
    /// </summary>
    public float damage { get { return m_damage; } set { m_damage = value; } }

    /// <summary>
    /// Critical Strike Chance
    /// </summary>
    public float critChance { get { return m_critChance; } set { m_critChance = value; } }

    /// <summary>
    /// Type of damage to apply
    /// </summary>
    public DamageType damageType { get { return m_damageType; } }

    /// <summary>
    /// Duration of the damage to be applied
    /// </summary>
    public int dmgDuration { get { return m_damageDuration; } }

    /// <summary>
    /// Cooldown of ability
    /// </summary>
    public float cooldown { get { return m_cooldown; } }

    /// <summary>
    /// Stun duration
    /// </summary>
    public float stunDuration { get { return m_stunDuration; } }

    /// <summary>
    /// Helper for making a simple damage info based on a single damage value
    /// </summary>
    /// <returns>Valid damage info instance</returns>
    public static AbilityInfo MakeDamageInfo(float damage, DamageType dmgType, int dmgDuration, float stunDuration)
    {
        return MakeDamageInfo(damage, typeof(AbilityInfo), dmgType, dmgDuration, stunDuration);
    }

    /// <summary>
    /// Helper for making a simple damage info based on a single damage value.
    /// This version will make a new instance based on given type
    /// </summary>
    /// <returns>Valid damage info instance</returns>
    public static AbilityInfo MakeDamageInfo(float damage, System.Type type, DamageType dmgType, int dmgDuration, float stunDuration)
    {
        if (type != typeof(AbilityInfo) && !type.IsSubclassOf(typeof(AbilityInfo)))
        {
            Debug.LogWarningFormat("Can not make Damage Info instance using type {0}", type.ToString());
            type = typeof(AbilityInfo);
        }

        AbilityInfo info = CreateInstance(type) as AbilityInfo;
        info.m_damage = damage;
        info.m_damageType = dmgType;
        info.m_damageDuration = dmgDuration;
        info.m_stunDuration = stunDuration;


        return info;
    }

    public void MultiplyDamage()
    {
        damage *= damageMultiplier;
    }

    private void OnEnable()
    {
        // assigning this variable to name of this object
        m_abilityName = this.name;

        MultiplyDamage();
    }
}

/// <summary>
/// Small class containing details about why damage occured.
/// Can be inherited from to provide additional details
/// </summary>
public class DamageEvent
{
    public DamageEvent()
    {

    }

    public DamageEvent(GameObject instigator, Vector2 hitLocation)
    {
        m_instigator = instigator;
        m_hitLocation = hitLocation;
    }

    public GameObject m_instigator = null;          // Instigator that caused the damaged
    public Vector2 m_hitLocation = Vector2.zero;    // Where the damaged occured
}

