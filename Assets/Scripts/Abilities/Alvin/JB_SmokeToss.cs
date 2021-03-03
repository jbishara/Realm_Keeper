using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_SmokeToss : MonoBehaviour
{
    private AbilityInfo m_smokeTossInfo;

    public AbilityInfo smokeTossInfo { get { return m_smokeTossInfo; } set { m_smokeTossInfo = value; } }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    
}
