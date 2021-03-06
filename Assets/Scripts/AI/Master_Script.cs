﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Master_Script : MonoBehaviour
{
    // managers and the winUI
    [Header("These all need references from in scene game objects")]
    public GameObject audioManager;
    public GameObject player;
    public GameObject enemyHandler;
    public GameObject portal;
    public GameObject portalSpawnPoint;
    
    // character values
    public string characterName;
    public GameObject characterSpawn;
    public Transform thisIsPlayerTransform;
    public GameObject tanseaPrefab;
    public GameObject zylarPrefab;
    public GameObject freyaPrefab;
    public bool hasSelectedCharacter;
    public bool playerHasSpawn;

    private GameObject spawnedObj;

    public static Master_Script instance;
    
    public void Awake()
    {
        audioManager = GameObject.Find("AudioManager");
        // if there is no refences to this gameobject set this object to it
        if (instance == null)
            instance = this;
        // else destroy the new version
        else
        {
            Destroy(gameObject);
            return;
        }

        // don't destroy these objects on start or loading new scene
        DontDestroyOnLoad(gameObject);
        if(player != null)
        {
            DontDestroyOnLoad(player);
        }
    }
    private void Start()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        switch (index)
        {
            case 0:
                // if start playing on menu it won't stop playing
                //audioManager.GetComponent<LD_AudioManager>().Play("Realm_of_Keepers_BG");
                break;
            case 1:
                break;
            case 2:
                audioManager.GetComponent<LD_AudioManager>().Play("Realm_of_Keepers_BG");
                audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_ROK01");
                audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_ROK02");
                break;
            case 3:
                audioManager.GetComponent<LD_AudioManager>().Play("Underground_Caven_BG");
                audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_UC01");
                audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_UC02");
                break;
            case 4:
                audioManager.GetComponent<LD_AudioManager>().Play("Forgotten_Plains_BG");
                audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_IF01");
                audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_IF02");
                break;
            default:
                Debug.LogWarning("Invalid build index" + index);
                break;
        }

        SceneManager.sceneLoaded += LevelLoaded;

    }

    private void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void spawnCharacter()
    {
        //characterSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn");

        if (playerHasSpawn)
        {
            Debug.Log("Player has spawn");
            return;
        }
        

        if (characterName == "Tansea")
        {
            spawnedObj = Instantiate(tanseaPrefab, characterSpawn.transform.position, characterSpawn.transform.rotation);
            playerHasSpawn = true;
            Debug.Log("Creating Tansea");
        }
        else if (characterName == ("Zylar"))
        {
            spawnedObj = Instantiate(zylarPrefab, characterSpawn.transform.position, characterSpawn.transform.rotation);
            playerHasSpawn = true;
            Debug.Log("Creating Zylar");
        }
        else if (characterName == ("Freya"))
        {
            spawnedObj = Instantiate(freyaPrefab, characterSpawn.transform.position, characterSpawn.transform.rotation);
            playerHasSpawn = true;
            Debug.Log("Creating Freya");
        }

        //player = GameObject.FindGameObjectWithTag("Player");
        player = spawnedObj;
    }

    void LevelLoaded(Scene current, LoadSceneMode next)
    {
        if(current.buildIndex != 0)
        {
            characterSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn");

            spawnCharacter();
        }
        else
        {
            playerHasSpawn = false;
            //player.SetActive(false);
        }
    }
}
