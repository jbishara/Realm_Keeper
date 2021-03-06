﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScriptEG : MonoBehaviour
{
    /// <summary>
    /// attach this script to any object that has health component for example
    /// </summary>
    private HealthComponent hScript;

    public GameObject deathMenu;
    public GameObject itemDrop;
    public GameObject enemyHandler;

    private GameObject portalObj;

    // Event that alerts the player they have crit
    public delegate void SpawnPortal();
    public static event SpawnPortal PortalSpawn;

    // Start is called before the first frame update
    void Start()
    {
        enemyHandler = GameObject.FindGameObjectWithTag("EnemyHandler");
        // we are referencing the health component script that is attached to this script as well
        hScript = GetComponent<HealthComponent>();

        // this listens to the ondeath event attached to the health component on this object, so when the event is called u do the stuff u want below
        hScript.OnDeath += ThisObjectDied;

        if (gameObject.name.Contains("BOSS"))
        {
            portalObj = GameObject.Find("Portal");
        }

        SceneManager.sceneLoaded += CheckReturnMenu;
    }

    private void CheckReturnMenu(Scene current, LoadSceneMode next)
    {
        // buildindex 0 is main menu
        if(current.buildIndex == 0)
        {
            Debug.Log("FUNCTION REACHED!!!");
        }
    }

    private void ThisObjectDied(HealthComponent self)
    {
        // do stuff when this object dies
        if (gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            deathMenu.SetActive(true);
        }
        else
        {
            // 10% chance for item to drop
            float rand = Random.Range(0f, 1f);
            enemyHandler.GetComponent<EnemyHandler>().EnemiesKilled++;
            if (gameObject.name.Contains("BOSS"))
            {
                Debug.Log("does this script get reached");
                Instantiate(itemDrop, gameObject.transform.position, Quaternion.identity);
                Debug.Log("Item drop!");
                portalObj.GetComponent<LD_NextLevel>().MovePortal();
                enemyHandler.GetComponent<EnemyHandler>().bossDead = true;
            }
            else if(rand <= 0.1f)
            {
                // may need add the enemy slay ++
                Instantiate(itemDrop, gameObject.transform.position, Quaternion.identity);
                
                Debug.Log("Item drop!");
            }

            
        }
    }
}
