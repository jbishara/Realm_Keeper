using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScriptEG : MonoBehaviour
{
    /// <summary>
    /// attach this script to any object that has health component for example
    /// </summary>
    private HealthComponent hScript;
    public GameObject deathMenu;
    private FSM_Enemy eScript;
    private int drop;
    public GameObject itemDrop;

    // Start is called before the first frame update
    void Start()
    {

        // we are referencing the health component script that is attached to this script as well
        hScript = GetComponent<HealthComponent>();

        // this listens to the ondeath event attached to the health component on this object, so when the event is called u do the stuff u want below
        hScript.OnDeath += ThisObjectDied;

        SceneManager.activeSceneChanged += CheckReturnMenu;
    }

    private void CheckReturnMenu(Scene current, Scene next)
    {
        // buildindex 0 is main menu
        if(current.buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }

    private void ThisObjectDied(HealthComponent self)
    {
        // do stuff when this object dies
        if (gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            deathMenu.SetActive(true);
        }
        else
        {
            // get enemyscript
            eScript = gameObject.GetComponent<FSM_Enemy>();
            drop = gameObject.GetComponent<FSM_Enemy>().DropChance;

            if(Random.Range(0, 100) <= drop)
            {
                Instantiate(itemDrop, gameObject.transform.position, Quaternion.identity);
                Debug.Log("Item drop!");
            }
        }
    }
}
