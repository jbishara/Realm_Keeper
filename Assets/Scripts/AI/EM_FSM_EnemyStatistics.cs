using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Assets.Scripts.AI.Enemy;
using UnityEngine;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

[Serializable]
public sealed class EM_FSM_EnemyEntityStatistic
{

    /// <summary>
    /// List of available Enemies
    /// </summary>
    public enum EnemyTypes
    {
        TestEnemy = 0,
        Bagooblin,
        Troll,
        Cyclops,
        StoneGolem,
        Elemental,
        CrystalGolem,
        SmallImp,
        Cultist,
        Noctua,
    }

    /// <summary>
    /// List of available Flags
    /// </summary>
    public enum AiEnemyFlag
    {
        None,
        Boss
    }



    /// <summary>
    /// EnemyType
    /// <para>Do not change during runtime</para>
    /// </summary>
    public EnemyTypes EnemyType;

    /// <summary>
    /// Ability slot 1, used as the "basic attack"
    /// </summary>
    public EnemyAbilities BoundAbility1;

    /// <summary>
    /// Ability slot A2
    /// </summary>
    public EnemyAbilities BoundAbility2;

    /// <summary>
    /// Ability slot 3, for bosses
    /// </summary>
    public EnemyAbilities BoundAbility3;

    /// <summary>
    /// Flag, this is used if this enemy differs from the usual enemies
    /// </summary>
    public AiEnemyFlag EnemyFlag;

    /// <summary>
    /// Base Health regeneration
    /// </summary>
    public float HealthRegen;


    /// <summary>
    /// Cooldown multiplier, if set to lower attack speed will be higher
    /// </summary>
    public float CoolDown = 1.00f;

    /// <summary>
    /// Multiplier of the attack damage, this does not affect the fire dmg/poison dmg
    /// </summary>
    public float AttackDamage = 1.0f;

    /// <summary>
    /// MovementSpeed Multiplier
    /// </summary>
    public float MovementSpeed = 1.0f;

    /// <summary>
    /// Vision Range, how long the enemy will se
    /// </summary>
    public float VisionRange = 20f;

    /// <summary>
    /// Angle based on forward vector to that the enemy sees
    /// </summary>
    public float VisionAngle = 60f;

    /// <summary>
    /// Closest distance this entity should have to the player
    /// </summary>
    public float ClosestDist2P = 4f;

    /// <summary>
    /// Harmless, the enemy will pursue but not attack
    /// </summary>
    public bool IsHarmless;

    /// <summary>
    /// Effects active to this enemy 
    /// </summary>
    public List<AbilityEffect> ActiveEffects;

    /// <summary>
    ///
    /// </summary>
    public Dictionary<EnemyAbilities, BaseAbility> AttachedAbilities;

    /// <summary>
    /// Used for a test
    /// </summary>
    private EM_FSM_Enemy gameReference;

    /// <summary>
    /// Health Component Used by this Unit
    /// </summary>
    public HealthComponent EnemyHealthComponent;


    public EM_FSM_EnemyEntityStatistic(EM_FSM_Enemy monoBehaviour)
    {
        ActiveEffects = new List<AbilityEffect>();
        gameReference = monoBehaviour;
    }

    public void Start(EM_FSM_Enemy Ref)
    {
        // Adds the abilities
        AttachedAbilities = new Dictionary<EnemyAbilities, BaseAbility>()
        {
            {BoundAbility1, GetAbilityFromEnum(BoundAbility1)},
            {BoundAbility2, GetAbilityFromEnum(BoundAbility2)},
        };

        // Adds the third ability if its a boss
        if (EnemyFlag == AiEnemyFlag.Boss && BoundAbility3 != EnemyAbilities.Empty)
            AttachedAbilities.Add(BoundAbility3, GetAbilityFromEnum(BoundAbility3));

        // Run once code for the abilities
        // Wanted to use LINQ but this saves performance :(
        for (int i = 0; i < AttachedAbilities.Count; i++)
        {
            AttachedAbilities.ElementAt(i).Value.EnterAbility();
        }

        // Uses the code already implemented for health.
        EnemyHealthComponent = gameReference.GetComponent<HealthComponent>();

        // Runs the Ai code if the enemy dies
        EnemyHealthComponent.OnDeath += delegate (HealthComponent self)
        {
            Ref.CurrentAiState.KillUnit();
        };
    }


    public void Update(EM_FSM_Enemy Ref)
    {


        // Update abilities
        // Wanted to use LINQ but this saves performance :(
        for (int i = 0; i < AttachedAbilities.Count; i++)
        {
            AttachedAbilities.ElementAt(i).Value.UpdateAbility();
        }
    }




