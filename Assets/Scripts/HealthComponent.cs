using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A health component. Can be used for more than just health (e.g. armor)
/// </summary>
public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float m_health = 100f;         // Health of object
    [SerializeField] private float m_maxHealth = 100f;      // Max health of object
    [SerializeField] private float m_armour = 0f;           // Armour of object
    [SerializeField] private bool m_invincible = false;     // If object cannot be damaged (invincible)

    // float used multiply damage based on negative armour
    private float damageMultiplier = 1f;

    // Event that is called when ever this object loses/restores health
    public delegate void OnHealthChangedEvent(HealthComponent self, float newHealth, float delta);
    public OnHealthChangedEvent OnHealthChanged;

    // Event that is called when damaged. This gets called after OnHealthChanged but before OnDeath (if damaged killed)
    public delegate void OnDamagedEvent(HealthComponent self, float damage, DamageInfo info, DamageEvent args);
    public OnDamagedEvent OnDamaged;

    // Event that is called upon death of the object. This will get called after OnHealthChanged and OnDamaged
    public delegate void OnDeathEvent(HealthComponent self);
    public OnDeathEvent OnDeath;

    /// <summary>
    /// This objects current health
    /// </summary>
    public float health { get { return m_health; } }

    /// <summary>
    /// This objects max health
    /// </summary>
    public float maxHealth { get { return m_maxHealth; } }

    /// <summary>
    /// This objects armour
    /// </summary>
    public float armour { get { return m_armour; } set { m_armour = value; } }

    /// <summary>
    /// If this object is dead (has no health)
    /// </summary>
    public bool isDead { get { return m_health <= 0f; } }

    /// <summary>
    /// If this object is invincible
    /// </summary>
    public bool isInvincible { get { return m_invincible; } set { m_invincible = value; } }

    void Start()
    {
        if (GetComponent<JB_PlayerStats>())
        {
            m_health = GetComponent<JB_PlayerStats>().health;
            m_maxHealth = GetComponent<JB_PlayerStats>().health;
            m_armour = GetComponent<JB_PlayerStats>().armour;
        }

        // Clamp health as it is
        m_health = Mathf.Min(m_health, m_maxHealth);


    }

    /// <summary>
    /// Restores health to this object. Will do nothing if dead
    /// </summary>
    /// <param name="amount">Amount of health to restore</param>
    /// <returns>Amount of health restored</returns>
    public float RestoreHealth(float amount)
    {
        if (amount > 0)
            return RestoreHealthImpl(amount);

        return 0f;
    }

    /// <summary>
    /// Applies damage to this object. Will do nothing if dead.
    /// </summary>
    /// <param name="amount">Amount of damage to apply</param>
    /// <param name="args">Additional arguments to provide about the damage</param>
    /// <returns>Amount of damage applied</returns>
    public float ApplyDamage(DamageInfo damage, DamageEvent args = null)
    {
    
        return ApplyDamageImpl(damage, args);

    }


    /// <summary>
    /// Implementation for restoring health, does not take in damage info
    /// </summary>
    /// <param name="amount">Amount of health to restore</param>
    /// <returns>Amount of health applied</returns>
    private float RestoreHealthImpl(float amount)
    {
        if (amount <= 0f)
            return 0f;

        // Don't bother if already at max health
        if (health >= maxHealth)
            return 0f;

        float OldHealth = m_health;
        m_health = Mathf.Clamp(OldHealth + amount, 0f, m_maxHealth);

        // Update delta based on how much health has actually changed
        // Value will be positive
        amount = m_health - OldHealth;

        InvokeEvents(amount, null, null);
        return amount;
    }

    /// <summary>
    /// Implementation for applying damage, takes in a damage info instance
    /// </summary>
    /// <param name="info">Info containing details for damage</param>
    /// <param name="args">Event causing the damage</param>
    /// <returns>Amount of damage applied</returns>
    private float ApplyDamageImpl(DamageInfo info, DamageEvent args)
    {
        if (!info)
            return 0f;

        float damage = info.damage;
        int dmgDuration = info.dmgDuration;
        DamageType dmgType = info.damageType;

        if(info.stunDuration > 0)
        {
            // TODO - stun this object
        }


        // determine what type of damage
        if(dmgType == DamageType.Normal || dmgType == DamageType.Fire)
        {
            float dmgReduction = 3 % m_armour;

            if(dmgReduction > 0)
            {
                // reduce damage amount based on this object's armour
                damage = ((100f - dmgReduction) / 100) * damage;
            }

            if(m_armour < 0)
            {
                // increase damage taken per negative armour
                damage *= damageMultiplier;
            }
            
        }
        
        
        if (isDead || damage <= 0f)
            return 0f;

        // Stop here if recieving damage but we are invincible
        if (m_invincible)
            return 0f;


        // calculations depending on damage type
        float oldHealth = m_health;

        switch (dmgType)
        {
            case DamageType.Normal:
                // TODO - add in critical chance calculation
                if (GetComponent<JB_PlayerStats>())
                {

                }
                m_health = Mathf.Clamp(oldHealth - damage, 0f, m_maxHealth);

                break;
            case DamageType.Fire:
                float fireDmg = damage / dmgDuration;
                StartCoroutine(ApplyFire(1f, dmgDuration, fireDmg));
                break;
            case DamageType.Poison:
                StartCoroutine(ApplyPoison(1f, dmgDuration));
                break;
            default:
                break;
        }


        // Update delta based on how much health has actually changed
        // Value will be negative
        damage = m_health - oldHealth;



        InvokeEvents(damage, info, args);
        return -damage;
    }

    
    IEnumerator ApplyFire(float damageDuration, int damageCount, float damageAmount)
    {

        int currentCount = 0;

        while (currentCount < damageCount)
        {
            m_health -= damageAmount;
            yield return new WaitForSeconds(damageDuration);
            currentCount++;
        }

    }

    IEnumerator ApplyPoison(float damageDuration, int damageCount)
    {

        int currentCount = 0;

        while (currentCount < damageCount)
        {
            float dmg = (0.01f * m_health);
            m_health -= dmg;
            yield return new WaitForSeconds(damageDuration);
            currentCount++;
        }

    }

    /// <summary>
    /// Invokes health changed events based on both current values and given params
    /// </summary>
    /// <param name="delta">Delta of health change</param>
    /// <param name="damageInfo">Damage info if damaged</param>
    /// <param name="damageArgs">Damage args if damaged</param>
    private void InvokeEvents(float delta, DamageInfo damageInfo, DamageEvent damageArgs)
    {
        if (OnHealthChanged != null)
            OnHealthChanged.Invoke(this, m_health, delta);

        if (delta < 0f)
            if (OnDamaged != null)
                OnDamaged.Invoke(this, Mathf.Abs(delta), damageInfo, damageArgs);

        if (isDead)
            if (OnDeath != null)
                OnDeath.Invoke(this);


    }

    // buffs or reduces current targets armour
    public IEnumerator ArmourAdjustment(float amount, float duration)
    {
        // holding initial armour value
        float temp;
        temp = m_armour;

        m_armour += amount;

        if(m_armour < 0)
        {
            damageMultiplier += Mathf.Abs(m_armour) / 100f;
        }

        yield return new WaitForSeconds(duration);

        m_armour = temp;
        damageMultiplier = 1f;
    }
}
