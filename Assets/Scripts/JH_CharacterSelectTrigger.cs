using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_CharacterSelectTrigger : MonoBehaviour
{
    //public GameObject player;
    public GameObject playercanvas;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player");
        //playercanvas = player.GetComponentInChildren<Canvas>;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playercanvas.GetComponent<JH_PauseMenuHubWorld>().CharacterSelectOpen();
        }        
    }
}
// You Can ignore this