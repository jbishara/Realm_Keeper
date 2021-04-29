using System.Collections;
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

        // don't destroy this object on start or loading new scene
        DontDestroyOnLoad(gameObject);
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
    }
}
