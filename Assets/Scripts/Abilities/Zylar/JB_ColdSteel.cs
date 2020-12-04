using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_ColdSteel : MonoBehaviour
{
    private Rigidbody rigidBody;

    [SerializeField] private GameObject coldSteelAoePrefab;
    [SerializeField] private float speed;
    
    private AbilityInfo m_coldSteelInfo;

    // used to set damage to ability based off base attack damage of character
    public AbilityInfo coldSteelInfo { get { return m_coldSteelInfo; } set { m_coldSteelInfo = value; } }


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidBody.velocity = (transform.forward * speed);
        //transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<HealthComponent>())
            {
                GameObject obj = Instantiate(coldSteelAoePrefab, transform.position, Quaternion.identity);
                obj.GetComponent<JB_ColdSteelAoe>().coldSteelInfo = m_coldSteelInfo;

                Destroy(gameObject);
            }
        }
    }
}
