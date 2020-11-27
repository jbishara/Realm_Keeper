using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_RockProjectile : MonoBehaviour
{
    private Transform m_targetLocation;
    private Rigidbody rigidBody;
    private bool isMoving;

    [SerializeField] private float force;
    [SerializeField] private DamageInfo rockThrowDamage;
    
    [Range(20.0f, 75.0f)] public float launchAngle;

    public Transform targetLocation { get { return m_targetLocation; } set { m_targetLocation = value; } }

    // used to set damage to ability based off base attack damage of character
    public float attackDamage { set { rockThrowDamage.damage = value; } }

    // cooldown of ability
    public float cooldown { get { return rockThrowDamage.cooldown; } }


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        //rigidBody.AddForce(transform.forward * force);

        Invoke("TurnOnCollider", 0.1f);
        Launch();
    }


    // launches the object towards the TargetObject with a given LaunchAngle
    void Launch()
    {
        // think of it as top-down view of vectors: 
        // we don't care about the y-component(height) of the initial and target position.
        Vector3 projectileXZPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
        Vector3 targetXZPos = new Vector3(m_targetLocation.position.x, 0.0f, m_targetLocation.position.z);

        // rotate the object to face the target
        transform.LookAt(targetXZPos);

        // shorthands for the formula
        float R = Vector3.Distance(projectileXZPos, targetXZPos);
        float G = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(launchAngle * Mathf.Deg2Rad);
        float H = m_targetLocation.position.y - transform.position.y;

        // calculate the local space components of the velocity 
        // required to land the projectile on the target object 
        float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
        float Vy = tanAlpha * Vz;

        // create the velocity vector in local space and get it in global space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        // launch the object by setting its initial velocity and flipping its state
        rigidBody.velocity = globalVelocity *force;
        isMoving = false;
    }

    private void TurnOnCollider()
    {
        GetComponent<SphereCollider>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // making sure health component script exists
        if(collision.gameObject.GetComponent<HealthComponent>())
        {
            collision.gameObject.GetComponent<HealthComponent>().ApplyDamage(rockThrowDamage);
        }
    }
}
