using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_MedusaAoeCircle : MonoBehaviour
{
    [HideInInspector]
    public AbilityInfo aoeDamage;

    [HideInInspector]
    public float duration;

    private void Start()
    {
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<HealthComponent>())
            {
                other.gameObject.GetComponent<HealthComponent>().ApplyDamage(aoeDamage);
            }
        }
    }
}
