using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public sealed class EM_BagooblinBite : EM_BaseEnemyAbility
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="abilityEnumVal"></param>
    /// <param name="information"></param>
    public EM_BagooblinBite(EM_FSM_Enemy parent, AiEnemyAbilities abilityEnumVal, AbilityInfo information) : base(parent, abilityEnumVal, information) { }



    /// <summary>
    /// Updates the ability
    /// </summary>
    internal override void UpdateAbilityInner()
    {

        Vector3 tmp = (PlayerTransform.position - parent.Agent.transform.position);

        // Checks if the player is inside given angle and hasn't run away
        if (Vector3.Dot(tmp.normalized, parent.Agent.transform.position) > 0 &&
            Vector3.Distance(parent.Agent.transform.position, PlayerTransform.position) <= 
            (AbilityInformation.abilityRange * 1.1f))
        {
            // Inflict Damage
            PlayerHealthComponentRef.ApplyDamage(AbilityInformation);
            Debug.LogWarning("Ability: Bagooblin Bite HIT");

        }
        else
        {
            // Enemy Missed
            Debug.LogWarning("Ability: Bagooblin Bite MISS");
        }

        AbilityOngoing = false;
    }


}
