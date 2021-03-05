using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyHandler : MonoBehaviour
{

    /// <summary>
    /// Closest distance to player a spawner can be when spawning
    /// </summary>
    [SerializeField] public float ClosestDistance2P = 4f;

    /// <summary>
    /// Maximum allowed enemies that is active simultaneously
    /// </summary>
    [SerializeField] public int MaxActiveEnemies = 5;

    [SerializeField] public Transform PlayerPosition;

    [SerializeField] public int EnemiesToKillBeforeBoss = 50;

    /// <summary>
    /// Boss wave
    /// </summary>
    [SerializeField] public AiEnemyTypes[] BossWave;
    
    /// <summary>
    /// Enemies killed
    /// </summary>
    public int EnemiesKilled { get; set; } = 0;

    /// <summary>
    /// How many enemies that is alive
    /// </summary>
    private int EnemiesAlive => GameObject.FindGameObjectsWithTag("Enemy").Length;

    /// <summary>
    /// List with all the available spawner on this level
    /// </summary>
    private readonly List<GameObject> enemySpawn = new List<GameObject>();

    /// <summary>
    /// Indicates if the bosswave has been spawned
    /// </summary>
    private bool bossSpawned = false;

    /// <summary>
    /// 
    /// </summary>
    public void EnemyKilled()
    {

        EnemiesKilled++;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemySpawn.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawner"));


    }

    // Update is called once per frame
    void Update()
    {

        if (EnemiesKilled < EnemiesToKillBeforeBoss)
        {
            // Get all active enemies, and if there is not enough enemies alive
            if (EnemiesAlive >= MaxActiveEnemies) return;


            List<GameObject> canSpawnSpawners = enemySpawn.FindAll(o =>
                o.GetComponent<EM_EnemySpawner>()
                    .CanSpawn(PlayerPosition, ClosestDistance2P));

            canSpawnSpawners[Random.Range(0, canSpawnSpawners.Count - 1)].GetComponent<EM_EnemySpawner>()
                .SpawnEnemy();


        }
        else if (EnemiesAlive == 0 && !bossSpawned)
        {






        }
        else if (EnemiesAlive == 0 && bossSpawned)
        {
            List<GameObject> bossSpawners = enemySpawn.FindAll(o =>
                o.GetComponent<EM_EnemySpawner>().CanSpawn(PlayerPosition, ClosestDistance2P));

            foreach (var d in bossSpawners.OrderByDescending(o => o.GetComponent<EM_EnemySpawner>().
                DistanceToPlayer(PlayerPosition)))
            {
                Console.WriteLine(d.GetComponent<EM_EnemySpawner>().DistanceToPlayer(PlayerPosition));
            }

           


            //todo: Spawn Portal behind the player
        }
    }
}