using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LD_CurrentObjective : MonoBehaviour
{
    // storing values from enemyhanlder
    private int enemieskilled_obj;
    private int enemieskilledUntilBoss_obj;
    private bool isBossDead = false;

    // text variables
    private Text objectinfoText;
    private Text currenttask;

    public GameObject information_UI;
    public GameObject Objective_UI;
    public GameObject bossUI;
    // gameobject refences so we can grab the enemy handler script easliy
    public GameObject enemyHandler;
    // inter that goes into a switch to change the task to what we want it to be, based on task
    public int amountOfObjectivesCompleted;
    // Start is called before the first frame update
    private void Awake()
    {
        enemyHandler = GameObject.Find("EnemyHandler");
        amountOfObjectivesCompleted = 1;
    }

    private void Start()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == 2)
        {
            amountOfObjectivesCompleted = 3;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Nexttask();
    }

    void Nexttask()
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        if (bossUI != null)
        {
            bossUI.SetActive(false);
        }
        if (index == 2)
        {
            amountOfObjectivesCompleted = 3;
        }
        switch ((int)amountOfObjectivesCompleted)
        {
            case 1:
                // updates the variables that we grab from the enemy handler script
                if (enemyHandler != null)
                {
                    enemieskilled_obj = enemyHandler.GetComponent<EnemyHandler>().EnemiesKilled;
                    enemieskilledUntilBoss_obj = enemyHandler.GetComponent<EnemyHandler>().EnemiesToKillBeforeBoss;

                    // updates our objective UI information text and main text
                    currenttask = Objective_UI.GetComponent<Text>();
                    objectinfoText = information_UI.GetComponent<Text>();

                    // changes the text for our UI elements
                    objectinfoText.text = "Slay foes";
                    currenttask.text = enemieskilled_obj + " out of " + enemieskilledUntilBoss_obj;
                    if (enemieskilled_obj >= 10)
                    {
                        amountOfObjectivesCompleted = 2;
                    }
                }
                // if you're in hub world it would tell you go to portal insted of slaying enemy's
                else amountOfObjectivesCompleted = 3;

                break;
            
            case 2:
                currenttask = Objective_UI.GetComponent<Text>();
                objectinfoText = information_UI.GetComponent<Text>();
                // changes text to this
                objectinfoText.text = "Slay boss";
                currenttask.text = "Find boss";
                
                bossUI.SetActive(true);

                // bools sets to true
                isBossDead = enemyHandler.GetComponent<EnemyHandler>().bossDead;

                if (isBossDead == true)
                {
                    bossUI.SetActive(false);
                    amountOfObjectivesCompleted = 3;
                }
                break;

            case 3:
                currenttask = Objective_UI.GetComponent<Text>();
                objectinfoText = information_UI.GetComponent<Text>();
                objectinfoText.text = "Portal";
                currenttask.text = "Go through the portal";
                break;

        }
    }
}
