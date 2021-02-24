using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyHandler : MonoBehaviour
{
    public List<GameObject> EnemySpawn = new List<GameObject>();

    [SerializeField] public float PlayerVisionRange = 5;

    [SerializeField] public int MaxActiveEnemies = 5;

    public int EnemiesKilled = 0;



    public void EnemyKilled()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawn.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawner"));



    }

    // Update is called once per frame
    void Update()
    {

        // Get all active enemies, and if there is not enough enemies alive
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < MaxActiveEnemies)
        {
            List<GameObject> canSpawnSpawners = EnemySpawn.FindAll(o => o.GetComponent<EM_EnemySpawner>().CanSpawn(PlayerVisionRange));

            canSpawnSpawners[Random.Range(0, canSpawnSpawners.Count - 1)].GetComponent<EM_EnemySpawner>().SpawnEnemy();
        }



    }
}