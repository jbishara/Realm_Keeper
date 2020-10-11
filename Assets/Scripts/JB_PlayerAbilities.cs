using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_PlayerAbilities : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Transform projectileTargetLocation;
    [SerializeField] private Transform meleeAttackArea;
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject rangeAttackPrefab;

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
            // range attack
            BasicRangeAttack();
        }

    }

    private void BasicMeleeAttack()
    {
        // melee attack
        Physics.OverlapSphere(meleeAttackArea.position, 6f);
        
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
