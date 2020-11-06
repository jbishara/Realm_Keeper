using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_RangeProjectile : MonoBehaviour
{
    private Rigidbody rigidBody;

    [SerializeField] private float speed;
    [SerializeField] DamageInfo dmgAmount;

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
        if(collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<HealthComponent>())
            {
                collision.gameObject.GetComponent<HealthComponent>().ApplyDamage(dmgAmount, DamageType.Normal);
                Destroy(gameObject);
            }
        }
    }
}
