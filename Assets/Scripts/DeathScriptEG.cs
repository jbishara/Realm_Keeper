using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScriptEG : MonoBehaviour
{
    /// <summary>
    /// attach this script to any object that has health component for example
    /// </summary>
    private HealthComponent hScript;

    // Start is called before the first frame update
    void Start()
    {
        // we are referencing the health component script that is attached to this script as well
        hScript = GetComponent<HealthComponent>();

        // this listens to the ondeath event attached to the health component on this object, so when the event is called u do the stuff u want below
        hScript.OnDeath += ThisObjectDied;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ThisObjectDied(HealthComponent self)
    {
        // do stuff when this object dies
    }
}
