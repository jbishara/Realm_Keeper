using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_ArcaneSwing : MonoBehaviour
{
    private AbilityInfo m_arcaneSwingInfo;

    // used to set damage to ability based off base attack damage of character
    public AbilityInfo arcaneSwingInfo { get { return m_arcaneSwingInfo; } set { m_arcaneSwingInfo = value; } }


    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            if (other.gameObject.GetComponent<HealthComponent>())
            {
                other.gameObject.GetComponent<HealthComponent>().ApplyDamage(m_arcaneSwingInfo);
                //other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(100f, other.gameObject.transform.position, 10f);
                Destroy(gameObject);
                // TODO - knockback enemy

            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }
}