    internal BaseAbility GetAbilityFromEnum(EnemyAbilities enemyAbility)
    {

        switch (enemyAbility)
        {
            case EnemyAbilities.BagooblinAbility1Slash: return new EM_BagooblinSlash(gameReference, enemyAbility);
            case EnemyAbilities.BagooblinAbility2Bite: return new EM_BagooblinBite(gameReference, enemyAbility);
            case EnemyAbilities.TrollAbility1Swing:
            case EnemyAbilities.TrollAbility2ShootSpike:
            case EnemyAbilities.ElementalsAbility1ShootFlame:
            case EnemyAbilities.ElementalsAbility2Dash:
            case EnemyAbilities.StoneGolemAbility1Swing:
            case EnemyAbilities.StoneGolemAbility2SelfBuff:
            case EnemyAbilities.CultistAbility1Swing:
            case EnemyAbilities.CultistAbility2Summon:
            case EnemyAbilities.ImpAbility1AttackAndEvade:
            case EnemyAbilities.ImpAbility2SelfBuff:
            case EnemyAbilities.CyclopsAbility1Swing:
            case EnemyAbilities.CyclopsAbility2Roar:
            case EnemyAbilities.CyclopsAbility3SummonRoar:
            case EnemyAbilities.CrystalGolemAbility1Swing:
            case EnemyAbilities.CrystalGolemAbility2SlamGround:
            case EnemyAbilities.CrystalGolemAbility3Shield:
            case EnemyAbilities.NoctuaAbility1Summon:
            case EnemyAbilities.NoctuaAbility2Aoe:
            case EnemyAbilities.NoctuaAbility3Swing:
            default:
            case EnemyAbilities.Empty: return new EM_EmptyAbility(gameReference, enemyAbility);
        }
    }

    /// <summary>
    /// List of abilities, 
    /// </summary>
    public enum EnemyAbilities
    {
        /// <summary>
        /// None
        /// </summary>
        Empty = 0,

        /// <summary>
        /// Slashes with their dagger
        /// </summary>
        BagooblinAbility1Slash,

        /// <summary>
        /// Bites and heals for a small amount of the damages dealt
        /// </summary>
        BagooblinAbility2Bite,

        /// <summary>
        /// Swings with they big arms
        /// </summary>
        TrollAbility1Swing,

        /// <summary>
        /// Shoots out spikes 5 in a random direction around itself. That will slow down the player’s movement speed to 30% for 3 seconds
        /// </summary>
        TrollAbility2ShootSpike,

        /// <summary>
        /// Shoots flame at the target, on hit it will apply fire stack
        /// </summary>
        ElementalsAbility1ShootFlame,

        /// <summary>
        /// Dashes towards the player and knocks them to the side on collision dealing massive damages
        /// </summary>
        ElementalsAbility2Dash,

        /// <summary>
        /// Swings with they hand and on hit will knock the player 
        /// </summary>
        StoneGolemAbility1Swing,

        /// <summary>
        /// Increases they own armor with 25 for 5 seconds
        /// </summary>
        StoneGolemAbility2SelfBuff,

        /// <summary>
        /// Slashes the player with a sacrifice knife that has  a  40% to apply a  slow effect to the player’s movement speed to 30% for 3 seconds
        /// </summary>
        CultistAbility1Swing,

        /// <summary>
        /// Summons 2 Imps 
        /// </summary>
        CultistAbility2Summon,

        /// <summary>
        /// Two hands they pitch fork and running forward then gets tired after 3 seconds 
        /// </summary>
        ImpAbility1AttackAndEvade,

        /// <summary>
        /// Sacrifice a bit of 10% max health to increase movement speed and attack speed with 30% for 6 seconds
        /// </summary>
        ImpAbility2SelfBuff,

        /// <summary>
        /// Swings with his hand and knocks the player to the corner of the room
        /// </summary>
        CyclopsAbility1Swing,

        /// <summary>
        /// The cyclops lets out a bit roar that will slow down the player’s movement speed to 30% for 3 seconds
        /// </summary>
        CyclopsAbility2Roar,

        /// <summary>
        /// Roars at his minions to come and help him. Makes 3 Bagooblins to come from caves above the arena and join the battle
        /// </summary>
        CyclopsAbility3SummonRoar,

        /// <summary>
        /// Swings with it’s sharp crystal parts at the player
        /// </summary>
        CrystalGolemAbility1Swing,

        /// <summary>
        /// Slams the ground that creates a shock wave that the player needs to jump over
        /// </summary>
        CrystalGolemAbility2SlamGround,

        /// <summary>
        /// Activates the crystal power and reflects any projective that will be thrown for 5 seconds
        /// </summary>
        CrystalGolemAbility3Shield,

        /// <summary>
        /// Summons small imps to fight the player
        /// </summary>
        NoctuaAbility1Summon,

        /// <summary>
        /// Throws up lava on a part of the stage that will damages anything that is in it, making that part of the stage disable to be used for a 7 seconds
        /// </summary>
        NoctuaAbility2Aoe,

        /// <summary>
        /// Swings with his hand on the stage where the player needs to jump above it or jump up on one of the elevated rocks to dodge the attack
        /// </summary>
        NoctuaAbility3Swing,
    }
}

