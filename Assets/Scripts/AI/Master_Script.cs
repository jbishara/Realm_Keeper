using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Master_Script : MonoBehaviour
{
    // managers and the winUI
    public GameObject winUI;
    public GameObject audioManager;
    public GameObject nextLevel;
    public static Master_Script instance;
    // strings
    public string hubLevel;
    public string firstLevel;
    public string lastLevel;

    public int buildindex;

    public bool thisIsHubLevel;
    private bool secoundLevel = false;
    
    public void Awake()
    {
        audioManager = GameObject.Find("AudioManager");

        
        lastLevel = nextLevel.GetComponent<LD_NextLevel>().finalLevel;
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

    //private void Start()
    //{
    //    Scene currentScene = SceneManager.GetActiveScene();
    //    buildindex = currentScene.buildIndex;
    //    switch (buildindex)
    //    {
    //        case 0:
    //            break;
    //        case 1:
    //            audioManager.GetComponent<LD_AudioManager>().Play("Realm_of_Keepers_BG");
    //            break;
    //        case 2:
    //            audioManager.GetComponent<LD_AudioManager>().Play("Realm_of_Keepers_BG");
    //            audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_1");

    //            break;
    //        case 3:
    //            break;
    //        case 4:
    //            break;
    //    }


    //}
    private void Update()
    {
        if (buildindex == 0)
        {

        }
        if (buildindex == 1)
        {
           // secoundLevel = true;
           // audioManager.GetComponent<LD_AudioManager>().Play("Realm_of_Keepers_BG");
            //audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_1");
        }
        if (buildindex == 2)
        {
            //thisIsHubLevel = true;
            //audioManager.GetComponent<LD_AudioManager>().Play("Realm_of_Keepers_BG");
           // audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_1");
        }
        if (buildindex == 3)
        {
            //audioManager.GetComponent<LD_AudioManager>().Play("Underground_Caven_BG");
        }
        if (buildindex == 4)
        {
            winUI = GameObject.Find("PlayerCharacter/Canvas/WinUI Variant 1");
           // audioManager.GetComponent<LD_AudioManager>().Play("Forgotten_Plains_BG");
        }
        if (secoundLevel == true)
        {
            //audioManager.GetComponent<LD_AudioManager>().Play("Forgotten_Plains_BG");
        }
    }
}
