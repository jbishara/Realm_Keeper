using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LD_NextLevel : MonoBehaviour
{
    // strings of scenenames

    public string finalLevel;

   
    public Master_Script master;

    private bool gameIsWon;
    
    private void Start()
    {
        master = GameObject.Find("GameMaster").GetComponent<Master_Script>();
        gameIsWon = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == finalLevel)
        {
            master.winUI.SetActive(true);
            gameIsWon = true;
            Time.timeScale = 0;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
