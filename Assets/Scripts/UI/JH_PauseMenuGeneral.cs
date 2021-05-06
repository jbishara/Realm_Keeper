﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JH_PauseMenuGeneral : MonoBehaviour
{
    public bool GameIsPaused = false;
    public bool playerHaveWon = false;

    public GameObject pauseMenuUI;                  // Connects to PauseMenuUI
    public GameObject settingsMenuUI;               // Connects to SettingsUI
    public GameObject deathMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        GameIsPaused = false;                       // Set bool to false at start of scene
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && playerHaveWon == false)       // When "Esc" is pressed game will pause, if already paused game will unpause
        {
            if (GameIsPaused == false)
            {
                Paused();
            }
            else
            {
                Resume();
            }
        }        
    }

    public void Paused()
    {
        pauseMenuUI.SetActive(true);                // Turns ON Pause Menu UI

        Cursor.lockState = CursorLockMode.None;     // Unlocks cursor movement
        Cursor.visible = true;                      // Makes cursor visible

        Time.timeScale = 0f;                        // Pauses the game
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);               // Turns OFF Pause Menu UI
        settingsMenuUI.SetActive(false);             // Turns OFF Settings Menu UI

        Cursor.lockState = CursorLockMode.Locked;   // Locks cursor movement
        Cursor.visible = false;                     // Makes cursor invisible

        Time.timeScale = 1f;                        // Resumes game at correct speed
        GameIsPaused = false;
    }

    public void SettingsMenuOpen()
    {
        pauseMenuUI.SetActive(false);               // Turns OFF Pause Menu UI
        settingsMenuUI.SetActive(true);             // Turns On Settings Menu UI
    }

    public void SettingsMenuClose()
    {
        settingsMenuUI.SetActive(false);            // Turns Off Settings Menu UI
        pauseMenuUI.SetActive(true);                // Turns ON Pause Menu UI
    }

    public void ExitToMenu()
    {
        Destroy(Master_Script.instance.player);
        Master_Script.instance.player = null;
        SceneManager.LoadScene("Main_Menu");        // LOADS Main Menu Scene
        Debug.Log("Quit to Main Menu");
    }

    public void ReturnToMainDeath()
    {
        Time.timeScale = 1;
        Destroy(Master_Script.instance.player);
        Master_Script.instance.player = null;
        SceneManager.LoadScene("Main_Menu");
    }
}
