﻿using System;
using System.Collections;
using System.Collections.Generic;
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
    /// FSM Ai Actions
    /// </summary>
    public enum FsmState
    {
        Guard,
        Patrol,
        Pursue,
        Retreat,
        Attack,

        /// <summary>
        /// Ai Action for standing still, will not interact with anything.
        /// If attacked it won't move but it can die
        /// <para>This is a test state</para>
        /// </summary>
        Idle
    }

    /// <summary>
    /// FSM Standard Behaviour
    /// </summary>
    public enum FsmAiStandardBehaviour
    {
        Guard,
        Patrol
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
    /// Ai State that is currently Running
    /// </summary>
    public FsmState CurrentFsmState;

    protected EM_FSM_AiState NextFsmState;
    protected FsmEvent CurrentEvent;
    protected GameObject Npc;
    protected Animator Animator;
    protected Transform Player;
    protected NavMeshAgent Agent;
    protected FsmAiStandardBehaviour AiStandardBehaviour;
    protected EM_FSM_Ai Owner;

    /// <summary>
    /// Constructs a base class of the state
    /// <para>Used when creating a new FSM State which inherits this class</para>
    /// </summary>
    /// <param name="npc"></param>
    /// <param name="agent"></param>
    /// <param name="animator"></param>
    /// <param name="player"></param>
    /// <param name="owner"></param>
    public EM_FSM_AiState(GameObject npc, NavMeshAgent agent, Animator animator, Transform player, EM_FSM_Ai owner)
    {
        Owner = owner;
        Npc = npc;
        Agent = agent;
        Animator = animator;
        Player = player;
        AiStandardBehaviour = Owner.Behaviour;
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
    protected EM_FSM_EnemyEntityStatistic EnemyStatistics => Owner.EnemyEntityStatistic;


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
    public bool CleverCanSeePlayer
    {
        get
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
    }

    public bool IsAttackedByPlayer
    {
        get
        {
            // todo: implement this
            return false;
        }
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

        Debug.LogWarning("Created a new State " + newState.ToString());


        object t = null;

        // Creates a new class for the new state
        switch (newState)
        {
            case FsmState.Idle:
                t = new Idle(Npc, Agent, Animator, Player, Owner);
                break;
            case FsmState.Guard:
                t = new Guard(Npc, Agent, Animator, Player, Owner);
                break;
            case FsmState.Pursue:
                t = new Pursue(Npc, Agent, Animator, Player, Owner);
                break;
            case FsmState.Retreat:
                t = new Retreat(Npc, Agent, Animator, Player, Owner);
                break;
        }

        if (exit) CurrentEvent = FsmEvent.Exit;

        return (T)Convert.ChangeType(t, typeof(T));
    }

}


/// <summary>
/// FSM:  Idle State
/// Desc: This is a test state
/// </summary>
public sealed class Idle : EM_FSM_AiState
{

    public Idle(GameObject npc, NavMeshAgent agent, Animator animator, Transform player, EM_FSM_Ai ai) : base(npc, agent, animator, player, ai)
    {
        CurrentFsmState = FsmState.Idle;
    }

    public override void Enter()
    {
        //todo: Animation implementation, this is an example!
        //Animator.SetTrigger("isIdle");
        base.Enter();
    }

    public override void Update()
    {
        //Debug.LogWarning("idle");
        NextFsmState = CreateNextState<Guard>(FsmState.Guard);
        CurrentEvent = FsmEvent.Exit;

    }

    public override void Exit()
    {
        base.Exit();
    }


}

public sealed class Guard : EM_FSM_AiState
{
    public Guard(GameObject npc, NavMeshAgent agent, Animator animator, Transform player, EM_FSM_Ai ai) : base(npc, agent, animator, player, ai)
    {
        CurrentFsmState = FsmState.Guard;

    }

    public override void Enter()
    {
        //Animator.SetTrigger("isIdle");
        Agent.isStopped = true;

        base.Enter();
    }

    public override void Update()
    {
        // Checks if the NPC can see the player, if it can then chase the player
        if (CleverCanSeePlayer)
        {
            NextFsmState = CreateNextState<Pursue>(FsmState.Pursue);
        }



    }

    public override void Exit()
    {
        //Animator.ResetTrigger("isIdle");
        base.Exit();
    }


}

public sealed class Pursue : EM_FSM_AiState
{
    public Pursue(GameObject npc, NavMeshAgent agent, Animator animator, Transform player, EM_FSM_Ai ai) : base(npc, agent, animator, player, ai)
    {
        CurrentFsmState = FsmState.Pursue;
    }


    public override void Enter()
    {
        Agent.speed = 5;
        Agent.isStopped = false;
        base.Enter();
    }

    public override void Update()
    {
        Agent.SetDestination(PlayerPosition);

        if (Vector3.Distance(Owner.SpawnPosition, Agent.transform.position) > Owner.MaximumDistance)
        {
            NextFsmState = CreateNextState<Retreat>(FsmState.Retreat);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }


}

public sealed class Retreat : EM_FSM_AiState
{
    public Retreat(GameObject npc, NavMeshAgent agent, Animator animator, Transform player, EM_FSM_Ai owner) : base(npc, agent, animator, player, owner)
    {
        CurrentFsmState = FsmState.Retreat;

    }

    public override void Enter()
    {
        Agent.speed = 5;
        Agent.isStopped = false;
        base.Enter();
    }

    public override void Update()
    {
        Agent.SetDestination(Owner.SpawnPosition);

        if (Vector3.Distance(Owner.SpawnPosition, Agent.transform.position) < 0.5f)
        {
            Agent.transform.rotation = Quaternion.Slerp(Agent.transform.rotation, Owner.SpawnRotation, 2f * Time.deltaTime);

            if (Quaternion.Angle(Agent.transform.rotation, Owner.SpawnRotation) < 1)
            {

                if (AiStandardBehaviour == FsmAiStandardBehaviour.Guard)
                {
                    NextFsmState = CreateNextState<Guard>(FsmState.Guard);
                }
                else if (AiStandardBehaviour == FsmAiStandardBehaviour.Patrol)
                {
                    NextFsmState = CreateNextState<Patrol>(FsmState.Patrol);
                }
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public sealed class Patrol : EM_FSM_AiState
{
    public Patrol(GameObject npc, NavMeshAgent agent, Animator animator, Transform player, EM_FSM_Ai owner) : base(npc, agent, animator, player, owner)
    {
        CurrentFsmState = FsmState.Patrol;

    }

    public override void Enter()
    {
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

