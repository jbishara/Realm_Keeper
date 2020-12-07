using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public enum BaseEnemyAbilityType
{
    Normal, SelfBuff, Summon
}

/// <summary>
/// Inheriable class to be attached to each ENEMY Ability
/// </summary>
public class BaseAbility
{
    /// <summary>
    /// Reference to the reach MonoBehaviour
    /// Not super experienced with Unity so i skipped ScriptableObject
    /// </summary>
    internal readonly EM_FSM_Enemy gameReference;

    /// <summary>
    /// Range in unit that this ablity has
    /// <para>Inherited from AbilityStats class</para>
    /// </summary>
    public float AbilityRange;

    /// <summary>
    /// Time required to cast this ability
    /// <para>Inherited from AbilityStats class</para>
    /// </summary>
    public float CastTime;

    /// <summary>
    /// Cooldown for this ability
    /// <para>Inherited from AbilityStats class</para>
    /// </summary>
    public float Cooldown;

    /// <summary>
    /// Timing variables, used to control delays
    /// </summary>
    private float currentCooldown, currentCastTime = -1337;

    /// <summary>
    /// Ability
    /// <para>Inherited from AbilityStats class</para>
    /// </summary>
    public EM_FSM_EnemyEntityStatistic.EnemyAbilities Ability;

    /// <summary>
    /// Effects that this ability causes
    /// <para>Inherited from AbilityStats class</para>
    /// </summary>
    public List<AbilityEffect> EffectCaused;

    /// <summary>
    /// Indicates that the ability is currently on cooldown
    /// </summary>
    public bool AbilityOnCooldown;

    /// <summary>
    /// Indicates that the ability logic is currently running
    /// </summary>
    public bool AbilityOngoing;

    /// <summary>
    /// Damage type, which type of damage this ability should inflict on the player
    /// </summary>
    public DamageType DamageType;

    /// <summary>
    /// Indicates if the ability is enabled
    /// <para>default = true</para>
    /// </summary>
    public bool AbilityEnabled = true;

    /// <summary>
    /// Ability Type, which kind of ability this is, this is supposed to used to make the enemy smarter
    /// </summary>
    public BaseEnemyAbilityType AbilityType;

    /// <summary>
    /// Reference to the player Health Component
    /// </summary>
    public HealthComponent PlayerHealthComponentRef;


    /// <summary>
    /// ctor
    /// Constructs a BaseAbility, this class should not be used as is but should be inherited to another real ability class
    /// </summary>
    /// <param name="gameRef">Refernce to MonoBehaviour to be able to do GetComponent</param>
    /// <param name="abilityEnumVal">Which ability this is</param>
    public BaseAbility(EM_FSM_Enemy gameRef, EM_FSM_EnemyEntityStatistic.EnemyAbilities abilityEnumVal)
    {
        // Set the parameters to local variables/properties
        gameReference = gameRef;
        Ability = abilityEnumVal;
        this.ApplyStatisticValues();
        PlayerHealthComponentRef = gameRef.GetComponent<HealthComponent>();
    }

    /// <summary>
    /// Runs when the Ability is first initialized
    /// <para>If there are any code that every ability should have, implement it here</para>
    /// </summary>
    public virtual void EnterAbility() { }

    /// <summary>
    /// Runs every frame that the Ability is used
    /// Run ability code after base
    /// <para>If there are any code that every ability should have, implement it here</para>
    /// </summary>
    public void UpdateAbility()
    {

        // Cooldown code
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
        else AbilityOnCooldown = false;

        // Run inner code if the ability is currently running
        if (!AbilityOngoing) return;

            UpdateAbilityInner();

            //todo: Implement Casting time
        
    }

    /// <summary>
    /// Updates the ability
    /// </summary>
    internal virtual void UpdateAbilityInner() { }
    internal virtual void UpdateAbilityCasting() { }

    /// <summary>
    /// Checks if player can do Ability
    /// Override if there should be more calculations 
    /// </summary>
    /// <param name="currentRange">Range from Unit to Player</param>
    /// <returns></returns>
    public virtual bool CanDoAbility(float currentRange)
    {
        return currentRange <= AbilityRange;
    }

    /// <summary>
    /// Runs the ability
    /// <para>Note: Please let the base.DoAbility run first</para>
    /// </summary>
    public void DoAbility()
    {
        if (AbilityOngoing) return;

        // Cooldown related stuff
        currentCooldown = Cooldown;
        AbilityOnCooldown = true;
        AbilityOngoing = true;


    }

    /// <summary>
    /// Common check do to when check ability
    /// </summary>
    /// <returns></returns>
    public bool AbilityCheckList()
    {
        return !AbilityOngoing && !AbilityOnCooldown && AbilityEnabled;

    }







}
