using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_DeathMarkAoe : MonoBehaviour
{
    [HideInInspector]
    public AbilityInfo deathMarkInfo;

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
                other.gameObject.GetComponent<HealthComponent>().ArmourAdjustment(20f, deathMarkInfo.dmgDuration);
            }
        }
    }
}
