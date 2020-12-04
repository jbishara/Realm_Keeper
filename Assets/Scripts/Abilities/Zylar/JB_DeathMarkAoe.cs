using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_DeathMarkAoe : MonoBehaviour
{
    
    private AbilityInfo m_deathMarkInfo;

    public AbilityInfo deathMarkInfo { get { return m_deathMarkInfo; }set { m_deathMarkInfo = value; } }

    
    public void DestroyThis(float duration)
    {
        Destroy(gameObject, duration);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Enemy")
    //    {
    //        Debug.Log("enemy test");
    //        if (other.gameObject.GetComponent<HealthComponent>())
    //        {
    //            StartCoroutine(other.gameObject.GetComponent<HealthComponent>().ArmourAdjustment(-20f, deathMarkInfo.dmgDuration));
    //        }
    //    }
    //}
}
