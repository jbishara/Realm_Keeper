using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_RockProjectile : MonoBehaviour
{
    private Rigidbody rigidBody;

    [SerializeField] private float force;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Vector3 downRotation;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(transform.forward * force);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.forward = Vector3.RotateTowards(transform.forward, downRotation, turnSpeed * Time.deltaTime, 1.0f);

        //rigidBody.velocity = transform.forward * speed;

        
        
    }
}
