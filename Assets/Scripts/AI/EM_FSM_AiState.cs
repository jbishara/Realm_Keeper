using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// Finite State Machine (FSM) - AiState
/// <para>Description: Definitions for the different actions used by the AI</para>
/// <para>Written by Eric C. Malmerström</para>
/// </summary>
public class EM_FSM_AiState
{
    /// <summary>
    /// FSM enemy Actions
    /// </summary>
    public enum FsmState
    {
        /// <summary>
        /// Guards an area, this is the current default enemy phase
        /// </summary>
        Guard,

        /// <summary>
        /// Pusure, will hunt the player and when close enough run abilities
        /// </summary>
        Pursue,

        /// <summary>
        /// Retreat, will return to original spawn position
        /// </summary>
        Retreat,

        /// <summary>
        /// Attack Phase
        /// Abilities will be run inside this and after this it will return to Pursue
        /// </summary>
        Attack,

        /// <summary>
        /// enemy Action for standing still, will not interact with anything.
        /// If attacked it won't move but it can die
        /// <para>This is a test state</para>
        /// </summary>
        Idle,

        /// <summary>
        /// Dead Enemy
        /// </summary>
        Dead
    }


    /// <summary>
    /// Processed Events
    /// <para>Indicates where in the FSM execution we are</para>
    /// </summary>
    public enum FsmEvent
    {
        /// <summary>
        /// Enter will be run once when FSM Stage is called
        /// </summary>
        Enter,
        /// <summary>
        /// Update will run each update until exit is called
        /// </summary>
        Update,
        /// <summary>
        /// Exit will run before switching FSM Stage
        /// </summary>
        Exit
    }


    /// <summary>
    /// enemy State that is currently Running
    /// </summary>
    public FsmState CurrentFsmState;

    /// <summary>
    /// New state class, will move to this state next update
    /// </summary>
    protected EM_FSM_AiState NextFsmState;

    /// <summary>
    /// Which part of the inner AI that is currently running
    /// </summary>
    protected FsmEvent CurrentEvent;

    /// <summary>
    /// Enemy Game-object
    /// </summary>
    protected GameObject Npc;

    /// <summary>
    /// Enemy Animator
    /// </summary>
    protected Animator Animator;

    /// <summary>
    /// Player position
    /// </summary>
    protected Transform Player;

    /// <summary>
    /// AI Agent, used to be able to navigate on the map
    /// </summary>
    protected NavMeshAgent Agent;

    /// <summary>
    /// Owner class, used to get references from the enemy class
    /// </summary>
    protected EM_FSM_Enemy enemy;

    /// <summary>
    /// Constructs a base class of the state
    /// <para>Used when creating a new FSM State which inherits this class</para>
    /// </summary>
    /// <param name="npc"></param>
    /// <param name="agent"></param>
    /// <param name="animator"></param>
    /// <param name="player"></param>
    /// <param name="enemy"></param>
    public EM_FSM_AiState(GameObject npc, NavMeshAgent agent, Animator animator, Transform player, EM_FSM_Enemy enemy)
    {
        this.enemy = enemy;
        Npc = npc;
        Agent = agent;
        Animator = animator;
        Player = player;
        CurrentEvent = FsmEvent.Enter;
    }


    /// <summary>
    /// Start of state, this code will be run Once
    /// </summary>
    public virtual void Enter() => CurrentEvent = FsmEvent.Update;

    /// <summary>
    /// Update logic, this code will be run every update until <see cref="Exit"/> is called
    /// </summary>
    public virtual void Update() => CurrentEvent = FsmEvent.Update;

    /// <summary>
    /// End of state, final code to be run once before switching state.
    /// </summary>
    public virtual void Exit() => CurrentEvent = FsmEvent.Exit;

    /// <summary>
    /// Update logic, this handles the events.
    /// <para>Should be called every update inside the inherited class</para>
    /// </summary>
    /// <returns></returns>
    public EM_FSM_AiState FsmProcessUpdate()
    {
        switch (CurrentEvent)
        {
            case FsmEvent.Enter:
                Enter();
                break;

            case FsmEvent.Update:
                Update();
                break;

            case FsmEvent.Exit:
                Exit();
                return NextFsmState;
        }
        return this;
    }

    /// <summary>
    /// Gets the EnemyEntityStatistic Manager, which holds the information about the enemy
    /// </summary>
    protected EM_FSM_EnemyEntityStatistic EnemyStatistics => enemy.EnemyEntityStatistic;


    /// <summary>
    /// Gets the Direction Vector of the player
    /// </summary>
    public Vector3 PlayerDirection => Player.position - Npc.transform.position;

    /// <summary>
    /// Gets the position of the player
    /// </summary>
    public Vector3 PlayerPosition => Player.position;

    /// <summary>
    /// Gets the Angle which the player is to the NPC
    /// </summary>
    public float PlayerAngle => Vector3.Angle(PlayerDirection, Npc.transform.forward);

