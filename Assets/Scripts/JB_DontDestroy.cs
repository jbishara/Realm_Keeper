using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JB_DontDestroy : MonoBehaviour
{
    private bool isOriginal = false;
    private GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        objects = GameObject.FindGameObjectsWithTag(this.tag);

        if(objects.Length == 1)
        {
            DontDestroyOnLoad(this.gameObject);
            isOriginal = true;
        }

        if (!isOriginal)
        {
            Destroy(this.gameObject);
        }

        SceneManager.activeSceneChanged += CheckScene;
    }

    private void CheckScene(Scene current, Scene next)
    {
        if (current.buildIndex == 0)
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
