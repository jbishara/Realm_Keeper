using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_ArcaneSwing : MonoBehaviour
{
    [SerializeField] private AbilityInfo arcaneSwingDamage;

    // used to set damage to ability based off base attack damage of character
    public float attackDamage { set { arcaneSwingDamage.damage = value; } }

    // cooldown of ability
    public float cooldown { get { return arcaneSwingDamage.cooldown; } }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<HealthComponent>())
        {   
            // make sure to not take health from player
            if(other.gameObject.tag != "Player")
            {
                other.gameObject.GetComponent<HealthComponent>().ApplyDamage(arcaneSwingDamage);

                // TODO - knockback enemy
            }
                
        }
    }
}
