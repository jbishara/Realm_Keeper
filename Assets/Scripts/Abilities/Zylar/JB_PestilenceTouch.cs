using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_PestilenceTouch : MonoBehaviour
{
    private AbilityInfo m_pestilenceInfo;

    public AbilityInfo pestilenceInfo { get { return m_pestilenceInfo; } set { m_pestilenceInfo = value; } }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, m_pestilenceInfo.dmgDuration);
    }

    
}
