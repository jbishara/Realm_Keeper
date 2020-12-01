using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_ArcaneShoot : MonoBehaviour
{
    private Rigidbody rigidBody;

    [SerializeField] private float speed;
    [SerializeField] private AbilityInfo arcaneShootDamage;
    
    // used to set damage to ability based off base attack damage of character
    public float attackDamage { set { arcaneShootDamage.damage = value; } }

    // cooldown of ability
    public float cooldown { get { return arcaneShootDamage.cooldown; } }

    private int counter;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = (transform.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        ++counter;

        // destroy this after hitting 5 targets
        if(counter >= 5)
        {
            Destroy(gameObject);
        }
        else
        {
            // make sure healthcomponent exists
            if (other.gameObject.GetComponent<HealthComponent>())
            {
                other.gameObject.GetComponent<HealthComponent>().ApplyDamage(arcaneShootDamage);
                StartCoroutine(other.gameObject.GetComponent<HealthComponent>().ArmourAdjustment(-12f,5f));
            }
        }
    }
}
