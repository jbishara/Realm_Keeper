using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



/// <summary>
/// Inheritable class to be attached to each ENEMY Ability
/// </summary>
public class EM_BaseEnemyAbility
{
    /// <summary>
    /// Reference to the reach MonoBehaviour
    /// Not super experienced with Unity so i skipped ScriptObj
    /// </summary>
    internal readonly FSM_Enemy parent;

    /// <summary>
    /// Timing variables, used to control delays
    /// </summary>
    private float currentCooldown;

    /// <summary>
    /// Ability
    /// <para>Inherited from EnemyPresets class</para>
    /// </summary>
    public AiEnemyAbilities Ability;

    /// <summary>
    /// Indicates that the ability is currently on cooldown
    /// </summary>
    public bool AbilityOnCooldown;

    /// <summary>
    /// Indicates that the ability logic is currently running
    /// </summary>
    public bool AbilityOngoing;

    /// <summary>
    /// Indicates if the ability is enabled
    /// <para>default = true</para>
    /// </summary>
    public bool AbilityEnabled = true;

    /// <summary>
    /// Reference to the player Health Component
    /// </summary>
    public HealthComponent PlayerHealthComponentRef => PlayerGameObject.GetComponent<HealthComponent>();

    /// <summary>
    /// Reference to the player Health Component
    /// </summary>
    public HealthComponent EnemyHealthComponentRef;

    /// <summary>
    /// Contains the specific information for this type of ability
    /// </summary>
    public AbilityInfo AbilityInformation;

    /// <summary>
    /// Player transform
    /// </summary>
    public Transform PlayerTransform => PlayerGameObject.transform;

    public Animator EnemyAnimator => parent.Animator;

    /// <summary>
    /// Game object of the player
    /// </summary>
    public GameObject PlayerGameObject;

    /// <summary>
    /// Cast time
    /// </summary>
    internal DateTime CastTime;

    /// <summary>
    /// Constructs a EM_BaseEnemyAbility, this class should not be used as is but should be inherited to another real ability class
    /// </summary>
    /// <param name="parent">Reference to MonoBehaviour to be able to do GetComponent</param>
    /// <param name="abilityEnumVal">Which ability this is</param>
    /// <param name="information"></param>
    public EM_BaseEnemyAbility(FSM_Enemy parent, AiEnemyAbilities abilityEnumVal, AbilityInfo information)
    {
        // Set the parameters to local variables/properties
        this.parent = parent;
        Ability = abilityEnumVal;
        AbilityInformation = information;
        PlayerGameObject = parent.Player;
        EnemyHealthComponentRef = parent.EnemyHealthComp;
    }

    /// <summary>
    /// Runs when the Ability is first initialized
    /// <para>If there are any code that every ability should have, implement it here</para>
    /// </summary>
    public virtual void EnterAbility() { }


    /// <summary>
    /// Runs the ability
    /// <para>Note: Please let the base.DoAbility run first</para>
    /// </summary>
    public void DoAbility()
    {
        if (AbilityOngoing) return;

        // Cooldown related stuff
        currentCooldown = AbilityInformation.cooldown;
        AbilityOnCooldown = true;
        AbilityOngoing = true;
        CastTime = DateTime.Now + TimeSpan.FromSeconds(AbilityInformation.castTime);
    }

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

        // Updates the ability during the Cast Time
        UpdateDuringCastTime();

        // Update the inner ability after the cast time is OK
        if (DateTime.Now > CastTime)
            UpdateAbilityInner();
    }

    /// <summary>
    /// Updates the ability
    /// </summary>
    internal virtual void UpdateAbilityInner() { }

    internal virtual void UpdateDuringCastTime() { }

    /// <summary>
    /// Checks if player can do Ability
    /// Override if there should be more calculations 
    /// </summary>
    /// <param name="currentRange">Range from Unit to Player</param>
    /// <returns></returns>
    public virtual bool CanDoAbility(float currentRange)
    {
        return currentRange <= AbilityInformation.abilityRange;
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
