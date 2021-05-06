using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LD_NextLevel : MonoBehaviour
{
    public GameObject UI;
    public GameObject CharacterSelection;
    public Transform portalSpawnPoint;
    

    void Start()
    {
        DeathScriptEG.PortalSpawn += MovePortal;
    }

    void OnCollisionEnter(Collision collision)
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == 4 && collision.gameObject.tag == "Player")
        {
            //GameObject.Find("PlayerCharacter/Canvas").GetComponent<JH_PauseMenuGeneral>().playerHaveWon = true;

            Master_Script.instance.player.GetComponentInChildren<JH_PauseMenuGeneral>().playerHaveWon = true;
            
            Time.timeScale = 0;
        }
        else if (index == 1 && collision.gameObject.tag == "Player")
        {
            // activates character selection screen after touching the portal in tutorial level
            CharacterSelection.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    public void MovePortal()
    {
        transform.position = portalSpawnPoint.position;
    }
}
