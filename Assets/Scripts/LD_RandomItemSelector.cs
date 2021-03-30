using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LD_RandomItemSelector : MonoBehaviour
{
    public GameObject[] items;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        index = Random.Range(0, items.Length);
        Debug.Log("this is index" + index);
        items[index].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
