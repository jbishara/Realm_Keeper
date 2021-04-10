using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LD_ROK_Audio : MonoBehaviour
{
    public GameObject audioManager;
    // Start is called before the first frame update
    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager");
    }

    void Start()
    {
        audioManager.GetComponent<LD_AudioManager>().Play("Realm_of_Keepers_BG");
        audioManager.GetComponent<LD_AudioManager>().Play("RK_Ambience_1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
