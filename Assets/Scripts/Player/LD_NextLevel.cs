using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LD_NextLevel : MonoBehaviour
{
    public GameObject UI;

    void OnCollisionEnter(Collision collision)
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index >= 4)
        {
            GameObject.Find("PlayerCharacter/Canvas").GetComponent<JH_PauseMenuGeneral>().playerHaveWon = true;
            Time.timeScale = 0;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
