using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_MedusaKiss : MonoBehaviour
{
    private Rigidbody rigidBody;

    [SerializeField] private float speed;
    [SerializeField] private AbilityInfo m_medusaKissDamage;
    [SerializeField] private GameObject aoePrefab;

    // used to set damage to ability based off base attack damage of character
    public AbilityInfo medusaKissInfo { get { return m_medusaKissDamage; } set { m_medusaKissDamage = value; } }


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = (transform.forward * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject obj = Instantiate(aoePrefab, collision.gameObject.transform.position, Quaternion.identity);

            obj.GetComponent<JB_MedusaAoeCircle>().aoeDamage = m_medusaKissDamage;
            obj.GetComponent<JB_MedusaAoeCircle>().duration = m_medusaKissDamage.dmgDuration;

            if (collision.gameObject.GetComponent<HealthComponent>())
            {
                collision.gameObject.GetComponent<HealthComponent>().ApplyDamage(m_medusaKissDamage);
                
                Destroy(gameObject);
            }
        }
    }
}
