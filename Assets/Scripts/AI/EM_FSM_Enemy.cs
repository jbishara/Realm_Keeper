using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

/// <summary>
/// enemy Base, this class hosts the AI for the individual enemy.
/// This should be attached to every enemy
/// </summary>
public class EM_FSM_Enemy : MonoBehaviour
{
    /// <summary>
    /// Player Position
    /// </summary>
    public GameObject Player;

    /// <summary>
    /// Maximum distance before calling retreat
    /// </summary>
    public float MaximumDistance;

    /// <summary>
    /// Reference to NavMeshAgent
    /// </summary>
    public NavMeshAgent Agent;

    /// <summary>
    /// Reference to Animator
    /// </summary>
    public Animator Animator;

    /// <summary>
    /// Current State of the enemy, this gets disposed and reinitialized when states is changed
    /// (Not the most efficient method but best for this structure)
    /// </summary>
    public EM_FSM_AiState CurrentAiState;

    /// <summary>
    /// Spawn Position of the Enemy
    /// </summary>
    public Vector3 SpawnPosition;

    /// <summary>
    /// Spawn Rotation of the Enemy
    /// </summary>
    public Quaternion SpawnRotation;

    /// <summary>
    /// Indicates if the animations should be called inside the FSM state
    /// Mostly used for development purposes when we don't have animations ready
    /// </summary>
    public bool UseAnimations = true;

    /// <summary>
    /// When the enemy should fall back after the player has been lost
    /// </summary>
    public int EnemyReturnsAfter = 5;

    /// <summary>
    /// Longest range for a ray to be casted
    /// </summary>
    public int EnemyRayCastMaxRange = 1500;

    /// <summary>
    /// aiEnemyType
    /// <para>Do not change during runtime</para>
    /// </summary>
    public AiEnemyTypes aiEnemyType;

    /// <summary>
    /// Ability slot 1, used as the "basic attack"
    /// </summary>
    public AiEnemyAbilities BoundAbility1;

    /// <summary>
    /// Ability slot A2
    /// </summary>
    public AiEnemyAbilities BoundAbility2;

    /// <summary>
    /// Ability slot 3, for bosses
    /// </summary>
    public AiEnemyAbilities BoundAbility3;

    /// <summary>
    /// Ability info for slot 1
    /// </summary>
    public AbilityInfo AbilityInfo1;

    /// <summary>
    /// Ability info for slot 2
    /// </summary>
    public AbilityInfo AbilityInfo2;

    /// <summary>
    /// Ability info for slot 3, for bosses
    /// </summary>
    public AbilityInfo AbilityInfo3;

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
    /// Will wait this many seconds before calling a new ability
    /// </summary>
    public float AiAwaitNewAttack = 0.5f;

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
    ///
    /// </summary>
    public Dictionary<AiEnemyAbilities, EM_BaseEnemyAbility> AttachedAbilities;

    /// <summary>
    /// Health Component Used by this Unit
    /// </summary>
    public HealthComponent EnemyHealthComp;

    /// <summary>
    /// Health Component, for the player
    /// </summary>
    public HealthComponent PlayerHealthComp;


    public EM_FSM_Enemy()
    {
    }

    /// <summary>
    /// Start Logic
    /// </summary>
    void Start()
    {
        // Set the parameters
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        PlayerHealthComp = Player.GetComponent<HealthComponent>();
        EnemyHealthComp = GetComponent<HealthComponent>();
        SpawnRotation = Agent.transform.rotation;
        SpawnPosition = Agent.transform.position;
        AttachedAbilities = new Dictionary<AiEnemyAbilities, EM_BaseEnemyAbility>();

        // Adds the abilities
        if (BoundAbility1 != AiEnemyAbilities.Empty)
            AttachedAbilities.Add(BoundAbility1, GetAbilityFromEnum(BoundAbility1, AbilityInfo1));

        if (BoundAbility2 != AiEnemyAbilities.Empty)
            AttachedAbilities.Add(BoundAbility2, GetAbilityFromEnum(BoundAbility2, AbilityInfo2));


        // Adds the third ability if its a boss
        if (BoundAbility3 != AiEnemyAbilities.Empty)
            AttachedAbilities.Add(BoundAbility3, GetAbilityFromEnum(BoundAbility3, AbilityInfo3));

        // Run once code for the abilities
        // Wanted to use LINQ but this saves performance :(
        for (int i = 0; i < AttachedAbilities.Count; i++)
        {
            AttachedAbilities.ElementAt(i).Value.EnterAbility();
        }

        // Runs the Ai code if the enemy dies
        EnemyHealthComp.OnDeath += delegate (HealthComponent self)
        {
            CurrentAiState.KillUnit();
        };





        // Creates a new Guard State, this is the default state
        CurrentAiState = new Guard(this.gameObject,
            Agent,
            Animator,
            Player,
            this);


    }

    /// <summary>
    /// Update Logic
    /// </summary>
    void Update()
    {
        // Updates the FSM each UPS
        CurrentAiState = CurrentAiState.FsmProcessUpdate();

        // Update abilities
        // Wanted to use LINQ but this saves performance :(
        for (int i = 0; i < AttachedAbilities.Count; i++)
        {
            AttachedAbilities.ElementAt(i).Value.UpdateAbility();
        }

    }


