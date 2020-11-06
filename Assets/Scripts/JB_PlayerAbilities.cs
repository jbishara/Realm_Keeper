using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { Normal, Fire, Poison }

public class JB_PlayerAbilities : MonoBehaviour
{
    [SerializeField] private DamageInfo normalAttack;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Transform projectileTargetLocation;
    [SerializeField] private Transform meleeAttackArea;
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject rangeAttackPrefab;

    private float delayTimerAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // ability one
            AbilityOne();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // ability two
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // ability three
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // melee attack
            BasicMeleeAttack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("right mouse click ...");
            // range attack
            BasicRangeAttack();
        }

        if (delayTimerAttack <= 0f)
        {
            delayTimerAttack = 0.75f;
            Debug.Log("right mouse click");

            
        }
        else
        {
            delayTimerAttack -= Time.deltaTime;
        }
        

    }

    private void BasicMeleeAttack()
    {
        // melee attack
        var colInfo = Physics.OverlapSphere(meleeAttackArea.position, 6f);

        if(colInfo != null)
        {
            foreach(Collider col in colInfo)
            {
                if (col.gameObject.GetComponent<HealthComponent>())
                {
                    col.gameObject.GetComponent<HealthComponent>().ApplyDamage(normalAttack, DamageType.Normal);
                }
            }
        }
        // Collider2D colInfo = Physics2D.OverlapCircle(transform.position, attackRange, attackMask);


    }

    private void BasicRangeAttack()
    {
        // range attack
        Instantiate(rangeAttackPrefab, meleeAttackArea.position, meleeAttackArea.rotation);
    }

    private void AbilityOne()
    {
        GameObject obj = Instantiate(rockPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        obj.GetComponent<JB_RockProjectile>().targetLocation = projectileTargetLocation;
    }
}
