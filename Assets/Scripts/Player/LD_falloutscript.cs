using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LD_falloutscript : MonoBehaviour
{
    public GameObject falloutSpawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = falloutSpawner.transform.position;
    }
}
