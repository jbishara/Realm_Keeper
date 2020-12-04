using UnityEngine;
using System.Collections;

public class JB_ArcadeBarrage : MonoBehaviour
{
    public float speed = 6.0f;

    private AbilityInfo m_arcadeBarrageInfo;

    public AbilityInfo arcadeBarrageInfo { get { return m_arcadeBarrageInfo; }set { m_arcadeBarrageInfo = value; } }

    // target location to travel to
    private Vector3 m_targetLocation;

    public Vector3 targetLocation { set { m_targetLocation = value; } }
    
    private float time;
    private float timer = 0.0f;

    private float xPow;
    private float yPow;
    private float zPow;
    private Vector3 prevPos;
    private Vector3 startPos;
    private Transform trans;

    void Start()
    {
        xPow = Random.Range(0.4f, 3.0f);
        yPow = Random.Range(0.4f, 3.0f);
        zPow = Random.Range(0.4f, 3.0f);

        trans = transform;
        prevPos = trans.position;
        startPos = transform.position;

        time = (m_targetLocation - trans.position).magnitude / speed;
    }

    void Update()
    {
        // erratic behaviour to travel towards target location
        Vector3 v3 = startPos;
        v3.x = Mathf.Lerp(v3.x, m_targetLocation.x, Mathf.Pow(timer / time, xPow));
        v3.y = Mathf.Lerp(v3.y, m_targetLocation.y, Mathf.Pow(timer / time, yPow));
        v3.z = Mathf.Lerp(v3.z, m_targetLocation.z, Mathf.Pow(timer / time, zPow));

        trans.position = v3;

        timer += Time.deltaTime;

        if (trans.position != prevPos)
        {
            trans.rotation = Quaternion.LookRotation(trans.position - prevPos);
        }

        prevPos = trans.position;

        if (timer > time)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            float rand = Random.Range(0f, 1f);

            // 20% chance to reduce armour
            if(rand >= 0.8f)
            {
                other.gameObject.GetComponent<HealthComponent>().ArmourAdjustment(6f, 3f);
            }

            other.gameObject.GetComponent<HealthComponent>().ApplyDamage(m_arcadeBarrageInfo);

            Destroy(gameObject);
        }
    }
}