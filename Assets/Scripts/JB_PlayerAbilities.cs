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

    [Header("Prefabs for abilities to spawn")]
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject rangeAttackPrefab;
    //[SerializeField] private CharacterStats characterStats;

    private float delayTimerAttack;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        //normalAttack.damage = characterStats.attackDamage;
    }

    // Update is called once per frame
    void Update()
    {

        // seconds timer
        if(timer >= 0)
            timer -= Time.deltaTime;

        PlayerInput();
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // ability one
            RockThrow();
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

      
        

    }

    private void BasicMeleeAttack()
    {
        float attackSpeed = GetComponent<JB_PlayerStats>().attackSpeed;

        
        if (timer <= 0)
        {
            // used to create a delay between attacks
            timer = attackSpeed;


            //TODO - add in animation


            // melee attack
            var colInfo = Physics.OverlapSphere(meleeAttackArea.position, 6f);

            if (colInfo != null)
            {
                foreach (Collider col in colInfo)
                {
                    if (col.gameObject.GetComponent<HealthComponent>())
                    {
                        col.gameObject.GetComponent<HealthComponent>().ApplyDamage(normalAttack);
                    }
                }
            }
        }

            
        
        // Collider2D colInfo = Physics2D.OverlapCircle(transform.position, attackRange, attackMask);


    }

    private void BasicRangeAttack()
    {
        float attackSpeed = GetComponent<JB_PlayerStats>().attackSpeed;

        if(timer <= 0)
        {
            timer = attackSpeed;

            // range attack
            Instantiate(rangeAttackPrefab, meleeAttackArea.position, meleeAttackArea.rotation);
        }
        
    }

    private void RockThrow()
    {
        GameObject obj = Instantiate(rockPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        obj.GetComponent<JB_RockProjectile>().targetLocation = projectileTargetLocation;
    }

    private void ArcaneShoot()
    {

    }
}
