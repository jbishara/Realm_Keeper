using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;


public enum DamageType { Normal, Fire, Poison }

public class JB_PlayerAbilities : MonoBehaviour
{
    private Rigidbody rigidBody;
    private AbilityInfo chargeInfo;
    private AbilityInfo wackInfo;
    private JB_PlayerStats playerStats;

    public List<CharacterAbilities> abilityList;

    [SerializeField] private CharacterClass characterClass;
    [SerializeField] private AbilityInfo normalAttack;
    [SerializeField] private Transform rockThrowSpawnPoint;
    [SerializeField] private Transform rockThrowTargetLocation;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Transform meleeAttackArea;
    [SerializeField] private Transform blinkLocation;

    [Header("Speed for Earth Speed ability")]
    [SerializeField] private float launchSpeed;
    private vThirdPersonController playerController;

    [Header("Prefabs for abilities to spawn")]
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject arcaneShootPrefab;
    [SerializeField] private GameObject arcaneSwingPrefab;
    [SerializeField] private GameObject medusaKissPrefab;
    [SerializeField] private GameObject deadlyThrowPrefab;
    [SerializeField] private GameObject deathMarkPrefab;
    [SerializeField] private GameObject soulDrainObject;

    private bool isCharging;
    private bool isSoulDraining;
    private float attackSwingTimer;
    private float[] abilityCooldownTimer;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        abilityCooldownTimer = new float[4];

        playerController = GetComponent<vThirdPersonController>();

        playerStats = GetComponent<JB_PlayerStats>();

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

    // abilities bound to keyboard keybinds
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
        UseSelectedAbility(AbilityType.Cast);
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

