using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JB_EnemyAI : MonoBehaviour
{
    public float baseAttackDamage;
    public float patrolRadius = 100f;
    public float lookRadius = 10f;
    public float patrolFrequency = 12f;
    public AiEnemyTypes enemyType;
    public LayerMask patrolLayer;

    [Tooltip("These are the ability info scriptable objects")]
    public AbilityInfo [] enemyAbilityInfo;

    [Tooltip("Ensure these strings match the animator parameters in the controller")]
    public string[] animParamaters;

    private Transform player;
    private NavMeshAgent agent;
    private HealthComponent enemyHealthScript;
    private Animator animController;

    private float [] attackTimer;
    private float swingTimer = 2f;

    private float timer;
    private float patrolTimer;

    private bool isPlayerDead;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Master_Script.instance.player.transform;

        enemyHealthScript = GetComponent<HealthComponent>();
        animController = GetComponent<Animator>();

        if (this.gameObject.name.Contains("BOSS"))
        {
            attackTimer = new float[3];
        }
        else
        {
            attackTimer = new float[2];
        }

        for(int i = 0; i<attackTimer.Length; ++i)
        {
            attackTimer[i] = enemyAbilityInfo[i].cooldown;
        }

        enemyHealthScript.OnDamaged += TakeDamage;
        enemyHealthScript.OnDeath += EnemyDeath;

        JB_PlayerAbilities.PlayerDied += PlayerDeath;
        
    }

    private void PlayerDeath()
    {
        isPlayerDead = true;
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        float speed = agent.velocity.magnitude;

        timer += Time.deltaTime;
        patrolTimer += Time.deltaTime;

        animController.SetFloat("Speed", speed);

        if (isPlayerDead) { return; }

        if(distance <= lookRadius)
        {
            agent.SetDestination(player.position);

            FaceTarget();

            if (distance <= agent.stoppingDistance)
            {
                // attack target
                // begin attack animation

                Debug.Log("checking distance if statement");
                AttackController();
            }
            
        }
        else
        {
            // patrol
            if(patrolTimer >= patrolFrequency)
            {
                // generates random number to fluctuate how often enemies patrol
                float rand = Random.Range(0f, patrolFrequency);
                patrolTimer = rand;

                EnemyPatrol();
            }
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void TakeDamage(HealthComponent self, float damage, AbilityInfo info, DamageEvent args)
    {
        // play take damage animation
        animController.SetTrigger("EnemyHit");
    }

    private void EnemyDeath(HealthComponent component)
    {
        animController.SetBool("IsDead", true);
    }


    private void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    private void EnemyPatrol()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;

        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, patrolLayer);
        Vector3 finalPosition = hit.position;

        agent.SetDestination(finalPosition);

    }

    private void AttackController()
    {
        // play animations

        Debug.Log("Function reached! :)");
        //animController.SetTrigger("AttackOne");

        if(timer >= swingTimer)
        {
            int rand = Random.Range(0, attackTimer.Length);

            if(timer >= attackTimer[rand])
            {
                Debug.Log(attackTimer.Length);
                animController.SetTrigger(animParamaters[rand]);
                timer = 0f;
            }
        }
    }

    public void AttackPlayer(int index)
    {
        float offset = transform.position.z + 2f;

        Vector3 attackPoint = new Vector3(transform.position.x, transform.position.y, offset);

        var colInfo = Physics.OverlapSphere(attackPoint, 5f);

        enemyAbilityInfo[index].damage *= enemyAbilityInfo[index].damageMultiplier;

        if (colInfo != null)
        {
            foreach (Collider col in colInfo)
            {
                if (col.gameObject.GetComponent<HealthComponent>() && col.gameObject.tag != "Enemy")
                {
                    Debug.Log("Enemy hit player " + col.gameObject.name);
                    col.gameObject.GetComponent<HealthComponent>().ApplyDamage(enemyAbilityInfo[index]);

                }
            }
        }
    }
}
