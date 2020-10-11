using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A set of properties relating to damage, not the damage to apply itself
/// </summary>
[CreateAssetMenu]
public class DamageInfo : ScriptableObject
{
    [SerializeField] protected float m_damage = 10f;        // Amount of damage to apply
    [SerializeField] protected float m_stunTime = 0f;       // If greater than zero, how long hurt object should be stunned for

    /// <summary>
    /// Amount of damage to apply
    /// </summary>
    public float damage { get { return m_damage; } }

    /// <summary>
    /// Amount of time hit object should be stunned for (if applicable)
    /// </summary>
    public float stunTime { get { return m_stunTime; } }

    /// <summary>
    /// Helper for making a simple damage info based on a single damage value
    /// </summary>
    /// <returns>Valid damage info instance</returns>
    public static DamageInfo MakeDamageInfo(float damage)
    {
        return MakeDamageInfo(damage, typeof(DamageInfo));
    }

    /// <summary>
    /// Helper for making a simple damage info based on a single damage value.
    /// This version will make a new instance based on given type
    /// </summary>
    /// <returns>Valid damage info instance</returns>
    public static DamageInfo MakeDamageInfo(float damage, System.Type type)
    {
        if (type != typeof(DamageInfo) && !type.IsSubclassOf(typeof(DamageInfo)))
        {
            Debug.LogWarningFormat("Can not make Damage Info instance using type {0}", type.ToString());
            type = typeof(DamageInfo);
        }

        DamageInfo info = CreateInstance(type) as DamageInfo;
        info.m_damage = damage;

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

