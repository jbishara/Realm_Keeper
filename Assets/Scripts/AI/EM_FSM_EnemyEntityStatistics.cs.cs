using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public sealed class EM_FSM_EnemyEntityStatistic
{
    public enum EnemyTypes
    {
        Bagooblin,
        Troll,
        Cyclops,
        Stone_Golem,
        Elemental,
        Crystal_Golem,
        Small_Imp,
        Cultist,
        Noctua,
        TestEnemy

    }

    public enum AiEnemyFlag
    {
        None,
        Boss

    }


    public enum Abilitys
    {
        TestAbility1,
        TestAbility2
    }


    public enum AttackTypes
    {
        Melee,
        Ranged,
        Mixed
    }

    public EnemyTypes EnemyType;
    public AttackTypes AttackType;
    public Abilitys BoundAbility1;
    public Abilitys BoundAbility2;
    public Abilitys BoundAbility3;
    public AiEnemyFlag EnemyFlag;
    public float AttackDamage;
    public float Health;
    public float HealthRegen;
    public float Armor;
    public float AttackSpeed;
    public float MovementSpeed;


    public float VisionRange = 20f;
    public float VisionAngle = 60f;


    public float CurrentHealth;







}

