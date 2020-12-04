using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_Blink : MonoBehaviour
{
    private Vector3 m_blinkLocation;


    [Tooltip("Layer Mask for raycast to hit - ground")]
    [SerializeField] private LayerMask layerMask;

    public Vector3 blinkLocation { get { return m_blinkLocation; } }


    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;

        // Does the ray intersect with the ground
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
            Debug.Log("Did Hit" + hit.point);
            m_blinkLocation = hit.point;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
