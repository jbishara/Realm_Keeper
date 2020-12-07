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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enemyRef"></param>
        /// <param name="abilityEnumVal"></param>
        /// <param name="information"></param>
        public EM_EmptyAbility(EM_FSM_Enemy enemyRef, AiEnemyAbilities abilityEnumVal, AbilityInfo information) : base(enemyRef, abilityEnumVal, information) { }

        internal override void UpdateAbilityInner()
        {
            AbilityOnCooldown = true;
        }
    }
}
