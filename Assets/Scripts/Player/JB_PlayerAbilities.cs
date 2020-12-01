using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;


public enum DamageType { Normal, Fire, Poison }

public class JB_PlayerAbilities : MonoBehaviour
{
    private Rigidbody rigidBody;

    public List<CharacterAbilities> abilityList;

    [SerializeField] private CharacterClass characterClass;
    [SerializeField] private AbilityInfo normalAttack;
    [SerializeField] private Transform rockThrowSpawnPoint;
    [SerializeField] private Transform rockThrowTargetLocation;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Transform meleeAttackArea;

    [Header("References for Earth Speed ability")]
    [SerializeField] private float launchSpeed;
    [SerializeField] private float launchAngle;
    private vThirdPersonController playerController;

    [Header("Prefabs for abilities to spawn")]
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject rangeAttackPrefab;
    [SerializeField] private GameObject arcaneShootPrefab;
    [SerializeField] private GameObject arcaneSwingPrefab;

    private float attackSwingTimer;
    private float[] abilityCooldownTimer;

    


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        abilityCooldownTimer = new float[4];

        playerController = GetComponent<vThirdPersonController>();

        foreach (CharacterAbilities abilities in abilityList)
        {
            abilities.InitialiseVariables();

            // first set of abilities active to those that match this game object character class
            if(abilities.characterClass == characterClass && !abilities.needChallenge)
            {
                abilities.isActive = true;
            }
        }
        
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

        if (abilityCooldownTimer[0] >= 0)
            abilityCooldownTimer[0] -= Time.deltaTime;

        if (abilityCooldownTimer[1] >= 0)
            abilityCooldownTimer[1] -= Time.deltaTime;

        if (abilityCooldownTimer[2] >= 0)
            abilityCooldownTimer[2] -= Time.deltaTime;

        if (abilityCooldownTimer[3] >= 0)
            abilityCooldownTimer[3] -= Time.deltaTime;

        
   
    }

    #region user input for abilities

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // ability one
            //RockThrow();
            UseDamageAbility();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // ability two
            //ArcaneShoot();
            UseUtilityAbility();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // ability three
            //StoneSkin();
            UseMobilityAbility();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // ability four
            UseUltimateAbility();
            
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

    

    private void UseUltimateAbility()
    {
        UseSelectedAbility(AbilityType.Ultimate);
    }

    private void UseMobilityAbility()
    {
        UseSelectedAbility(AbilityType.Mobility);
    }

    private void UseUtilityAbility()
    {
        UseSelectedAbility(AbilityType.Utility);
    }

    private void UseDamageAbility()
    {
        UseSelectedAbility(AbilityType.Damage);
    }
  
    private void UseSelectedAbility(AbilityType abilityType)
    {
        foreach (CharacterAbilities ability in abilityList)
        {
            if (ability.abilityType == abilityType && ability.isActive)
            {
                gameObject.SendMessage(ability.abilityName, ability);
            }
        }
    }

    #endregion

    #region Tansea abilities
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

    

    private void RockThrow(CharacterAbilities ability)
    {
        float attackDamage = GetComponent<JB_PlayerStats>().attackDamage;
        int index = (int)ability.abilityType;

        if(abilityCooldownTimer[index] <= 0)
        {
            
            GameObject obj = Instantiate(rockPrefab, rockThrowSpawnPoint.position, rockThrowSpawnPoint.rotation);
            obj.GetComponent<JB_RockProjectile>().targetLocation = rockThrowTargetLocation;

            obj.GetComponent<JB_RockProjectile>().attackDamage = attackDamage;

            //abilityOneCooldownTimer = obj.GetComponent<AbilityInfo>().cooldown;
            abilityCooldownTimer[index] = ability.cooldown;
        }

        
    }

    private void ArcaneShoot(CharacterAbilities ability)
    {
        float attackDamage = GetComponent<JB_PlayerStats>().attackDamage;
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            GameObject obj = Instantiate(arcaneShootPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            obj.GetComponent<JB_ArcaneShoot>().attackDamage = attackDamage *1.1f;

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void ArcaneSwing(CharacterAbilities ability)
    {
        float attackDamage = GetComponent<JB_PlayerStats>().attackDamage;
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            GameObject obj = Instantiate(arcaneSwingPrefab, meleeAttackArea.position, meleeAttackArea.rotation);

            obj.GetComponent<JB_ArcaneSwing>().attackDamage = attackDamage;

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void EarthSpeed(CharacterAbilities ability)
    {
        StartCoroutine(LaunchPlayer());
    }

    IEnumerator LaunchPlayer()
    {
        playerController.enabled = false;

        Vector3 dir = rockThrowSpawnPoint.forward;
        dir.Normalize();

        rigidBody.velocity = dir * launchSpeed;
        yield return new WaitForSeconds(3f);
        rigidBody.velocity = Vector3.zero;
        playerController.enabled = true;
    }

    private void StoneSkin(CharacterAbilities ability)
    {
        int index = (int)ability.abilityType;

        // Creates a stone skin around himself that will increase his armor with 25 armor for 6 seconds
        // cooldown = 15 seconds
        if (abilityCooldownTimer[index] <= 0)
        {
            StartCoroutine(GetComponent<HealthComponent>().ArmourAdjustment(25f, 6f));
            abilityCooldownTimer[index] = ability.cooldown;
        }

        
    }

    private void Charge()
    {
        
    }

    #endregion
}
