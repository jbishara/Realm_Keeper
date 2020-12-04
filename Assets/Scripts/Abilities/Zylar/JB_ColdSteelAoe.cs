using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_ColdSteelAoe : MonoBehaviour
{
    private AbilityInfo m_coldSteelInfo;

    [Tooltip("Rotation Speed")]
    public float rotSpeed;

    public AbilityInfo coldSteelInfo { get { return m_coldSteelInfo; } set { m_coldSteelInfo = value; } }

    private void Start()
    {
        Destroy(gameObject, m_coldSteelInfo.dmgDuration);
    }

    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotSpeed);
    }
}
