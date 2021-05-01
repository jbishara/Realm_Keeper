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

            FaceTarget();

            if (distance <= agent.stoppingDistance)
            {
                // attack target
                // begin attack animation
                AttackController();
            }
        }
        else
        {
            // patrol
            EnemyPatrol();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    private void EnemyPatrol()
    {
        Vector3 randomDirection = Random.insideUnitSphere * lookRadius;

        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, lookRadius, 1);
        Vector3 finalPosition = hit.position;

        agent.SetDestination(finalPosition);

    }

    private void AttackController()
    {
        // play animations

    }
}
