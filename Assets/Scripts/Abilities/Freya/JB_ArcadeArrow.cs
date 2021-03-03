using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_ArcadeArrow : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody rigidBody;
    private AbilityInfo m_arcadeArrowInfo;

    // used to set damage to ability based off base attack damage of character
    public AbilityInfo arcadeArrowInfo { get { return m_arcadeArrowInfo; } set { m_arcadeArrowInfo = value; } }

    

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, m_arcadeArrowInfo.dmgDuration);
    }

    
    void FixedUpdate()
    {
        rigidBody.velocity = (transform.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // pierces thru an infinite number of enemies
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<HealthComponent>().ApplyDamage(m_arcadeArrowInfo);
        }
        // destroys arrow when not hitting with an enemy
        else
        {
            Destroy(gameObject);
        }

    }
}
