using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LD_NextLevel : MonoBehaviour
{
   
    public Master_Script master;

    public GameObject UI;

    private bool playerWon = false;

    //private bool gameIsWon;
    private void Awake()
    {
        master = GameObject.Find("GameMaster").GetComponent<Master_Script>();
        UI = master.winUI;
    }

    void OnCollisionEnter(Collision collision)
    {
        
        //if (indexs == 4)
        //{
            
            
        //}
        //else
        //{
            
        //}
        


    }

    private void OnTriggerEnter(Collider other)
    {
        switch (master.stageLevel)
        {
            case 4:
                UI.SetActive(true);
                GameObject.Find("PlayerCharacter/Canvas").GetComponent<JH_PauseMenuGeneral>().playerHaveWon = true;
                playerWon = true;
                break;
            default:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
    }
}
