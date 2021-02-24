using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class EM_EnemySpawner : MonoBehaviour
{
    [SerializeField] public Transform Position;

    [SerializeField] public AiEnemyTypes EnemyType;

    [SerializeField] public Transform PlayerPosition;

    [SerializeField] public float ClosestDistance2P = 4f;



    /// <summary>
    /// Checks if an Enemy can be spawned
    /// </summary>
    /// <returns></returns>
    public bool CanSpawn(float playerVisionRange)
    {
        // Check dist to spawner from player
        if (Vector3.Distance(PlayerPosition.position, Position.position) > ClosestDistance2P)
        {
            // Raycast to make sure that the spawner cannot be seen by the player
            Physics.Raycast(PlayerPosition.position, PlayerPosition.position - Position.position,
                out RaycastHit info, playerVisionRange);

            if (info.collider.tag != null && info.collider.tag == "EnemySpawner")
            {
                return true;
            }
        }
        return false;
    }

    public void SpawnEnemy()
    {

    }


}

