using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_ArcadeShoot : MonoBehaviour
{
    private Rigidbody rigidBody;
    private AbilityInfo m_arcadeShootInfo;
    private bool m_isThirdAttack;

    [SerializeField] private float speed;

    // used to set damage to ability based off base attack damage of character
    public AbilityInfo arcadeShootInfo { get { return m_arcadeShootInfo; } set { m_arcadeShootInfo = value; } }

    // true if this projectil is third attack phase 
    public bool isThirdAttack { set { m_isThirdAttack = value; } }

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
        
        if (m_isThirdAttack)
        {
            // make sure healthcomponent exists
            if (other.gameObject.GetComponent<HealthComponent>())
            {
                other.gameObject.GetComponent<HealthComponent>().ApplyDamage(m_arcadeShootInfo);
                StartCoroutine(other.gameObject.GetComponent<HealthComponent>().ArmourAdjustment(-10f, 3f));
                Destroy(gameObject);
            }
        }
        else
        {
            // make sure healthcomponent exists
            if (other.gameObject.GetComponent<HealthComponent>())
            {
                other.gameObject.GetComponent<HealthComponent>().ApplyDamage(m_arcadeShootInfo);
                Destroy(gameObject);
            }
        }
    }
}
