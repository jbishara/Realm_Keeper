using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A set of properties relating to damage, not the damage to apply itself
/// </summary>
[CreateAssetMenu]
public class DamageInfo : ScriptableObject
{
    private float m_damage = 10f;                           // Amount of damage to apply
    private float m_critChance;                             // Critical Strike Chance
    [SerializeField] protected DamageType m_damageType;     // Type of damage done, normal, fire or poison
    [SerializeField] protected int m_damageDuration;        // Duration for damage over time abilities
    [SerializeField] protected float m_cooldown;            // Cooldown of ability
    [SerializeField] protected float m_stunDuration;                // Stun duration

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
    public static DamageInfo MakeDamageInfo(float damage, DamageType dmgType, int dmgDuration, float stunDuration)
    {
        return MakeDamageInfo(damage, typeof(DamageInfo), dmgType, dmgDuration, stunDuration);
    }

    /// <summary>
    /// Helper for making a simple damage info based on a single damage value.
    /// This version will make a new instance based on given type
    /// </summary>
    /// <returns>Valid damage info instance</returns>
    public static DamageInfo MakeDamageInfo(float damage, System.Type type, DamageType dmgType, int dmgDuration, float stunDuration)
    {
        if (type != typeof(DamageInfo) && !type.IsSubclassOf(typeof(DamageInfo)))
        {
            Debug.LogWarningFormat("Can not make Damage Info instance using type {0}", type.ToString());
            type = typeof(DamageInfo);
        }

        DamageInfo info = CreateInstance(type) as DamageInfo;
        info.m_damage = damage;
        info.m_damageType = dmgType;
        info.m_damageDuration = dmgDuration;
        info.m_stunDuration = stunDuration;


        return info;
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

