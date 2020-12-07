using System;
using System.Collections;
using System.Collections.Generic;
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
    public Transform Player; 

    /// <summary>
    /// Enemy statistics
    /// </summary>
    public EM_FSM_EnemyEntityStatistic EnemyEntityStatistic;

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
    private Animator animator;

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


    public EM_FSM_Enemy()
    {
        EnemyEntityStatistic = new EM_FSM_EnemyEntityStatistic(this);

    }

    /// <summary>
    /// Start Logic
    /// </summary>
    void Start()
    {
        // Set the parameters
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SpawnRotation = Agent.transform.rotation;
        SpawnPosition = Agent.transform.position;


        // Runs the start code inside the EnemyEntityStatistic, also Ability.Start will be run inside
        EnemyEntityStatistic.Start(this);

        // Creates a new Guard State, this is the default state
        CurrentAiState = new Guard(this.gameObject,
            Agent,
            animator,
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
        EnemyEntityStatistic.Update(this);

    }
}
