using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Ability for Bagooblin Slash
/// </summary>
public sealed class EM_BagooblinSlash : EM_BaseEnemyAbility
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="abilityEnumVal"></param>
    /// <param name="information"></param>
    public EM_BagooblinSlash(FSM_Enemy parent, AiEnemyAbilities abilityEnumVal, AbilityInfo information) : base(parent, abilityEnumVal, information) { }


    /// <summary>
    /// Updates the ability
    /// </summary>
    internal override void UpdateAbilityInner()
    {

        Vector3 tmp = (PlayerTransform.position - parent.Agent.transform.position);

        // Checks if the player is inside given angle
        // 0.5 => 90 degrees 0 => 180 degrees
        if (Vector3.Dot(tmp.normalized, parent.Agent.transform.position) > 0 &&
            Vector3.Distance(parent.Agent.transform.position, PlayerTransform.position) <=
            (AbilityInformation.abilityRange * 1.1f))
        {
            // Inflict Damage
            PlayerHealthComponentRef.ApplyDamage(AbilityInformation);


            Debug.LogWarning("Ability: Bagooblin Slash HIT for " + AbilityInformation.damage);


        }
        else
        {

            // Enemy Missed
            Debug.LogWarning("Ability: Bagooblin Slash MISS");

        }

        AbilityOngoing = false;
    }
}

