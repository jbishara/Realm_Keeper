using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Ai Base, this class hosts the AI for the individual enemy.
/// This should be attached to every enemy
/// </summary>
public class EM_FSM_Ai : MonoBehaviour
{
    /// <summary>
    /// Player Position
    /// </summary>
    public Transform Player;

    /// <summary>
    /// Enemy Behaviour, available modes:
    /// <para>Guards a location -> <see cref="EM_FSM_AiState.FsmAiStandardBehaviour.Guard"/></para>
    /// <para>Patrols between a set of checkpoints -> <see cref="EM_FSM_AiState.FsmAiStandardBehaviour.Patrol"/></para>
    /// </summary>
    public EM_FSM_AiState.FsmAiStandardBehaviour Behaviour;

    /// <summary>
    /// Enemy statistics
    /// </summary>
    public EM_FSM_EnemyEntityStatistic EnemyEntityStatistic;

    /// <summary>
    /// Checkpoints to patrol
    /// </summary>
    public List<Vector3> CheckPoints;
    
    /// <summary>
    /// Maximum distance before calling retreat
    /// </summary>
    public float MaximumDistance;

    /// <summary>
    /// Reference to NavMeshAgent
    /// </summary>
    private NavMeshAgent agent;

    /// <summary>
    /// Reference to Animator
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Current State of the Ai, this gets disposed and reinitilized when states is changed
    /// (Not the most efficient method but best for this structure)
    /// </summary>
    private EM_FSM_AiState currentState;

    /// <summary>
    /// Spawn Postion of the Enemy
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


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        SpawnRotation = agent.transform.rotation;
        SpawnPosition = agent.transform.position;


        currentState = new Idle(this.gameObject,
            agent,
            animator,
            Player,
            this);
    }


    void Update()
    {
        // Updates the FSM each UPS
        currentState = currentState.FsmProcessUpdate();
        EnemyEntityStatistic.Update(this);
    }
}
