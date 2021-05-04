using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JH_CreditMenuReturn : MonoBehaviour
{
    /// <summary>
    /// 
    ///  Yeah this is a mess but I was trying to figure out how to get the scene to go back tot he main menu automatically after the credits have rolled.
    ///  I honestly don't know enough about this stuff but I was trying so witness the mess of my failures and ym lose to an animation clip.
    ///  
    ///  Player can get back to the Main Menu so it wasn't a requirement but I felt that it would just feel better for the players to get sent back automatically.
    ///  
    ///  From the only one who could bugger it up this bad,
    ///  Jacquie
    ///  
    ///  Ps. It's 2am and I ain't built for this.
    /// 
    /// </summary>

    private Animation creditScrollAnimation;            

    //private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        creditScrollAnimation = gameObject.GetComponentInChildren<Animation>();

        //creditScroll = gameObject.GetComponent<Animation>();
        //coroutine = CreditTime(30.0f);
        //StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu();
        }

        //if (creditScrollAnimation.isPlaying)
        //{
        //    return;
        //    Debug.Log("What is happening?");
        //}
        //else
        //{
        //    ReturnToMenu();
        //}

        //if (isPlaying == true)
        //{
        //    return;
        //    Debug.Log("What is happening?");
        //}
        //else
        //{
        //    ReturnToMenu();
        //}
    }

    //private IEnumerator CreditTime(float waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    ReturnToMenu();
    //}

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
