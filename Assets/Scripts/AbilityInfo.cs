using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.XR.WSA.Persistence;


/// <summary>
/// A set of properties relating to abilities - damage or utility, not the damage to apply itself
/// </summary>
[CreateAssetMenu]
public class AbilityInfo : ScriptableObject
{
    private string m_abilityName;                               // Name of ability
    private float m_critChance;                                 // Critical Strike Chance
    private bool m_isLeeching;                                  // Can this ability leech
    private bool m_isBookHeld;                                  // Is this player holding a book (used to reduced remaining cds on abilities)
    [SerializeField] private float m_damage;                    // Amount of damage to apply
    [SerializeField] private float m_damageMultiplier = 1f;     // Multiplier to damage based off ability
    [SerializeField] private DamageType m_damageType;           // Type of damage done, normal, fire or poison
    [SerializeField] private int m_damageDuration;              // Duration for damage over time abilities
    [SerializeField] private float m_cooldown;                  // Cooldown of ability
    [SerializeField] private float m_stunDuration;              // Stun duration
    [SerializeField] private AbilityType m_abilityType;         // Type of this ability
    [SerializeField] private CharacterClass m_characterClass;   // Character that can use this ability
    [SerializeField] private float m_castTime;                  // Cast Time of the ability
    [SerializeField] private float m_abilityRange;              // Ability Range
    [SerializeField] private bool m_needChallenge;              // Determines whether or not to activate this ability first or requires challenge
    [SerializeField] private bool m_isActive;                   // Is this ability active - usable

    /// <summary>
    /// Is this ability active
    /// </summary>
    public bool isActive { get { return m_isActive; } set { m_isActive = value; } }

    /// <summary>
    /// Can this ability leech
    /// </summary>
    public bool isLeeching { get { return m_isLeeching; } set { m_isLeeching = value; } }

    /// <summary>
    /// Can this ability leech
    /// </summary>
    public bool isBookHeld { get { return m_isBookHeld; } set { m_isBookHeld = value; } }

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
    public DamageType damageType { get { return m_damageType; } set { m_damageType = value; } }

    /// <summary>
    /// Duration of the damage to be applied
    /// </summary>
    public int dmgDuration { get { return m_damageDuration; } }

    /// <summary>
    /// Cooldown of ability
    /// </summary>
    public float cooldown { get { return m_cooldown; } set { m_cooldown = value; } }

    /// <summary>
    /// Stun duration
    /// </summary>
    public float stunDuration { get { return m_stunDuration; } }

    /// <summary>
    /// Range of the ability
    /// </summary>
    public float abilityRange { get { return m_abilityRange; } }

    /// <summary>
    /// Cast Time
    /// </summary>
    public float castTime { get { return m_castTime; } }


    /// <summary>
    /// Helper for making a simple ability info based on a single damage value
    /// </summary>
    /// <returns>Valid damage info instance</returns>
    public static AbilityInfo MakeDamageInfo(float damage, DamageType dmgType, int dmgDuration, float stunDuration, bool isLeeching)
    {
        return MakeDamageInfo(damage, typeof(AbilityInfo), dmgType, dmgDuration, stunDuration, isLeeching);
    }

    /// <summary>
    /// Helper for making a simple ability info based on a single damage value.
    /// This version will make a new instance based on given type
    /// </summary>
    /// <returns>Valid damage info instance</returns>
    public static AbilityInfo MakeDamageInfo(float damage, System.Type type, DamageType dmgType, int dmgDuration, float stunDuration, bool isLeeching)
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
        info.m_isLeeching = isLeeching;


        return info;
    }



    private void OnEnable()
    {
        // assigning this variable to name of this object
        m_abilityName = this.name;


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

