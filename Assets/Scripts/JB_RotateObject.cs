﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_RotateObject : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * speed);
    }
}
