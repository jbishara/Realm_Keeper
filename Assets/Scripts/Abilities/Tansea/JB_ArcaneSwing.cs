using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_ArcaneSwing : MonoBehaviour
{
    [SerializeField] private AbilityInfo m_arcaneSwingInfo;

    // used to set damage to ability based off base attack damage of character
    public AbilityInfo arcaneSwingInfo { get { return m_arcaneSwingInfo; } set { m_arcaneSwingInfo = value; } }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<HealthComponent>())
        {   
            // make sure to not take health from player
            if(other.gameObject.tag != "Player")
            {
                other.gameObject.GetComponent<HealthComponent>().ApplyDamage(m_arcaneSwingInfo);

                // TODO - knockback enemy
            }
                
        }
    }
}
