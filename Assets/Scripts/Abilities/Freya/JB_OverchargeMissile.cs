using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_OverchargeMissile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float delayHoming;

    private Rigidbody rigidBody;
    private GameObject[] enemies;
    private Transform target;
    private float timer = 0;

    private AbilityInfo m_overchargeInfo;

    public AbilityInfo overchargeInfo { get { return m_overchargeInfo; } set { m_overchargeInfo = value; } }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        FindClosestEnemy();

    }

    private void FixedUpdate()
    {
        rigidBody.velocity = (transform.forward * speed);

        if(delayHoming > timer)
        {
            timer += Time.deltaTime;
        }
        else
        {
            RotateTowardsTarget();
        }

    }

    /// <summary>
    /// Searches all enemies in scene and find the closest enemy
    /// </summary>
    private void FindClosestEnemy()
    {
        float minimumDistance = Mathf.Infinity;

        

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies == null)
            return;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                target = enemy.transform;
            }
        }
    }

    /// <summary>
    /// Rotating towards closest enemy
    /// </summary>
    private void RotateTowardsTarget()
    {

        if (enemies == null)
        {
            Destroy(gameObject);
            return;
        }
            

        var targetRotation = Quaternion.LookRotation(target.position - transform.position);

        rigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed));
    }

    private void OnTriggerEnter(Collider other)
    {
        // pierces thru an infinite number of enemies
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<HealthComponent>().ApplyDamage(m_overchargeInfo);
        }
        // destroys arrow when not hitting with an enemy
        else
        {
            Destroy(gameObject);
        }

    }
}
