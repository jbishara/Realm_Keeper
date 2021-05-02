using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Invector.vCharacterController;



public enum DamageType
{
    Normal,
    Fire,
    Poison
}

public enum CharacterClass
{
    Tansea,
    Zylar,
    Freya,
    Alvin
}
public enum AbilityType
{
    Cast,
    Utility,
    Mobility,
    Ultimate
}

public class JB_PlayerAbilities : MonoBehaviour
{
    [HideInInspector] public bool isBookHeld;

    private int attackPhase;
    private Rigidbody rigidBody;
    private JB_PlayerStats playerStats;
    private IEnumerator myCoroutine;
    private vThirdPersonController playerController;
    private LD_AudioManager audioManager;
    private Animator animController;

    
    [SerializeField] public List<AbilityInfo> abilityList;

    [SerializeField] private CharacterClass characterClass;
    [SerializeField] private AbilityInfo normalAttack;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Transform meleeAttackArea;
    
    [Header("Tansea Prefabs and references")]
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject arcaneShootPrefab;
    [SerializeField] private GameObject arcaneSwingPrefab;
    [SerializeField] private GameObject motherNatureEffect;
    [SerializeField] private Transform rockThrowSpawnPoint;
    [SerializeField] private Transform rockThrowTargetLocation;
    [SerializeField] private float earthSpeedLaunchPower;
    private AbilityInfo chargeInfo;
    private AbilityInfo wackInfo;

    [Header("Zylar Prefabs and references")]
    [SerializeField] private GameObject medusaKissPrefab;
    [SerializeField] private GameObject deadlyThrowPrefab;
    [SerializeField] private GameObject deathMarkPrefab;
    [SerializeField] private GameObject soulDrainObject;
    [SerializeField] private GameObject deadlyCloudObject;
    [SerializeField] private GameObject coldSteelPrefab;
    [SerializeField] private GameObject pestilenceTouchPrefab;
    [SerializeField] private Transform blinkLocation;

    [Header("Freya Prefabs and references")]
    [SerializeField] private GameObject arcadeArrowPrefab;
    [SerializeField] private GameObject arcadeBarragePrefab;
    [SerializeField] private Transform[] arcadeBarrageLocations;
    [SerializeField] private GameObject shieldProtectionObject;
    [SerializeField] private GameObject dashingRoadPrefab;
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private GameObject overchargeMissilePrefab;
    [SerializeField] private GameObject arcadeShootPrefab;
    private AbilityInfo dashingRoadInfo;
    private float speedBoostTemp;
    private Vector3 portalPosition;

    [Header("Alvin Prefabs and references")]
    [SerializeField] private GameObject smokeTossPrefab;

