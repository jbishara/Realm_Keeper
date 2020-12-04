using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_DeadlyThrow : MonoBehaviour
{
    private Rigidbody rigidBody;

    [SerializeField] private float speed;
    private AbilityInfo m_deadlyThrowInfo;

    // used to set damage to ability based off base attack damage of character
    public AbilityInfo deadlyThrowInfo { get { return m_deadlyThrowInfo; } set { m_deadlyThrowInfo = value; } }

    
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
                collision.gameObject.GetComponent<HealthComponent>().ApplyDamage(m_deadlyThrowInfo);
                Destroy(gameObject);
            }
        }
    }
}
