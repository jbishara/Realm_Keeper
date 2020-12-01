using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// List of abilities, 
    /// </summary>
    public enum Abilities
    {
        /// <summary>
        /// None
        /// </summary>
        Empty = 0,

        /// <summary>
        /// Slashes with their dagger
        /// </summary>
        BagooblinA1Slash,

        /// <summary>
        /// Bites and heals for a small amount of the damages dealt
        /// </summary>
        BagooblinA2Bite,

        /// <summary>
        /// Swings with they big arms
        /// </summary>
        TrollA1Swing,

        /// <summary>
        /// Shoots out spikes 5 in a random direction around itself. That will slow down the player’s movement speed to 30% for 3 seconds
        /// </summary>
        TrollA2ShootSpike,

        /// <summary>
        /// Shoots flame at the target, on hit it will apply fire stack
        /// </summary>
        ElementalsA1ShootFlame,

        /// <summary>
        /// Dashes towards the player and knocks them to the side on collision dealing massive damages
        /// </summary>
        ElementalsA2Dash,

        /// <summary>
        /// Swings with they hand and on hit will knock the player 
        /// </summary>
        StoneGolemA1Swing,

        /// <summary>
        /// Increases they own armor with 25 for 5 seconds
        /// </summary>
        StoneGolemA2SelfBuff,

        /// <summary>
        /// Slashes the player with a sacrifice knife that has  a  40% to apply a  slow effect to the player’s movement speed to 30% for 3 seconds
        /// </summary>
        CultistA1Swing,

        /// <summary>
        /// Summons 2 Imps 
        /// </summary>
        CultistA2Summon,

        /// <summary>
        /// Two hands they pitch fork and running forward then gets tired after 3 seconds 
        /// </summary>
        ImpA1AttackAndEvade,

        /// <summary>
        /// Sacrifice a bit of 10% max health to increase movement speed and attack speed with 30% for 6 seconds
        /// </summary>
        ImpA2SelfBuff,

        /// <summary>
        /// Swings with his hand and knocks the player to the corner of the room
        /// </summary>
        CyclopsA1Swing,

        /// <summary>
        /// The cyclops lets out a bit roar that will slow down the player’s movement speed to 30% for 3 seconds
        /// </summary>
        CyclopsA2Roar,

        /// <summary>
        /// Roars at his minions to come and help him. Makes 3 Bagooblins to come from caves above the arena and join the battle
        /// </summary>
        CyclopsA3SummonRoar,

        /// <summary>
        /// Swings with it’s sharp crystal parts at the player
        /// </summary>
        CrystalGolemA1Swing,

        /// <summary>
        /// Slams the ground that creates a shock wave that the player needs to jump over
        /// </summary>
        CrystalGolemA2SlamGround,

        /// <summary>
        /// Activates the crystal power and reflects any projective that will be thrown for 5 seconds
        /// </summary>
        CrystalGolemA3Shield,

        /// <summary>
        /// Summons small imps to fight the player
        /// </summary>
        NoctuaA1Summon,

        /// <summary>
        /// Throws up lava on a part of the stage that will damages anything that is in it, making that part of the stage disable to be used for a 7 seconds
        /// </summary>
        NoctuaA2Aoe,

        /// <summary>
        /// Swings with his hand on the stage where the player needs to jump above it or jump up on one of the elevated rocks to dodge the attack
        /// </summary>
        NoctuaA3Swing,
    }


    /// <summary>
    /// EnemyType
    /// <para>Do not change during runtime</para>
    /// </summary>
    public EnemyTypes EnemyType;

    /// <summary>
    /// Ability slot 1, used as the "basic attack"
    /// </summary>
    public Abilities BoundAbility1;

    /// <summary>
    /// Ability slot A2
    /// </summary>
    public Abilities BoundAbility2;

    /// <summary>
    /// Ability slot 3, for bosses
    /// </summary>
    public Abilities BoundAbility3;

    /// <summary>
    /// Flag, this is used if this enemy differs from the usual enemies
    /// </summary>
    public AiEnemyFlag EnemyFlag;

    /// <summary>
    /// Maximum health
    /// </summary>
    public float MaxHealth;

    /// <summary>
    /// Base Health regeneration
    /// </summary>
    public float HealthRegen;

    /// <summary>
    /// Armor
    /// </summary>
    public float Armor;

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
    /// Invulnerability, the enemy will pursue and attack but cannot die
    /// </summary>
    public bool IsInvulnerable;

    /// <summary>
    /// Harmless, the enemy will pursue but not attack
    /// </summary>
    public bool IsHarmless;

    /// <summary>
    /// Indicates if the enemy will pursue the player or just do its tasks without attacking
    /// </summary>
    public bool IsTargetingPlayer = true;

    /// <summary>
    /// Current Health of the enemy
    /// </summary>
    public float CurrentHealth;

    /// <summary>
    /// Effects active to this enemy 
    /// </summary>
    public List<AbilityEffect> ActiveEffects;


    public EM_FSM_EnemyEntityStatistic()
    {
        ActiveEffects = new List<AbilityEffect>();
    }


    public void Update(EM_FSM_Ai Ref)
    {




    }




}

