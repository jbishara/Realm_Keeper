using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_DeadlyCloud : MonoBehaviour
{
    private AbilityInfo m_deadlyCloudInfo;
    private float m_magnitude;

    public AbilityInfo deadlyCloudInfo { get { return m_deadlyCloudInfo; } set { m_deadlyCloudInfo = value; } }
    public float magnitude { set { m_magnitude = value; } }

    public void SetLength(float magnitude)
    {
        float z = magnitude;

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
    }
    
}
