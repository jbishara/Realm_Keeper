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
    // on collision of gameobject
    void OnCollisionEnter(Collision collision)
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == 4 && collision.gameObject.tag == "Player")
        {
            //GameObject.Find("PlayerCharacter/Canvas").GetComponent<JH_PauseMenuGeneral>().playerHaveWon = true;

            Master_Script.instance.player.GetComponentInChildren<JH_PauseMenuGeneral>().playerHaveWon = true;
            
            Time.timeScale = 0;
        }
        // in tutorial level takes you back to main menu and destorys the player & sets value to null
        else if (index == 1 && collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Main_Menu");
            Destroy(Master_Script.instance.player);
            Master_Script.instance.player = null;
            Cursor.lockState = CursorLockMode.None;     // Unlocks cursor movement
            Cursor.visible = true;                      // shows cursor
        }
        else if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    public void MovePortal()
    {
        transform.position = portalSpawnPoint.position;
    }
}