    [Header("UI References")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI[] cooldownTimerText;

    private bool isCharging;
    private bool isSoulDraining;
    private bool isDashingRoad;
    private float attackSwingTimer;
    private float[] abilityCooldownTimer;


    // Start is called before the first frame update
    void Start()
    {
        attackPhase = 1;   

        rigidBody = GetComponent<Rigidbody>();

        abilityCooldownTimer = new float[4];

        playerController = GetComponent<vThirdPersonController>();

        playerStats = GetComponent<JB_PlayerStats>();

        animController = GetComponent<Animator>();

        //audioManager = GameObject.Find("AudioManager").GetComponent<LD_AudioManager>();

        audioManager = Master_Script.instance.audioManager.GetComponent<LD_AudioManager>();

        foreach (AbilityInfo abilities in abilityList)
        {
            //abilities.InitialiseVariables();


            // first set of abilities active to those that match this game object character class
            if (abilities.characterClass == characterClass && !abilities.needChallenge)
            {
                abilities.isActive = true;
            }
        }

        JB_Enemy.DeactivateShield += TurnOffShield;

        JB_Portal.SendPortalLocation += SetPortalLocation;

        HealthComponent.CriticalStrike += CriticalStrike;

        //normalAttack.damage = characterStats.attackDamage;
    }

    // Update is called once per frame
    void Update()
    {
        Timers();
        
        PlayerInput();

        Raycasting();

        UIElements();
    }

    /// <summary>
    /// toggles on the leech for each ability, turned on when vampire item is held
    /// </summary>
    /// <param name="isTrue"></param>
    public void ToggleLeech(bool isTrue)
    {
        if (isTrue)
        {
            foreach (AbilityInfo ability in abilityList)
            {
                ability.isLeeching = true;
            }
        }
        else
        {
            foreach (AbilityInfo ability in abilityList)
            {
                ability.isLeeching = false;
            }
        }
        
    }

    /// <summary>
    /// toggles on the ability to reduce cds when a critical strike occurs, this is active when book item is held
    /// </summary>
    /// <param name="isTrue"></param>
    public void ToggleBook(bool isTrue)
    {
        if (isTrue)
        {
            foreach (AbilityInfo ability in abilityList)
            {
                ability.isBookHeld = true;
            }
        }
        else
        {
            foreach (AbilityInfo ability in abilityList)
            {
                ability.isBookHeld = false;
            }
        }

    }

    private void CriticalStrike(bool isCrit)
    {
        // reduce remaining cds by x amount
        for(int i = 0; i < abilityCooldownTimer.Length; ++i)
        {
            abilityCooldownTimer[i] -= 3f;

            if (abilityCooldownTimer[i] <= 0f)
            {
                abilityCooldownTimer[i] = 0f;
            }
        }
    }

    private void Raycasting()
    {
        // TODO - creates a forward ray cast that detects whether or not player is near the edge, so they do not blink off the edge
    }

    private void UIElements()
    {
        healthBar.fillAmount = (playerStats.health / playerStats.maxHealth);

        for(int i = 0; i < cooldownTimerText.Length; ++i)
        {
            if(abilityCooldownTimer[i] >= 0)
            {
                cooldownTimerText[i].text = abilityCooldownTimer[i].ToString("F0");
            }
            else
            {
                cooldownTimerText[i].text = "";
            }
            

        }
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
        // ability used my zylar, cannot cast while souldraining
        if (isSoulDraining) { return; }
            

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // ability one
            UseCastAbility();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // ability two
            UseUtilityAbility();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // ability three
            UseMobilityAbility();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // ability four
            UseUltimateAbility();
            
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // basic attack
            BasicAttack();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            // testing stuff here
            GetComponent<HealthComponent>().ApplyDamage(normalAttack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            // testing stuff here
            GetComponent<HealthComponent>().RestoreHealth(10f);
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

    private void UseCastAbility()
    {
        UseSelectedAbility(AbilityType.Cast);
    }
  
    /// <summary>
    /// Activiates ability based on ability type / name and active
    /// </summary>
    /// <param name="abilityType"></param>
    private void UseSelectedAbility(AbilityType abilityType)
    {
        foreach (AbilityInfo ability in abilityList)
        {
            if (ability.abilityType == abilityType && ability.isActive)
            {
                Debug.Log(ability.abilityName);
                gameObject.SendMessage(ability.abilityName, ability);
            }
        }
    }

    // TODO - create public functions that activate and deactivate abilities

    #endregion

    #region basic attacks
    private void BasicAttack()
    {
        float attackSpeed = playerStats.attackSpeed;
        normalAttack.damage = playerStats.attackDamage;

        if (attackSwingTimer <= 0)
        {
            // used to create a delay between attacks
            attackSwingTimer = attackSpeed;

            switch (characterClass)
            {
                case CharacterClass.Tansea:
                    // TODO - attack for tansea
                    TanseaAttack();
                    break;
                case CharacterClass.Zylar:
                    ZylarAttacks();
                    // TODO - attack for zylar
                    break;
                case CharacterClass.Freya:
                    // TODO - attack for freya
                    FreyaAttack();
                    break;
                case CharacterClass.Alvin:
                    // TODO - attack for alvin
                    break;
                default:
                    MeleeAttack(DamageType.Normal);
                    break;
                    // melee attack
                    

            }

            // TODO - add in animation
            

            
        }
    }

    private void MeleeAttack(DamageType dmgType)
    {
        var colInfo = Physics.OverlapSphere(meleeAttackArea.position, 5f);

        normalAttack.damageType = dmgType;

        attackPhase++;
        attackPhase %= 3;

        if (colInfo != null)
        {
            foreach (Collider col in colInfo)
            {
                if (col.gameObject.GetComponent<HealthComponent>() && col.gameObject.tag != "Player")
                {
                    
                    col.gameObject.GetComponent<HealthComponent>().ApplyDamage(normalAttack);

                    if(attackPhase == 3 && characterClass == CharacterClass.Tansea)
                    {
                        // do bonus attack for Tansea's third attack
                        TanseaThirdHit(col.gameObject);
                    }
                }
            }
        }
        
    }

    private void TanseaThirdHit(GameObject enemy)
    {
        float randomNumber = Random.Range(0f, 1f);
        if (randomNumber <= 0.2f)
        {
            // stun the target
        }
    }

    private void TanseaAttack()
    {
        switch (attackPhase)
        {
            // TODO - animations for each attack phase
            // attack phases 1, 2, and 3
            case 0:
                MeleeAttack(DamageType.Normal);
                //audioManager.Play("Zylar_Ability_use_V01");
                animController.Play("Attack1");
                break;
            case 1:
                MeleeAttack(DamageType.Normal);
                //audioManager.Play("Zylar_Ability_use_V02");
                animController.Play("Attack2");
                break;
            case 2:
                MeleeAttack(DamageType.Poison);
                //audioManager.Play("Zylar_Ability_use_V03");
                animController.Play("Attack3");
                break;
        }
    }

    private void ZylarAttacks()
    {
        switch (attackPhase)
        {
            // TODO - animations for each attack phase
            // attack phases 1, 2, and 3
            case 0:
                MeleeAttack(DamageType.Normal);
                audioManager.Play("Zylar_Ability_use_V01");
                animController.Play("Attack1");
                break;
            case 1:
                MeleeAttack(DamageType.Normal);
                audioManager.Play("Zylar_Ability_use_V02");
                animController.Play("Attack2");
                break;
            case 2:
                MeleeAttack(DamageType.Poison);
                audioManager.Play("Zylar_Ability_use_V03");
                animController.Play("Attack3");
                break;
        }
        
    }


    private void FreyaAttack()
    {
        switch (attackPhase)
        {
            // TODO - animations for each attack phase
            // attack phases 1, 2, and 3
            case 0:
                //audioManager.Play("Zylar_Ability_use_V01");
                SpawnArcadeShoot(false);
                animController.Play("Attack1");
                break;
            case 1:
                SpawnArcadeShoot(false);
                //audioManager.Play("Zylar_Ability_use_V02");
                animController.Play("Attack2");
                break;
            case 2:
                SpawnArcadeShoot(true);
                //audioManager.Play("Zylar_Ability_use_V03");
                animController.Play("Attack3");
                break;
        }

        attackPhase++;
        attackPhase %= 3;
    }

    private void SpawnArcadeShoot(bool isThirdAttack)
    {
        GameObject obj = Instantiate(arcadeShootPrefab, projectileSpawnPoint.position, arcadeShootPrefab.transform.rotation);

        obj.GetComponent<JB_ArcadeShoot>().arcadeShootInfo = normalAttack;
        obj.GetComponent<JB_ArcadeShoot>().isThirdAttack = isThirdAttack;
    }


    #endregion

    #region Collider / Trigger Functions

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DashingRoad")
        {
            playerStats.moveSpeed *= 1.2f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DashingRoad")
        {
            playerStats.moveSpeed *= 0.8f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isCharging && collision.gameObject.tag == "Enemy")
        {
            rigidBody.velocity = Vector3.zero;

            isCharging = false;

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

    private void RockThrow(AbilityInfo ability)
    {
        float attackDamage = playerStats.attackDamage * ability.damageMultiplier;
        int index = (int)ability.abilityType;

        if(abilityCooldownTimer[index] <= 0)
        {
            
            GameObject obj = Instantiate(rockPrefab, rockThrowSpawnPoint.position, rockThrowSpawnPoint.rotation);
            obj.GetComponent<JB_RockProjectile>().targetLocation = rockThrowTargetLocation;

            obj.GetComponent<JB_RockProjectile>().rockThrowInfo.damage = attackDamage;

            animController.Play("RockThrow");

            abilityCooldownTimer[index] = ability.cooldown;
        }

        
    }

    private void ArcaneShoot(AbilityInfo ability)
    {
        float attackDamage = playerStats.attackDamage * ability.damageMultiplier;
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            GameObject obj = Instantiate(arcaneShootPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            obj.GetComponent<JB_ArcaneShoot>().arcaneShootInfo.damage = attackDamage;

            animController.Play("ArcaneShoot");

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void ArcaneSwing(AbilityInfo ability)
    {
        float attackDamage = playerStats.attackDamage * ability.damageMultiplier; 
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            GameObject obj = Instantiate(arcaneSwingPrefab, meleeAttackArea.position, meleeAttackArea.rotation);

            obj.GetComponent<JB_ArcaneSwing>().arcaneSwingInfo.damage = attackDamage;

            animController.Play("ArcaneSwing");

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void EarthSpeed(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        if(abilityCooldownTimer[index] <= 0)
        {
            StartCoroutine(LaunchPlayer());
            abilityCooldownTimer[index] = ability.cooldown;

            animController.Play("EarthSpeed");
        }
        
    }

    IEnumerator LaunchPlayer()
    {
        playerController.enabled = false;

        Vector3 dir = rockThrowSpawnPoint.forward;
        dir.Normalize();

        rigidBody.velocity = dir * earthSpeedLaunchPower;

        yield return new WaitForSeconds(3f);
        rigidBody.velocity = Vector3.zero;
        playerController.enabled = true;
    }


    private void StoneArmour(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        // Creates a stone skin around himself that will increase his armor with 25 armor for 6 seconds
        
        if (abilityCooldownTimer[index] <= 0)
        {
            StartCoroutine(GetComponent<HealthComponent>().ArmourAdjustment(25f, 6f));
            abilityCooldownTimer[index] = ability.cooldown;

            animController.Play("StoneArmour");
        }

        
    }

    private void Charge(AbilityInfo ability)
    {
        float attackDamage = playerStats.attackDamage * ability.damageMultiplier;
        int index = (int)ability.abilityType;

        chargeInfo = ability;
        chargeInfo.damage = attackDamage;

        if (abilityCooldownTimer[index] <= 0)
        {
            StartCoroutine(Charging());
            abilityCooldownTimer[index] = ability.cooldown;

            animController.Play("Charge");
        }
        
    }

    /// <summary>
    /// Charge ability - player charges forward for 3.5 seconds or when colliding with enemy
    /// </summary>
    /// <returns></returns>
    IEnumerator Charging()
    {
        playerController.enabled = false;
        isCharging = true;
        rigidBody.velocity = transform.forward * earthSpeedLaunchPower;

        yield return new WaitForSeconds(3.5f);

        isCharging = false;
        rigidBody.velocity = Vector3.zero;
        playerController.enabled = true;
        
    }


    /// <summary>
    /// Heals player + attack speed buff
    /// </summary>
    /// <param name="ability"></param>
    private void MotherNature(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            StartCoroutine(playerStats.AttackSpeedBuff(0.35f, ability.dmgDuration));
            StartCoroutine(StartMotherNatureEffect(ability.dmgDuration));

            float healAmount = (playerStats.maxHealth / 2f);

            // checking to see if heal amount is greater than max health, to ensure it does not exceed it
            if(healAmount > playerStats.maxHealth)
            {
                healAmount = playerStats.maxHealth;
            }

            GetComponent<HealthComponent>().RestoreHealth(healAmount);

            animController.Play("MotherNature");

            abilityCooldownTimer[index] = ability.cooldown;
        }
        

    }

    IEnumerator StartMotherNatureEffect(float duration)
    {
        motherNatureEffect.SetActive(true);
        yield return new WaitForSeconds(duration);
        motherNatureEffect.SetActive(false);
    }

    private void ArcaneWack(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            wackInfo = ability;
            // TODO - begin wack animation - use animation sheet to call Wack event

            animController.Play("ArcaneWack");

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

    private void MedusaKiss(AbilityInfo ability)
    {
        float attackDamage = playerStats.attackDamage *ability.damageMultiplier;
        int index = (int)ability.abilityType;

        if(abilityCooldownTimer[index] <= 0)
        {

            GameObject obj = Instantiate(medusaKissPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            obj.GetComponent<JB_MedusaKiss>().medusaKissInfo = ability;
            obj.GetComponent<JB_MedusaKiss>().medusaKissInfo.damage = attackDamage;
            //obj.GetComponent<JB_MedusaAoeCircle>().duration = ability.dmgDuration;

            animController.Play("MedusaKiss");

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void DeadlyThrow(AbilityInfo ability)
    {
        
        float attackDamage = playerStats.attackDamage * ability.damageMultiplier;
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            

            // range attack
            GameObject obj = Instantiate(deadlyThrowPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            obj.GetComponent<JB_DeadlyThrow>().deadlyThrowInfo.damage = attackDamage;

            animController.Play("DeadlyThrow");

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void DeathMark(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        
        if (abilityCooldownTimer[index] <= 0)
        {
            
            GameObject obj = Instantiate(deathMarkPrefab, rockThrowSpawnPoint.position, projectileSpawnPoint.rotation);

            obj.GetComponent<JB_DeathMarkAoe>().deathMarkInfo = ability;
            obj.GetComponent<JB_DeathMarkAoe>().DestroyThis(ability.dmgDuration);

            animController.Play("DeathMark");

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void SoulDrain(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        float duration = ability.dmgDuration;

        if (abilityCooldownTimer[index] <= 0)
        {
            // starting coroutine for turning on / off souldrain ability
            StartCoroutine(SoulDraining(duration, ability));

            animController.Play("SoulDrain");

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
        
        // boolean used to stop player from casting while soul draining
        isSoulDraining = true;
        yield return new WaitForSeconds(duration);

        soulDrainObject.SetActive(false);
        isSoulDraining = false;
    }

    private void Blink(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {

            // move player forward

            transform.position = blinkLocation.GetComponent<JB_Blink>().blinkLocation;

            animController.Play("Blink");

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void DeadlyCloud(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            // calculating distance between current position and new blink position - used to determine the length of poisonous cloud
            float magnitude = Vector3.Distance(transform.position, blinkLocation.GetComponent<JB_Blink>().blinkLocation);

            // move player forward
            transform.position = blinkLocation.GetComponent<JB_Blink>().blinkLocation;

            // spawn poisonous cloud
            StartDeadlyCloud(ability.dmgDuration, magnitude, ability);

            animController.Play("DeadlyCloud");

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }


    /// <summary>
    /// Activates and deactivates poisonous cloud - also assigns required variables
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="magnitude"></param>
    /// <param name="abilityInfo"></param>
    /// <returns></returns>
    IEnumerator StartDeadlyCloud(float duration, float magnitude, AbilityInfo abilityInfo)
    {
        deadlyCloudObject.SetActive(true);
        deadlyCloudObject.GetComponent<JB_DeadlyCloud>().SetLength(magnitude);
        deadlyCloudObject.GetComponent<JB_DeadlyCloud>().deadlyCloudInfo = abilityInfo;
        deadlyCloudObject.GetComponent<JB_DeadlyCloud>().deadlyCloudInfo.damage = playerStats.attackDamage * abilityInfo.damageMultiplier;
        yield return new WaitForSeconds(duration);
        deadlyCloudObject.SetActive(false);
    }

    private void ColdSteel(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        float attackDamage = playerStats.attackDamage * ability.damageMultiplier;

        if (abilityCooldownTimer[index] <= 0)
        {
            GameObject obj = Instantiate(coldSteelPrefab, projectileSpawnPoint.position, projectileSpawnPoint.transform.rotation);

            obj.GetComponent<JB_ColdSteel>().coldSteelInfo = ability;
            obj.GetComponent<JB_ColdSteel>().coldSteelInfo.damage = attackDamage;

            animController.Play("ColdSteel");

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void PestilenceTouch(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        float attackDamage = playerStats.attackDamage * ability.damageMultiplier;

        if (abilityCooldownTimer[index] <= 0)
        {
            GameObject obj = Instantiate(pestilenceTouchPrefab, transform.position, pestilenceTouchPrefab.transform.rotation);

            obj.GetComponent<JB_PestilenceTouch>().pestilenceInfo = ability;
            obj.GetComponent<JB_PestilenceTouch>().pestilenceInfo.damage = attackDamage;

            animController.Play("PestilenceTouch");

            abilityCooldownTimer[index] = ability.cooldown;
        }
        
    }
    #endregion

    #region Freya abilities

    private void ArcadeArrow(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            GameObject obj = Instantiate(arcadeArrowPrefab, projectileSpawnPoint.position, projectileSpawnPoint.transform.rotation);

            obj.GetComponent<JB_ArcadeArrow>().arcadeArrowInfo = ability;
            obj.GetComponent<JB_ArcadeArrow>().arcadeArrowInfo.damage = playerStats.attackDamage * ability.damageMultiplier;

            animController.Play("ArcadeArrow");

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void ArcadeBarrage(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            
            // 7 projectiles to spawn
            for(int i = 0; i < 7; ++i)
            {
                Vector3 randomPos = GetPositionAroundObject(arcadeBarrageLocations[i]);

                GameObject obj = Instantiate(arcadeBarragePrefab, projectileSpawnPoint.position, projectileSpawnPoint.transform.rotation);

                obj.GetComponent<JB_ArcadeBarrage>().targetLocation = randomPos;
                obj.GetComponent<JB_ArcadeBarrage>().arcadeBarrageInfo = ability;
                obj.GetComponent<JB_ArcadeBarrage>().arcadeBarrageInfo.damage = playerStats.attackDamage * ability.damageMultiplier;

                animController.Play("ArcadeBarrage");

                abilityCooldownTimer[index] = ability.cooldown;
            }
            
        }
    }

    /// <summary>
    /// Returns a random position around a given vector
    /// </summary>
    /// <param name="tx"></param>
    /// <returns></returns>
    Vector3 GetPositionAroundObject(Transform point)
    {
        float radius = 2.5f;

        Vector3 offset = Random.insideUnitCircle * radius;

        Vector3 pos = point.position + offset;

        return pos;
    }

    private void ShieldProtection(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            myCoroutine = ShieldProtectionActivate(ability.dmgDuration);
            StartCoroutine(myCoroutine);

            animController.Play("ShieldProtection");

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    /// <summary>
    /// Turns off shield protection game object / stops coroutine - triggered by event 
    /// </summary>
    private void TurnOffShield()
    {
        StopCoroutine(myCoroutine);
        shieldProtectionObject.SetActive(false);
        GetComponent<HealthComponent>().isInvincible = false;
    }

    /// <summary>
    /// Coroutine used to control shield protection if no dmg is taken so it does not last forever
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public IEnumerator ShieldProtectionActivate(float duration)
    {
        shieldProtectionObject.SetActive(true);
        GetComponent<HealthComponent>().isInvincible = true;

        yield return new WaitForSeconds(duration);

        shieldProtectionObject.SetActive(false);
        GetComponent<HealthComponent>().isInvincible = false;
    }

    private void HealingBurst(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            float healAmount = (playerStats.maxHealth * 0.2f);

            GetComponent<HealthComponent>().RestoreHealth(healAmount);

            animController.Play("HealingBurst");

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void DashingRoad(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            GameObject obj = Instantiate(dashingRoadPrefab, transform.position, dashingRoadPrefab.transform.rotation);

            Destroy(obj, ability.dmgDuration);

            animController.Play("DashingRoad");

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    /// <summary>
    /// Creates portal for the player to click on
    /// </summary>
    /// <param name="ability"></param>
    private void Portal(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            GameObject obj = Instantiate(portalPrefab, transform.position, Quaternion.identity);

            animController.Play("Portal");

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    /// <summary>
    /// Moves player once clicked on portal
    /// </summary>
    /// <param name="pos"></param>
    private void SetPortalLocation(Vector3 pos)
    {
        Debug.Log("testing event");
        portalPosition = pos;

        // move player to new location
        transform.position = portalPosition;
    }

    private IEnumerator TeleportPlayer(Vector3 pos)
    {
        playerController.enabled = false;
        transform.position = pos;
        yield return new WaitForSeconds(0.5f);
        playerController.enabled = true;
    }

    private void OVERCHARGE(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            // spawning 5 overcharge missiles
            for(int i = 0; i < 5; ++i)
            {
                // spawning overcharge missiles
                GameObject obj = Instantiate(overchargeMissilePrefab, transform.position, overchargeMissilePrefab.transform.rotation);
            }

            animController.Play("OVERCHARGE");

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    private void ArcadeKnowledge(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        if (abilityCooldownTimer[index] <= 0)
        {
            // reduce cds of all abilities by 3 seconds then restore them back to normal
            // start a coroutine

            StartCoroutine(ReduceCooldowns());

            animController.Play("ArcadeKnowledge");

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

    IEnumerator ReduceCooldowns()
    {

        // we check that this player is Freya, to make sure it's not used with another hero, just a precaution.

        foreach (AbilityInfo abilities in abilityList)
        {
            
            if (abilities.isActive && characterClass == CharacterClass.Freya)
            {
                abilities.cooldown -= 3f;
            }
        }

        yield return new WaitForSeconds(7f);

        foreach (AbilityInfo abilities in abilityList)
        {

            if (abilities.isActive && characterClass == CharacterClass.Freya)
            {
                abilities.cooldown += 3f;
            }
        }
    }
    #endregion

#region Alvin abilities

    private void SmokeToss(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        float attackDamage = playerStats.attackDamage * ability.damageMultiplier;

        if (abilityCooldownTimer[index] <= 0)
        {
            GameObject obj = Instantiate(smokeTossPrefab, transform.position, smokeTossPrefab.transform.rotation);

            obj.GetComponent<JB_SmokeToss>().smokeTossInfo = ability;
            obj.GetComponent<JB_SmokeToss>().smokeTossInfo.damage = attackDamage;

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }
    
    private void ExplosiveThrow(AbilityInfo ability)
    {
        int index = (int)ability.abilityType;

        float attackDamage = playerStats.attackDamage * ability.damageMultiplier;

        if (abilityCooldownTimer[index] <= 0)
        {
            

            abilityCooldownTimer[index] = ability.cooldown;
        }
    }

#endregion


}
