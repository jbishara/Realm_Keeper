using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JH_MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;                       // Connects to MainMenuUI
    public GameObject logbookMenuUI;                    // Connects to LogbookMenuUI
    public GameObject settingsMenuUI;                   // Connects to SettingsMenuUI
    public GameObject profileDropdownUI;                // Connects to ProfileDropdown

    public void PlayGame() 
    {
        SceneManager.LoadScene("Tutorial");             // LOADS Tutorial scene to teach the user what to do
    }

    public void LogbookMenuOpen()
    {
        mainMenuUI.SetActive(false);                    // Turns OFF Main Menu UI
        logbookMenuUI.SetActive(true);                  // Turns ON Logbook Menu UI
    }

    public void LogbookMenuClose()
    {
        logbookMenuUI.SetActive(false);                 // Turns OFF Logbook Menu UI
        mainMenuUI.SetActive(true);                     // Turns ON Main Menu UI
    }

    public void SettingsMenuOpen()
    {
        mainMenuUI.SetActive(false);                    // Turns OFF Main Menu UI
        settingsMenuUI.SetActive(true);                 // Turns ON Settings Menu UI
    }

    public void SettingsMenuClose()
    {
        settingsMenuUI.SetActive(false);                // Turns OFF Settings Menu UI
        mainMenuUI.SetActive(true);                     // Turns ON Main Menu UI
    }

    public void CreditsScene()
    {
        //SceneManager.LoadScene("");                     // LOADS Credit scene (Credit scene currently does not exist)
        Debug.Log("Goes to Credit Sene");
    }

    public void QuitGame()
    {
        Application.Quit();                             // Quit application fuction
        Debug.Log("Quit Game");
    }

    //public void PlayerProfile()                         // Players can change profiles for different save data
    //{

    //}
}
