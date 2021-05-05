using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LD_SetCharacter : MonoBehaviour
{
    //public string whatCharacter;

    //public void Tansea()
    //{
    //    Master_Script.instance.characterName = ("Tansea");
    //    Master_Script.instance.hasSelectedCharacter = true;
    //}

    //public void Zylar()
    //{
    //    Master_Script.instance.characterName = ("Zylar");
    //    Master_Script.instance.hasSelectedCharacter = true;
    //}

    //public void Freya()
    //{
    //    Master_Script.instance.characterName = ("Freya");
    //    Master_Script.instance.hasSelectedCharacter = true;
    //}

    public void CharacterChosen(string name)
    {
        Master_Script.instance.characterName = name;
        Master_Script.instance.hasSelectedCharacter = true;
    }

    public void LoadRealmOfKeepers()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Master_Script.instance.player = null;
        SceneManager.LoadScene("Realm_of_keepers");
        Time.timeScale = 1;
    }
}