    /// <summary>
    /// Indicates if the NPC can see the player, <para>This is less processor heavier
    /// then the <see cref="CleverCanSeePlayer"/> but the bot can see through the walls</para>
    /// </summary>
    public bool CanSeePlayer => (PlayerDirection.magnitude < EnemyStatistics.VisionRange && PlayerAngle < EnemyStatistics.VisionAngle);


    /// <summary>
    /// Indicates if the NPC can see the player
    /// </summary>
    public bool CleverCanSeePlayer()
    {
        if (CanSeePlayer)
        {
            // Calculate which angle to cast the ray from the enemy to the player
            if (Vector3.Angle(PlayerDirection, Npc.transform.forward) < EnemyStatistics.VisionAngle)
            {
                // Ray casts a ray towards the player so it can detects if the NPC really can see the player.
                if (Physics.Raycast(Npc.transform.position, PlayerDirection, out RaycastHit info, EnemyStatistics.VisionRange))
                {
                    // Check so the hit target is a player
                    if (info.collider.tag != null && info.collider.tag == "Player")
                    {
                        return true;
                    }
                }
            }
        }
        return false;

    }




    /// <summary>
    /// Creates a new FSM-State
    /// </summary>
    /// <typeparam name="T">New FSM State</typeparam>
    /// <param name="newState">New State</param>
    /// <param name="exit">Auto Start the new FSM State</param>
    /// <returns></returns>
    public T CreateNextState<T>(FsmState newState, bool exit = true)
    {
        object t = null;

        // Creates a new class for the new state
        switch (newState)
        {
            case FsmState.Idle:
                t = new Idle(Npc, Agent, Animator, Player, enemy);
                break;
            case FsmState.Guard:
                t = new Guard(Npc, Agent, Animator, Player, enemy);
                break;
            case FsmState.Pursue:
                t = new Pursue(Npc, Agent, Animator, Player, enemy);
                break;
            case FsmState.Retreat:
                t = new Retreat(Npc, Agent, Animator, Player, enemy);
                break;
            case FsmState.Attack:
                t = new Attack(Npc, Agent, Animator, Player, enemy);
                break;
            case FsmState.Dead:
                t = new Dead(Npc, Agent, Animator, Player, enemy);
                break;
        }

        // If the exit parameter is given =>
        // Exit the state next UPS instead of waiting until another trigger
        if (exit) CurrentEvent = FsmEvent.Exit;

        return (T)Convert.ChangeType(t, typeof(T));
    }

    /// <summary>
    /// Forces a pursue state, to be used when attacked by the player
    /// <para></para>
    /// </summary>
    public void NoticePlayer()
    {
        NextFsmState = CreateNextState<Pursue>(FsmState.Pursue);
        NextFsmState = CreateNextState<Pursue>(FsmState.Pursue);
    }

    /// <summary>
    /// Kills this unit
    /// </summary>
    public void KillUnit()
    {
        NextFsmState = CreateNextState<Dead>(FsmState.Dead);
    }



}


/// <summary>
/// FSM:  Idle State
/// Desc: This is a test state
/// </summary>
public sealed class Idle : EM_FSM_AiState
{
    public Idle(GameObject npc, NavMeshAgent agent, Animator animator, Transform player, EM_FSM_Enemy enemy) : base(npc, agent, animator, player, enemy)
    {
        CurrentFsmState = FsmState.Idle;
        Agent.isStopped = true;

    }

