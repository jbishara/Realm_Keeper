using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LD_NextLevel : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        //gameObject.transform.position = falloutSpawner;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
