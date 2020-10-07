using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_PlayerController : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject rockPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Abilities();
    }

    private void Abilities()
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
    }

    private void AbilityOne()
    {
        Instantiate(rockPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
    }
}
