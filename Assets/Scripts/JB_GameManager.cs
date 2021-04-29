using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_GameManager : MonoBehaviour
{
    #region Singleton

    public static JB_GameManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
    public GameObject audioManager;
    public GameObject enemyHandler;

}
