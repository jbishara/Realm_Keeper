using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.AI.Enemy;
using UnityEngine;


public sealed class EM_BagooblinSlash : BaseAbility
{



    public override void EnterAbility()
    {
        base.EnterAbility();
    }



    internal override void UpdateAbilityInner()
    {
        
        Vector3 tmp = (gameReference.Player.position - gameReference.Agent.transform.position);

        // Checks if the player is inside given angle
        if (Vector3.Dot(tmp.normalized, gameReference.Agent.transform.position) > 0.5)
        { 
            // Enemy Hit
            Debug.LogWarning("HIT");
        }
        else
        {
            // Enemy Missed

        }

        AbilityOngoing = false;
    }

    public override bool CanDoAbility(float currentRange)
    {
        return base.CanDoAbility(currentRange);
    }


    public EM_BagooblinSlash(EM_FSM_Enemy gameRef, EM_FSM_EnemyEntityStatistic.EnemyAbilities abilityEnumVal) : base(gameRef, abilityEnumVal)
    {
    }
}

