using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayer : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Master_Script.instance.player.transform.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
