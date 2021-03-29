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
    [SerializeField] public float ClosestDistance2P = 12;

    /// <summary>
    /// Maximum allowed enemies that is active simultaneously
    /// </summary>
    [SerializeField] public int MaxActiveEnemies = 5;

    [SerializeField] public Transform PlayerPosition;

    [SerializeField] public int EnemiesToKillBeforeBoss = 20;

    [SerializeField] public List<GameObject> ItemPrefabs;

    /// <summary>
    /// Boss wave
    /// </summary>
    [SerializeField] public AiEnemyTypes[] BossWave;

    /// <summary>
    /// This kind of boss that would spawn in this zone
    /// </summary>
    public GameObject ThisZoneBoss;

    /// <summary>
    /// this is spawner for boss
    /// </summary>
    public GameObject BossSpawner;

    /// <summary>
    /// This is portal for the level
    /// </summary>
    public GameObject portal;

    /// <summary>
    /// Portal spawnpoint for level
    /// </summary>
    public GameObject portalspawnpoint;

    [SerializeField] public int SpawnDelayMs = 3000;

    public TimeSpan TimeBetweenSpawn;
    private DateTime lastSpawn;

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
        Debug.Log("I just died");
    }

    // Start is called before the first frame update
    void Start()
    {
        enemySpawn.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawner"));
        lastSpawn = DateTime.Now;
        TimeBetweenSpawn = TimeSpan.FromMilliseconds(SpawnDelayMs);
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemiesKilled < EnemiesToKillBeforeBoss)
        {
            // Get all active enemies, and if there is not enough enemies alive
            if (EnemiesAlive >= MaxActiveEnemies || lastSpawn > DateTime.Now) return;

            List<GameObject> canSpawnSpawners = enemySpawn.FindAll(o => 
                o.GetComponent<EM_EnemySpawner>().CanSpawn(PlayerPosition, ClosestDistance2P));
            
            if(canSpawnSpawners.Count < 1) return;

            GameObject selectedSpawner = canSpawnSpawners[Random.Range(0, canSpawnSpawners.Count - 1)];

            
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
            {
                GameObject enemyLoop = GameObject.FindGameObjectsWithTag("Enemy")[i];

                // Disallow spawn if an enemy is to close to the spawner
                if (Vector3.Distance(enemyLoop.transform.position, selectedSpawner.transform.position) < 2) return;
            }

            lastSpawn = DateTime.Now + TimeBetweenSpawn;
            selectedSpawner.GetComponent<EM_EnemySpawner>().SpawnEnemy();

            Debug.LogWarning("Spawned");

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

        if (EnemiesKilled == 10 && !bossSpawned)
        {
            bossSpawned = true;
            Instantiate(ThisZoneBoss, BossSpawner.transform.position, Quaternion.identity);
        }
    }
}