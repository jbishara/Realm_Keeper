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
    public string lastlevel;

    public void Awake()
    {
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

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == (hubLevel))
        {
            // play audio
            audioManager.GetComponent<LD_AudioManager>().Play("Realm_of_Keepers_BG");
            audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_1");
            // audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_2");
            //audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_3");
            //audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_4");
            //audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_5");
            //audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_6");
        }
        else if (scene.name == (firstLevel))
        {
            audioManager.GetComponent<LD_AudioManager>().Play("Underground_Caven_BG");
        }
        else if (scene.name == (lastlevel))
        {
            audioManager.GetComponent<LD_AudioManager>().Play("Forgotten_Plains_BG");
        }
    }
         

// Start is called before the first frame update
void Start()
    {
        audioManager = GameObject.Find("AudioManager");
        lastlevel = nextLevel.GetComponent<LD_NextLevel>().finalLevel;

    }

    // Update is called once per frame
    void Update()
    {
        // checks what scene is currently playing, and plays audio based on that

    }
}