    private EM_BaseEnemyAbility GetAbilityFromEnum(AiEnemyAbilities aiEnemyAbility, AbilityInfo abInfo)
    {

        switch (aiEnemyAbility)
        {
            default:
            case AiEnemyAbilities.Bagooblin1Slash: return new EM_BagooblinSlash(this, aiEnemyAbility, abInfo);
            case AiEnemyAbilities.Bagooblin2Bite: return new EM_BagooblinBite(this, aiEnemyAbility, abInfo);
            case AiEnemyAbilities.Troll1Swing: return new EM_Troll_Swing(this, aiEnemyAbility, abInfo);
            case AiEnemyAbilities.Troll2ShootSpike: return new EM_TrollShootSpike(this, aiEnemyAbility, abInfo);
            case AiEnemyAbilities.Elementals1ShootFlame:
            case AiEnemyAbilities.Elementals2Dash:
            case AiEnemyAbilities.StoneGolem1Swing:
            case AiEnemyAbilities.StoneGolem2SelfBuff:
            case AiEnemyAbilities.Cultist1Swing:
            case AiEnemyAbilities.Cultist2Summon:
            case AiEnemyAbilities.Imp1AttackAndEvade:
            case AiEnemyAbilities.Imp2SelfBuff:
            case AiEnemyAbilities.Cyclops1Swing:
            case AiEnemyAbilities.Cyclops2Roar:
            case AiEnemyAbilities.Cyclops3SummonRoar:
            case AiEnemyAbilities.CrystalGolem1Swing:
            case AiEnemyAbilities.CrystalGolem2SlamGround:
            case AiEnemyAbilities.CrystalGolem3Shield:
            case AiEnemyAbilities.Noctua1Summon:
            case AiEnemyAbilities.Noctua2Aoe:
            case AiEnemyAbilities.Noctua3Swing: return null;
        }
    }
}


/// <summary>
/// List of available Enemies
/// </summary>
public enum AiEnemyTypes
{
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
public enum AiEnemyAbilities
{
    /// <summary>
    /// None
    /// </summary>
    Empty = 0,

    /// <summary>
    /// Slashes with their dagger
    /// </summary>
    Bagooblin1Slash,

    /// <summary>
    /// Bites and heals for a small amount of the damages dealt
    /// </summary>
    Bagooblin2Bite,

    /// <summary>
    /// Swings with they big arms
    /// </summary>
    Troll1Swing,

    /// <summary>
    /// Shoots out spikes 5 in a random direction around itself. That will slow down the player’s movement speed to 30% for 3 seconds
    /// </summary>
    Troll2ShootSpike,

    /// <summary>
    /// Shoots flame at the target, on hit it will apply fire stack
    /// </summary>
    Elementals1ShootFlame,

    /// <summary>
    /// Dashes towards the player and knocks them to the side on collision dealing massive damages
    /// </summary>
    Elementals2Dash,

    /// <summary>
    /// Swings with they hand and on hit will knock the player 
    /// </summary>
    StoneGolem1Swing,

    /// <summary>
    /// Increases they own armor with 25 for 5 seconds
    /// </summary>
    StoneGolem2SelfBuff,

    /// <summary>
    /// Slashes the player with a sacrifice knife that has  a  40% to apply a  slow effect to the player’s movement speed to 30% for 3 seconds
    /// </summary>
    Cultist1Swing,

    /// <summary>
    /// Summons 2 Imps 
    /// </summary>
    Cultist2Summon,

    /// <summary>
    /// Two hands they pitch fork and running forward then gets tired after 3 seconds 
    /// </summary>
    Imp1AttackAndEvade,

    /// <summary>
    /// Sacrifice a bit of 10% max health to increase movement speed and attack speed with 30% for 6 seconds
    /// </summary>
    Imp2SelfBuff,

    /// <summary>
    /// Swings with his hand and knocks the player to the corner of the room
    /// </summary>
    Cyclops1Swing,

    /// <summary>
    /// The cyclops lets out a bit roar that will slow down the player’s movement speed to 30% for 3 seconds
    /// </summary>
    Cyclops2Roar,

    /// <summary>
    /// Roars at his minions to come and help him. Makes 3 Bagooblins to come from caves above the arena and join the battle
    /// </summary>
    Cyclops3SummonRoar,

    /// <summary>
    /// Swings with it’s sharp crystal parts at the player
    /// </summary>
    CrystalGolem1Swing,

    /// <summary>
    /// Slams the ground that creates a shock wave that the player needs to jump over
    /// </summary>
    CrystalGolem2SlamGround,

    /// <summary>
    /// Activates the crystal power and reflects any projective that will be thrown for 5 seconds
    /// </summary>
    CrystalGolem3Shield,

    /// <summary>
    /// Summons small imps to fight the player
    /// </summary>
    Noctua1Summon,

    /// <summary>
    /// Throws up lava on a part of the stage that will damages anything that is in it, making that part of the stage disable to be used for a 7 seconds
    /// </summary>
    Noctua2Aoe,

    /// <summary>
    /// Swings with his hand on the stage where the player needs to jump above it or jump up on one of the elevated rocks to dodge the attack
    /// </summary>
    Noctua3Swing,
}


