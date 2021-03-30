using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LD_NextLevel : MonoBehaviour
{
    public string finalLevel;
    public GameObject winMenu;

    void OnCollisionEnter(Collision collision)
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == finalLevel)
        {
            Time.timeScale = 0;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