    public override void Enter()
    {
        if (enemy.UseAnimations)
            Animator.SetTrigger("");
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

/// <summary>
/// FSM:    Guard State
/// Desc:   Guards an area and pursues the player if in range
/// </summary>
public sealed class Guard : EM_FSM_AiState
{
    public Guard(GameObject npc, NavMeshAgent agent, Animator animator, Transform player, EM_FSM_Enemy enemy) : base(npc, agent, animator, player, enemy)
    {
        CurrentFsmState = FsmState.Guard;
    }

    public override void Enter()
    {
        Agent.isStopped = true;
        base.Enter();
    }

    public override void Update()
    {
        // Checks if the NPC can see the player, if it can then chase the player
        if (CleverCanSeePlayer())
        {
            NextFsmState = CreateNextState<Pursue>(FsmState.Pursue);
        }


        
        if (Quaternion.Angle(Agent.transform.rotation, enemy.SpawnRotation) > 2)
        {

            float rotAm = 2f * Time.deltaTime;

            Agent.transform.rotation = Quaternion.Slerp(Agent.transform.rotation,
                enemy.SpawnRotation, rotAm);
        }


    }

    public override void Exit()
    {
        //Animator.ResetTrigger("isIdle");
        base.Exit();
    }


}

/// <summary>
/// FSM:    Pursue State
/// Desc:   Pursues the Player and calls Attack phase if the player is in range
/// </summary>
public sealed class Pursue : EM_FSM_AiState
{
    private DateTime playerWasLastSeen;
    private bool ohNoPlayerIsLost;


    public Pursue(GameObject npc, NavMeshAgent agent, Animator animator, Transform player, EM_FSM_Enemy enemy) : base(npc, agent, animator, player, enemy)
    {
        CurrentFsmState = FsmState.Pursue;
    }


    public override void Enter()
    {

        Agent.isStopped = false;
        base.Enter();
    }

    public override void Update()
    {

        // Stop moving the enemy if its to close to the player
        if (Vector3.Distance(Agent.transform.position, PlayerPosition) < EnemyStatistics.ClosestDist2P)
        {
            Agent.velocity = Vector3.zero;
            Agent.isStopped = true;
            Agent.updatePosition = false;
        }
        // Restore movement when player is gaining range
        else
        {
            Agent.isStopped = false;
            Agent.updatePosition = true;
        }



        // Checks if the player is gone
        {
            if (ohNoPlayerIsLost &&
                DateTime.Now > (playerWasLastSeen + TimeSpan.FromSeconds(enemy.EnemyReturnsAfter)))
            {
                NextFsmState = CreateNextState<Retreat>(FsmState.Retreat);
                ohNoPlayerIsLost = false;
                Debug.Log("The enemy lost the player");
            }

            if (Physics.Raycast(Npc.transform.position, PlayerDirection, out RaycastHit info, enemy.EnemyRayCastMaxRange))
            {
                // Check so the hit target is a player
                if (info.collider.tag != null && info.collider.tag == "Player")
                {
                    Agent.SetDestination(PlayerPosition);
                    ohNoPlayerIsLost = false;
                }
                else
                {
                    if (!ohNoPlayerIsLost)
                    {
                        playerWasLastSeen = DateTime.Now;
                    }
                    ohNoPlayerIsLost = true;
                }
            }
        }



        // Calls retreat if its enabled
        if (enemy.MaximumDistance != 0 &&
            Vector3.Distance(enemy.SpawnPosition, Agent.transform.position) > enemy.MaximumDistance)
        {
            NextFsmState = CreateNextState<Retreat>(FsmState.Retreat);
        }


        // Checks if the enemy can cast any abilities
        if (!EnemyStatistics.IsHarmless &&
            EnemyStatistics.AttachedAbilities.Values.Count(
                a =>
                    a.AbilityCheckList() &&
                    a.CanDoAbility(Vector3.Distance(Agent.transform.position, PlayerPosition))) > 0)
        {
            // Creates the attack phase
            NextFsmState = CreateNextState<Attack>(FsmState.Attack);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }


}

/// <summary>
/// FSM:    Retreat State
/// Desc:   Retreats the enemy if the player has moved to far
/// </summary>
public sealed class Retreat : EM_FSM_AiState
{
    public Retreat(GameObject npc, NavMeshAgent agent, Animator animator, Transform player, EM_FSM_Enemy enemy) : base(npc, agent, animator, player, enemy)
    {
        CurrentFsmState = FsmState.Retreat;

    }

    public override void Enter()
    {
        Agent.isStopped = false;
        base.Enter();
    }

    public override void Update()
    {
        Agent.SetDestination(enemy.SpawnPosition);

        if (Vector3.Distance(enemy.SpawnPosition, Agent.transform.position) < 5f)
        {
            NextFsmState = CreateNextState<Guard>(FsmState.Guard);
        }

        if (CleverCanSeePlayer())
        {
            NextFsmState = CreateNextState<Pursue>(FsmState.Pursue);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}

/// <summary>
/// FSM:    Attack State
/// Desc:   Attacks the player if its in range
/// </summary>
public sealed class Attack : EM_FSM_AiState
{
    private EM_FSM_EnemyEntityStatistic.EnemyAbilities decidedEnemyAbility;

    public Attack(GameObject npc, NavMeshAgent agent, Animator animator, Transform player, EM_FSM_Enemy enemy) : base(npc, agent, animator, player, enemy)
    {
        CurrentFsmState = FsmState.Attack;
    }

    public override void Enter()
    {

        // Decide action
        decidedEnemyAbility = EnemyStatistics.AttachedAbilities.First(pair =>
                pair.Value.CanDoAbility(Vector3.Distance(Agent.transform.position, PlayerPosition)) &&
                pair.Value.AbilityCheckList()).Key;

        // Stop the enemy
        Agent.isStopped = true;

        // Start the ability
        EnemyStatistics.AttachedAbilities[decidedEnemyAbility].DoAbility();

        base.Enter();
    }

    public override void Update()
    {

        // Await the ability to finish
        if (!EnemyStatistics.AttachedAbilities[decidedEnemyAbility].AbilityOngoing)
        {
            NextFsmState = CreateNextState<Pursue>(FsmState.Pursue);
        }


    }


    public override void Exit()
    {
        base.Exit();
    }
}

public sealed class Dead : EM_FSM_AiState
{
    public Dead(GameObject npc, NavMeshAgent agent, Animator animator, Transform player, EM_FSM_Enemy enemy) : base(npc, agent, animator, player, enemy)
    {
        CurrentFsmState = FsmState.Dead;

    }

    public override void Enter()
    {
        Agent.isStopped = true;
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

