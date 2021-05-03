using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_LogbookMenu : MonoBehaviour
{
    public GameObject itemTab;
    public GameObject enemyTab;
    public GameObject realmKeeperTab;
    public GameObject challengesTab;
    

    public void ItemButton()
    {
        itemTab.SetActive(true);

        enemyTab.SetActive(false);
        realmKeeperTab.SetActive(false);
        challengesTab.SetActive(false);
    }

    public void EnemyButton()
    {
        enemyTab.SetActive(true);

        itemTab.SetActive(false);
        realmKeeperTab.SetActive(false);
        challengesTab.SetActive(false);
    }

    public void RealmKeeperButton()
    {
        realmKeeperTab.SetActive(true);

        itemTab.SetActive(false); 
        enemyTab.SetActive(false);
        challengesTab.SetActive(false);
    }

    public void ChallengesButton()
    {
        challengesTab.SetActive(true);

        itemTab.SetActive(false); 
        enemyTab.SetActive(false);
        realmKeeperTab.SetActive(false);
    }
}
