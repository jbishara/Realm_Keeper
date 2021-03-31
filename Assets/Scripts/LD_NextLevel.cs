using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LD_NextLevel : MonoBehaviour
{
    public string finalLevel;
    public GameObject canvas;
    public Master_Script master;
    
    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        canvas.GetComponentInChildren<Transform>(true);
        master = GameObject.Find("EGO Game Master").GetComponent<Master_Script>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == finalLevel)
        {
            master.winUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
