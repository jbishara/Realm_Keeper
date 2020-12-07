using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI.Enemy
{
    /// <summary>
    /// Temporary ability to prevent game crash, it doesn't do anything
    /// </summary>
    public sealed class EM_EmptyAbility : BaseAbility
    {

        public EM_EmptyAbility(EM_FSM_Enemy gameRef, EM_FSM_EnemyEntityStatistic.EnemyAbilities abilityEnumVal) : base(gameRef, abilityEnumVal)
        {}

        internal override void UpdateAbilityInner()
        {
            AbilityOnCooldown = true;
        }
    }
}
