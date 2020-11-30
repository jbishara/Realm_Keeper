﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JH_PauseMenuHubWorld : MonoBehaviour
{
    public bool GameIsPaused = false;
    public bool MenuOpen = false;

    public GameObject pauseMenuPanel;               // Connects to PauseMenuPanel
    public GameObject pauseMenuUI;                  // Connects to PauseMenuUI
    public GameObject settingsMenuUI;               // Connects to SettingsUI
    public GameObject characterSelectMenuUI;        // Connects to CharacterSelectMenuUI
    public GameObject loadoutMenuUI;                // Connects to LoadoutMenuUI
    public GameObject logbookMenuUI;                // Connects to LogbookMenuUI

    // Start is called before the first frame update
    void Start()
    {
        GameIsPaused = false;                       // Set bool to false at start of scene
        MenuOpen = false;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && MenuOpen == false)       // When "Esc" is pressed game will pause, if already paused game will unpause  
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
        else
        {
            DisableMenus();                         // Disables all Menus
        }
    }

    public void Paused()
    {
        pauseMenuPanel.SetActive(true);             // Turns ON Pause Menu Panel

        Cursor.lockState = CursorLockMode.None;     // Unlocks cursor movement
        Cursor.visible = true;                      // Makes cursor visible

        Time.timeScale = 0f;                        // Pauses the game
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);            // Turns OFF Pause Menu Panel

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
        SceneManager.LoadScene("Main_Menu");                 // LOADS Main Menu Scene
        Debug.Log("Quit to Main Menu");
    }

    void DisableMenus()                             // Disables all other menus
    {
        MenuOpen = false;
        characterSelectMenuUI.SetActive(false);
        loadoutMenuUI.SetActive(false);
        logbookMenuUI.SetActive(false);
    }
}
