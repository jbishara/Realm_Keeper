using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_SoulDrain : MonoBehaviour
{
    public AbilityInfo m_abilityInfo;

    private GameObject enemy;
    public JB_PlayerStats playerStats;

    public AbilityInfo abilityInfo { get { return m_abilityInfo; } set { m_abilityInfo = value; } }

    

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Enemy")
    //    {
    //        if (other.gameObject.GetComponent<HealthComponent>())
    //        {
    //            //other.gameObject.GetComponent<HealthComponent>().ApplyDamage(m_abilityInfo);
    //            enemy = other.gameObject;
    //            AdjustDamage();
    //            InvokeRepeating("ApplyingDamage", 0f, 0.5f);

    //        }
    //    }
    //}

    //private void AdjustDamage()
    //{
    //    m_abilityInfo.damage = playerStats.attackDamage * m_abilityInfo.damageMultiplier;
    //}

    //private void ApplyingDamage()
    //{
        
    //    enemy.GetComponent<HealthComponent>().ApplyDamage(m_abilityInfo);
    //}
}
