using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { Normal, Fire, Poison }

public class JB_PlayerAbilities : MonoBehaviour
{
    private Rigidbody rigidBody;

    
    [SerializeField] private DamageInfo normalAttack;
    [SerializeField] private Transform rockThrowSpawnPoint;
    [SerializeField] private Transform rockThrowTargetLocation;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Transform meleeAttackArea;

    [Header("Values for Earth Speed ability")]
    [SerializeField] private float launchSpeed;
    [SerializeField] private float launchAngle;

    [Header("Prefabs for abilities to spawn")]
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject rangeAttackPrefab;
    [SerializeField] private GameObject arcaneShootPrefab;
    [SerializeField] private GameObject arcaneSwingPrefab;

    private float attackSwingTimer;

    private float abilityOneCooldownTimer;
    private float abilityTwoCooldownTimer;
    private float abilityThreeCooldownTimer;
    private float abilityFourCooldownTimer;
    private float abilityFiveCooldownTimer;
    private float abilitySixCooldownTimer;
    private float abilitySevenCooldownTimer;
    private float abilityEightCooldownTimer;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        //normalAttack.damage = characterStats.attackDamage;
    }

    // Update is called once per frame
    void Update()
    {
        Timers();

        PlayerInput();
    }

    private void Timers()
    {
        // timers for each ability, calculated when needed

        if (attackSwingTimer >= 0)
            attackSwingTimer -= Time.deltaTime;

        if (abilityOneCooldownTimer >= 0)
            abilityOneCooldownTimer -= Time.deltaTime;

        if (abilityTwoCooldownTimer >= 0)
            abilityTwoCooldownTimer -= Time.deltaTime;

        if (abilityThreeCooldownTimer >= 0)
            abilityThreeCooldownTimer -= Time.deltaTime;

        if (abilityFourCooldownTimer >= 0)
            abilityFourCooldownTimer -= Time.deltaTime;

        if (abilityFiveCooldownTimer >= 0)
            abilityFiveCooldownTimer -= Time.deltaTime;

        if (abilitySixCooldownTimer >= 0)
            abilitySixCooldownTimer -= Time.deltaTime;

        if (abilitySevenCooldownTimer >= 0)
            abilitySevenCooldownTimer -= Time.deltaTime;

        if (abilityEightCooldownTimer >= 0)
            abilityEightCooldownTimer -= Time.deltaTime;
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // ability one
            RockThrow();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // ability two
            ArcaneShoot();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // ability three
            StoneSkin();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // ability four
            EarthSpeed();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // melee attack
            BasicMeleeAttack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("right mouse click ...");
            // range attack
            BasicRangeAttack();
        }

    }

  

    private void BasicMeleeAttack()
    {
        float attackSpeed = GetComponent<JB_PlayerStats>().attackSpeed;
        normalAttack.damage = GetComponent<JB_PlayerStats>().attackDamage;
        
        if (attackSwingTimer <= 0)
        {
            // used to create a delay between attacks
            attackSwingTimer = attackSpeed;


            //TODO - add in animation


            // melee attack
            var colInfo = Physics.OverlapSphere(meleeAttackArea.position, 6f);

            if (colInfo != null)
            {
                foreach (Collider col in colInfo)
                {
                    if (col.gameObject.GetComponent<HealthComponent>())
                    {
                        col.gameObject.GetComponent<HealthComponent>().ApplyDamage(normalAttack);
                    }
                }
            }
        }

        

    }

    private void BasicRangeAttack()
    {
        float attackSpeed = GetComponent<JB_PlayerStats>().attackSpeed;
        float attackdmg = GetComponent<JB_PlayerStats>().attackDamage;

        if (attackSwingTimer <= 0)
        {
            attackSwingTimer = attackSpeed;

            // range attack
            GameObject obj = Instantiate(rangeAttackPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            obj.GetComponent<JB_RangeProjectile>().attackDamage = attackdmg;
        }
        
    }

    private void RockThrow()
    {
        float attackDamage = GetComponent<JB_PlayerStats>().attackDamage;

        if(abilityOneCooldownTimer <= 0)
        {
            
            GameObject obj = Instantiate(rockPrefab, rockThrowSpawnPoint.position, rockThrowSpawnPoint.rotation);
            obj.GetComponent<JB_RockProjectile>().targetLocation = rockThrowTargetLocation;

            obj.GetComponent<JB_RockProjectile>().attackDamage = attackDamage * 1.3f;

            abilityOneCooldownTimer = obj.GetComponent<DamageInfo>().cooldown;
        }

        
    }

    private void ArcaneShoot()
    {
        float attackDamage = GetComponent<JB_PlayerStats>().attackDamage;

        if(abilityTwoCooldownTimer <= 0)
        {
            GameObject obj = Instantiate(arcaneShootPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            obj.GetComponent<JB_ArcaneShoot>().attackDamage = attackDamage *1.1f;

            abilityTwoCooldownTimer = obj.GetComponent<JB_ArcaneShoot>().cooldown;
        }
    }

    private void ArcaneSwing()
    {
        float attackDamage = GetComponent<JB_PlayerStats>().attackDamage;

        if(abilityThreeCooldownTimer <= 0)
        {
            GameObject obj = Instantiate(arcaneSwingPrefab, meleeAttackArea.position, meleeAttackArea.rotation);

            obj.GetComponent<JB_ArcaneSwing>().attackDamage = attackDamage;

            abilityFourCooldownTimer = obj.GetComponent<JB_ArcaneSwing>().cooldown;
        }
    }

    private void StoneSkin()
    {

        // Creates a stone skin around himself that will increase his armor with 25 armor for 6 seconds
        // cooldown = 15 seconds
        if(abilityFourCooldownTimer <= 0)
        {
            StartCoroutine(GetComponent<HealthComponent>().ArmourAdjustment(25f, 6f));
            abilityFourCooldownTimer = 15f;
        }

        
    }

    private void EarthSpeed()
    {
        // slingshot player forward
        // cooldown = 10 seconds

        Debug.Log("button reached");

        if(abilityFiveCooldownTimer <= 0)
        {
            Debug.Log("skill reached");
            LaunchPlayer();
            abilityFiveCooldownTimer = 1f;
        }
    }

    private void LaunchPlayer()
    {
        Debug.Log("launch reached");


        // think of it as top-down view of vectors: 
        // we don't care about the y-component(height) of the initial and target position.
        Vector3 projectileXZPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
        Vector3 targetXZPos = new Vector3(rockThrowTargetLocation.position.x, 0.0f, rockThrowTargetLocation.position.z);

        // rotate the object to face the target
        transform.LookAt(targetXZPos);

        // shorthands for the formula
        float R = Vector3.Distance(projectileXZPos, targetXZPos);
        float G = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(launchAngle * Mathf.Deg2Rad);
        float H = rockThrowTargetLocation.position.y - transform.position.y;

        // calculate the local space components of the velocity 
        // required to land the projectile on the target object 
        float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
        float Vy = tanAlpha * Vz;

        // create the velocity vector in local space and get it in global space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        // launch the object by setting its initial velocity and flipping its state
        rigidBody.velocity = globalVelocity * launchSpeed;

    }

    private void Charge()
    {
        
    }
}
