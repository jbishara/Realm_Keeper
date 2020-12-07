using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.AI.Enemy;
using UnityEngine;

/// <summary>
/// Probably Temporary
/// Used to define stats for the AiEnemyAbilities
/// Maybe make this to script-able objects in the future but for now this works and isn't causing any performance issues.
/// </summary>
public static class EnemyPresets
{
    /// <summary>
    /// Dictionary with the ability stats inside
    /// </summary>
    public static readonly Dictionary<AiEnemyAbilities, EnemyAbilityStats> EnemyAbilityStats =
        new Dictionary<AiEnemyAbilities, EnemyAbilityStats>
        {
                // Character   : None
                // Ability     : Empty
                // Description : Used as a placeholder to prevent errors if an ability is not assigned
                {AiEnemyAbilities.Empty, new EnemyAbilityStats
                {
                    AbilityRange = int.MaxValue,
                    CastTime = int.MaxValue,
                    AbilityType = BaseEnemyAbilityType.NormalDamageDealer,

                }},
                // Character   : Bagooblin
                // Ability     : Slash
                {AiEnemyAbilities.Bagooblin1Slash, new EnemyAbilityStats
                {
                    AbilityRange = 10,
                    CastTime = 0,
                    AbilityType = BaseEnemyAbilityType.NormalDamageDealer,

                }},
                // Character   : Bagooblin
                // Ability     : Bite
                {AiEnemyAbilities.Bagooblin2Bite, new EnemyAbilityStats
                {
                    AbilityRange = 10,
                    CastTime = 0,
                    AbilityType = BaseEnemyAbilityType.NormalDamageDealer,
                }},
        };

    // public static readonly Dictionary<EM_FSM_EnemyEntityStatistic.AiEnemyAbilities>

    /// <summary>
    /// Applies the Ability Stats to the BaseAbility inherited class.
    /// </summary>
    /// <param name="a">BASE ABILITY REFERENCE</param>
    public static void ApplyStatisticValues(this BaseAbility a)
    {
        // Sets the parameters
        a.AbilityRange = EnemyAbilityStats[a.Ability].AbilityRange;
        a.CastTime = EnemyAbilityStats[a.Ability].CastTime;
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
    public List<AbilityEffect> EffectCaused = new List<AbilityEffect>();
    public BaseEnemyAbilityType AbilityType;

}






