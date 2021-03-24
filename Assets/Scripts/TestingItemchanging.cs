using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingItemchanging : MonoBehaviour
{
    public GameObject items;
    public GameObject[] itemprefabs;
    private int WhatNumberDidIRoll;
    // Start is called before the first frame update
    void Start()
    {
        // finds all gameobject with the tag "Item"
        //itemprefabs = GameObject.FindGameObjectsWithTag("Item");
        //WhatNumberDidIRoll = Random.Range (0, itemprefabs.Length);
        //items = itemprefabs[WhatNumberDidIRoll];
        //print(items.name);
        //print(itemprefabs.Length);
        //Instantiate(items, gameObject.transform.position, Quaternion.identity);
        //items = itemprefabs[Random.Range(0, itemprefabs.Length)];
    }

}
