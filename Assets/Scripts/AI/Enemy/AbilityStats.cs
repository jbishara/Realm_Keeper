using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.AI.Enemy;
using UnityEngine;

/// <summary>
/// Probably Temporary
/// Used to define stats for the EnemyAbilities
/// Maybe make this to script-able objects in the future but for now this works and isn't causing any performance issues.
/// </summary>
public static class AbilityStats
{
    /// <summary>
    /// Dictionary with the ability stats inside
    /// </summary>
    public static readonly Dictionary<EM_FSM_EnemyEntityStatistic.EnemyAbilities, EnemyAbilityStats> EnemyAbilityStats =
        new Dictionary<EM_FSM_EnemyEntityStatistic.EnemyAbilities, EnemyAbilityStats>
        {
                // Character   : None
                // Ability     : Empty
                // Description : Used as a placeholder to prevent errors if an ability is not assigned
                {EM_FSM_EnemyEntityStatistic.EnemyAbilities.Empty, new EnemyAbilityStats
                {
                    AbilityRange = int.MaxValue,
                    Cooldown = int.MaxValue,
                    CastTime = int.MaxValue,
                    DamageType = DamageType.Normal,
                    AbilityType = BaseEnemyAbilityType.Normal,

                }},
                // Character   : Bagooblin
                // Ability     : Slash
                {EM_FSM_EnemyEntityStatistic.EnemyAbilities.BagooblinAbility1Slash, new EnemyAbilityStats
                {
                    AbilityRange = 4,
                    Cooldown = 2,
                    CastTime = 0,
                    DamageType = DamageType.Normal,
                    AbilityType = BaseEnemyAbilityType.Normal,

                }},
                // Character   : Bagooblin
                // Ability     : Bite
                {EM_FSM_EnemyEntityStatistic.EnemyAbilities.BagooblinAbility2Bite, new EnemyAbilityStats
                {
                    AbilityRange = 4,
                    Cooldown = 12,
                    CastTime = 0,
                    DamageType = DamageType.Normal,
                    AbilityType = BaseEnemyAbilityType.Normal,
                }},
        };

    /// <summary>
    /// Applies the Ability Stats to the BaseAbility inherited class.
    /// </summary>
    /// <param name="a">BASE ABILITY REFERENCE</param>
    public static void ApplyStatisticValues(this BaseAbility a)
    {
        // Sets the parameters
        a.AbilityRange = EnemyAbilityStats[a.Ability].AbilityRange;
        a.Cooldown = EnemyAbilityStats[a.Ability].Cooldown;
        a.CastTime = EnemyAbilityStats[a.Ability].CastTime;
        a.DamageType = EnemyAbilityStats[a.Ability].DamageType;
        a.AbilityType = EnemyAbilityStats[a.Ability].AbilityType;
        a.EffectCaused = EnemyAbilityStats[a.Ability].EffectCaused;



    }

}

/// <summary>
/// Probably temporary, but holds the EnemyAbilityStats for now
/// </summary>
public class EnemyAbilityStats
{
    public float AbilityRange;
    public float CastTime;
    public float Cooldown;
    public List<AbilityEffect> EffectCaused = new List<AbilityEffect>();
    public DamageType DamageType;
    public BaseEnemyAbilityType AbilityType;
}

