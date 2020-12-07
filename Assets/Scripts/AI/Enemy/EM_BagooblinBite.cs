using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI.Enemy
{
    public sealed class EM_BagooblinBite : BaseAbility
    {
        
        public override void EnterAbility()
        {
            AbilityEnabled = false;
            base.EnterAbility();
        }

        internal override void UpdateAbilityInner()
        {
         
        }

        public override bool CanDoAbility(float currentRange)
        {
            return base.CanDoAbility(currentRange);
        }


        public EM_BagooblinBite(EM_FSM_Enemy gameRef, EM_FSM_EnemyEntityStatistic.EnemyAbilities abilityEnumVal) : base(gameRef, abilityEnumVal)
        {

        }
    }
}
