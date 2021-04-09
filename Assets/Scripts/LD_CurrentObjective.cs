using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    // gameobject refences so we can grab the enemy handler script easliy
    public GameObject enemyHandler;

    // inter that goes into a switch to change the task to what we want it to be, based on task
    public int amountOfObjectivesCompleted;
    // Start is called before the first frame update
    void Start()
    {
            amountOfObjectivesCompleted = 1;
        
       //// TODO make it sure that it know it's in hubworld and set variable to 3
        //if (GameObject.Find("GameMaster").GetComponent<Master_Script>().hubLevel == ("Realm_of_keepers"))
        //{
        //    amountOfObjectivesCompleted = 3;
        //}
        //else
    }

    // Update is called once per frame
    void Update()
    {
        Nexttask();
    }

    void Nexttask()
    {

        switch ((int)amountOfObjectivesCompleted)
        {
            case 1:
                // updates the variables that we grab from the enemy handler script
                enemieskilled_obj = enemyHandler.GetComponent<EnemyHandler>().EnemiesKilled;
                enemieskilledUntilBoss_obj = enemyHandler.GetComponent<EnemyHandler>().EnemiesToKillBeforeBoss;
                
                // updates our objective UI information text and main text
                currenttask = Objective_UI.GetComponent<Text>();
                objectinfoText = information_UI.GetComponent<Text>();

                // changes the text for our UI elements
                objectinfoText.text = "Slay foes";
                currenttask.text = enemieskilled_obj + " out of " + enemieskilledUntilBoss_obj;
                if(enemieskilled_obj == 10)
                {
                    Debug.Log("I ran");
                    amountOfObjectivesCompleted = 2;
                }
                break;
            
            case 2:

                currenttask = Objective_UI.GetComponent<Text>();
                objectinfoText = information_UI.GetComponent<Text>();

                // changes text to this
                objectinfoText.text = "Slay boss";
                currenttask.text = "Find boss";
                Debug.Log("HEEEEEEEEEEEEEEEEEEEEEEEEEEEEEY");

                // bools sets to true
                isBossDead = enemyHandler.GetComponent<EnemyHandler>().bossDead;

                if (isBossDead == true)
                {
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
