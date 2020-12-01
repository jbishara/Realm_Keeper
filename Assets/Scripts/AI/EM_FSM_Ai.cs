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
    /// Player Positon
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
    /// <para>Note: <see cref="EM_FSM_AiState.FsmAiStandardBehaviour.Patrol"/> must be current Behaviour or will throw an <see cref="System.Exception"/></para>
    /// </summary>
    public List<Vector3> CheckPoints
    {
        get
        {
            // Gets the value of the current behaviour is set to Patrol
            if (Behaviour.Equals(EM_FSM_AiState.FsmAiStandardBehaviour.Patrol))
                return checkpoints;
            else throw new System.Exception("Wrong behaviour to an enemy is set, set to Patrol to use Checkpoints");
        }
        set => checkpoints = value;

    }

    /// <summary>
    /// Checkpoints for the patrol
    /// </summary>
    private List<Vector3> checkpoints;





    public int MaximumDistance;

    private NavMeshAgent agent;
    private Animator animator;
    private EM_FSM_AiState currentState;


    public Vector3 SpawnPosition;
    public Quaternion SpawnRotation;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();

        SpawnRotation = agent.transform.rotation;
        SpawnPosition = agent.transform.position;


        currentState = new Idle(this.gameObject,
            agent,
            animator,
            Player,
            this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.FsmProcessUpdate();
    }
}
