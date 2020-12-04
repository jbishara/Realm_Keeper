using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_ArcaneShoot : MonoBehaviour
{
    private Rigidbody rigidBody;
    private AbilityInfo m_arcaneShootInfo;
    private int counter;

    [SerializeField] private float speed;
    
    // used to set damage to ability based off base attack damage of character
    public AbilityInfo arcaneShootInfo { get { return m_arcaneShootInfo; } set { m_arcaneShootInfo = value; } }

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
                other.gameObject.GetComponent<HealthComponent>().ApplyDamage(m_arcaneShootInfo);
                StartCoroutine(other.gameObject.GetComponent<HealthComponent>().ArmourAdjustment(-12f,5f));
            }
        }
    }
}