    #region Collider / Trigger Functions

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isCharging && collision.gameObject.tag == "Enemy")
        {
            rigidBody.velocity = Vector3.zero;

            StopAllCoroutines();
            playerController.enabled = true;

            if (collision.gameObject.GetComponent<HealthComponent>())
            {
                collision.gameObject.GetComponent<HealthComponent>().ApplyDamage(chargeInfo);
            }
        }
        else
        {
            StopAllCoroutines();
        }
    }

    #endregion

    #region Tansea abilities

    private void BasicMeleeAttack()
    {
        float attackSpeed = playerStats.attackSpeed;
        normalAttack.damage = playerStats.attackDamage;
        
        if (attackSwingTimer <= 0)
        {
            // used to create a delay between attacks
            attackSwingTimer = attackSpeed;


            //TODO - add in animation


            // melee attack
            var colInfo = Physics.OverlapSphere(meleeAttackArea.position, 5f);

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
       
        
    }

    

    private void RockThrow(CharacterAbilities ability)
    {
        float attackDamage = playerStats.attackDamage * ability.abilityInfo.damageMultiplier;
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
        float attackDamage = playerStats.attackDamage * ability.abilityInfo.damageMultiplier;
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            GameObject obj = Instantiate(arcaneShootPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            obj.GetComponent<JB_ArcaneShoot>().attackDamage = attackDamage;

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void ArcaneSwing(CharacterAbilities ability)
    {
        float attackDamage = playerStats.attackDamage * ability.abilityInfo.damageMultiplier; 
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
        int index = (int)ability.abilityType;

        if(abilityCooldownTimer[index] <= 0)
        {
            StartCoroutine(LaunchPlayer());
            abilityCooldownTimer[index] = ability.cooldown;
        }
        
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
        
        if (abilityCooldownTimer[index] <= 0)
        {
            StartCoroutine(GetComponent<HealthComponent>().ArmourAdjustment(25f, 6f));
            abilityCooldownTimer[index] = ability.cooldown;
        }

        
    }

    private void Charge(CharacterAbilities ability)
    {
        float attackDamage = playerStats.attackDamage * ability.abilityInfo.damageMultiplier;
        int index = (int)ability.abilityType;

        chargeInfo = ability.abilityInfo;
        chargeInfo.damage = attackDamage;

        if (abilityCooldownTimer[index] <= 0)
        {
            StartCoroutine(Charging());
            abilityCooldownTimer[index] = ability.cooldown;
        }
        
    }

    IEnumerator Charging()
    {
        playerController.enabled = false;
        isCharging = true;
        rigidBody.velocity = transform.forward * launchSpeed;

        yield return new WaitForSeconds(3.5f);

        isCharging = false;
        rigidBody.velocity = Vector3.zero;
        playerController.enabled = true;
        
    }

    private void MotherNature(CharacterAbilities ability)
    {
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            StartCoroutine(playerStats.AttackSpeedBuff(0.35f, 8f));
            abilityCooldownTimer[index] = ability.cooldown;
        }
        

    }

    private void ArcaneWack(CharacterAbilities ability)
    {
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            wackInfo = ability.abilityInfo;
            // begin wack animation - use animation sheet to call Wack event

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }
    
    private void WackDamage()
    {
        wackInfo.damage = playerStats.attackDamage * wackInfo.damageMultiplier;

        var colInfo = Physics.OverlapSphere(meleeAttackArea.position, 5f);

        if (colInfo != null)
        {
            foreach (Collider col in colInfo)
            {
                if (col.gameObject.GetComponent<HealthComponent>())
                {
                    col.gameObject.GetComponent<HealthComponent>().ApplyDamage(wackInfo);
                }
            }
        }
    }


    #endregion

    #region Zylar abilities

    private void MedusaKiss(CharacterAbilities ability)
    {
        float attackDamage = playerStats.attackDamage *ability.abilityInfo.damageMultiplier;
        int index = (int)ability.abilityType;

        if(abilityCooldownTimer[index] <= 0)
        {

            GameObject obj = Instantiate(medusaKissPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            obj.GetComponent<JB_MedusaKiss>().attackDamage = attackDamage;
            obj.GetComponent<JB_MedusaAoeCircle>().duration = ability.abilityInfo.dmgDuration;

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void DeadlyThrow(CharacterAbilities ability)
    {
        
        float attackDamage = playerStats.attackDamage * ability.abilityInfo.damageMultiplier;
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            

            // range attack
            GameObject obj = Instantiate(deadlyThrowPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            obj.GetComponent<JB_DeadlyThrow>().attackDamage = attackDamage;

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void DeathMark(CharacterAbilities ability)
    {
        int index = (int)ability.abilityType;

        
        if (abilityCooldownTimer[index] <= 0)
        {
            
            GameObject obj = Instantiate(deathMarkPrefab, rockThrowSpawnPoint.position, projectileSpawnPoint.rotation);

            obj.GetComponent<JB_DeathMarkAoe>().deathMarkInfo = ability.abilityInfo;
            obj.GetComponent<JB_DeathMarkAoe>().DestroyThis(ability.abilityInfo.dmgDuration);

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void SoulDrain(CharacterAbilities ability)
    {
        int index = (int)ability.abilityType;

        float duration = ability.abilityInfo.dmgDuration;

        if (abilityCooldownTimer[index] <= 0)
        {

            // starting coroutine for turning on / off souldrain ability
            StartCoroutine(SoulDraining(duration, ability.abilityInfo));
            //GameObject obj = Instantiate(deathMarkPrefab, rockThrowSpawnPoint.position, projectileSpawnPoint.rotation);

            

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    /// <summary>
    /// Activating souldrain ability
    /// duration - how long to keep ability active
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="ability"></param>
    /// <returns></returns>
    IEnumerator SoulDraining(float duration, AbilityInfo ability)
    {
        soulDrainObject.SetActive(true);
        soulDrainObject.GetComponent<JB_SoulDrain>().abilityInfo = ability;
        soulDrainObject.GetComponent<JB_SoulDrain>().abilityInfo.damage = playerStats.attackDamage * ability.damageMultiplier;
        yield return new WaitForSeconds(duration);
        soulDrainObject.SetActive(false);
    }

    private void Blink(CharacterAbilities ability)
    {
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {

            // move player forward

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    #endregion

}
