using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JB_EnemyAI : MonoBehaviour
{
    public float lookRadius = 10f;
    public AiEnemyTypes enemyType;

    private Transform player;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Master_Script.instance.player.transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(player.position);

            if(distance <= agent.stoppingDistance)
            {
                // attack target
                // begin attack animation
                FaceTarget();
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
