using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterClass
{
    Tansea,
    Zylar,
    Freya,
    Alvin
}
public enum AbilityType
{
    Damage,
    Utility,
    Mobility,
    Ultimate
}

public enum TanseaAbilities
{
    RockThrow,
    ArcaneShoot,
    ArcaneSwing,
    StoneSkin,
    EarthSpeed,
    Charge,
    MotherNature,
    Wack
}

public enum ZylarAbilities
{
    MedusasKiss,
    DeadlyThrow,
    DeathMarks,
    SoulDrain,
    Blink,
    DeadlyCloud,
    ColdSteel
}

public enum FreyaAbilities
{
    ArcadeArrow,
    ArcadeBarrage,
    ShieldOfProtection,
    HealingBurst,
    DashingRoad,
    Portal,
    Overcharge,
    ArcadeKnowledge
}

public enum DevilAbilities
{
    SmokeToss,
    ExplosiveThrow,
    Anger,
    Calm,
    BurstOfSpeed,
    BlinkingDagger,
    FlurryOfHatred,
    DeadlyAim
}



[System.Serializable]
public class CharacterAbilities 
{
    [SerializeField] private AbilityInfo ability;

    private bool m_isActive;
    private bool m_needChallenge;
    private string m_abilityName;
    private float m_cooldown;
    private AbilityType m_abilityType;
    private CharacterClass m_characterClass;

    /// <summary>
    /// Cooldown
    /// </summary>
    public float cooldown { get { return m_cooldown; } }

    /// <summary>
    /// A boolean to determine whether or not this ability needs a challenge to acquire
    /// </summary>
    public bool needChallenge { get { return m_needChallenge; } }

    /// <summary>
    /// Character that uses this ability
    /// </summary>
    public CharacterClass characterClass { get { return m_characterClass; }  }

    /// <summary>
    /// A boolean to determine whether or not this ability is active
    /// </summary>
    public bool isActive { get { return m_isActive; } set { m_isActive = value; } }

    /// <summary>
    /// Name of ability
    /// </summary>
    public string abilityName { get { return m_abilityName; } }

    /// <summary>
    /// Ability type
    /// </summary>
    public AbilityType abilityType { get { return m_abilityType; }  }


    public void InitialiseVariables()
    {
        m_isActive = false;
        m_abilityName = ability.abilityName;
        m_abilityType = ability.abilityType;
        m_characterClass = ability.characterClass;
        m_needChallenge = ability.needChallenge;
        m_cooldown = ability.cooldown;
    }

    public void TestingThisMethod()
    {
        Debug.Log("FUNCTION REACHED");
    }
    
}
