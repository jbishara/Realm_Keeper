using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;


public class EM_EnemySpawner : MonoBehaviour
{

    [SerializeField] public GameObject[] SpawnerCanSpawn;

    /// <summary>
    /// Checks if an Enemy can be spawned
    /// </summary>
    /// <param name="playerPosition"></param>
    /// <param name="closestDistanceToPlayer"></param>
    /// <returns>True if all checks are OK</returns>
    public bool CanSpawn(Transform playerPosition, float closestDistanceToPlayer)
    {
        // Check dist to spawner from player
        if (Vector3.Distance(playerPosition.position, transform.position) > closestDistanceToPlayer)
        {
            Ray rayFromCamera = new Ray(transform.position,
                Camera.main.transform.position - transform.position);

            // Ray-cast to make sure that the spawner cannot be seen by the player
            Physics.Raycast(rayFromCamera, out RaycastHit info);
            
            return info.transform != playerPosition;
        }
        return false;
    }

    public void SpawnEnemy()
    {
        if (SpawnerCanSpawn.Length < 1)
        {
            Debug.LogWarning("No Enemies specified in a spawner!");
            return;
        }
        int sp = Random.Range(0, SpawnerCanSpawn.Length);

        Instantiate(SpawnerCanSpawn[sp], gameObject.transform.position, Quaternion.identity);

    }

    public float DistanceToPlayer(Transform playerPos)
    {
        return Vector3.Distance(transform.position, playerPos.position);
    }
}

