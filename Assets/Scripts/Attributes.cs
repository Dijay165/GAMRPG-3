using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Attributes : MonoBehaviour
{
    public int skillPoints;

    private int defaultStrength;
    private int defaultAgility;
    private int defaultIntelligence;

    private int defaultArmor;
    private int defaultMagicResistance;

    private int defaultAttackDamage;
    private float defaultAttackSpeed;
    private float defaultAttackRange;

    private float defaultMovementSpeed;



    public int strength;
    public int agility;
    public int intelligence;

    public float armor;
    public int magicResistance;

    public float attackDamage;
    public float attackSpeed;
    public float attackRange;

    public float movementSpeed;
    public WeaponType weaponType;
    public ArmorType armorType;

    public Action OnAttributesUpdated;

    public float bonusArmor;

    public void ResetValues()
    {

        if (TryGetComponent<Unit>(out Unit unit))
        {
            skillPoints = 0;

            defaultStrength = unit.unitStat.startingStrength;
            defaultAgility = unit.unitStat.startingAgility;
            defaultIntelligence = unit.unitStat.startingIntelligence;

            defaultArmor = unit.unitStat.startingArmor;
            defaultMagicResistance = unit.unitStat.startingMagicResistance;

            defaultAttackDamage = unit.unitStat.startingAttackDamage;
            defaultAttackSpeed = unit.unitStat.startingAttackSpeed;
            defaultAttackRange = unit.unitStat.startingAttackRange;

            defaultMovementSpeed = unit.unitStat.startingMovementSpeed;

            weaponType = unit.unitStat.weaponType;
            armorType = unit.unitStat.armorType;

            armor = defaultArmor + bonusArmor;

        }
        InitializeValues();
    }
    public void InitializeValues()
    {

        strength = defaultStrength;
        agility = defaultAgility;
        intelligence = defaultIntelligence;

        armor = defaultArmor;
        magicResistance = defaultMagicResistance;

        attackDamage = defaultAttackDamage;
        attackSpeed = defaultAttackSpeed;
        attackRange = defaultAttackRange;

        movementSpeed = defaultMovementSpeed;


    }


   
    };










//public static Dictionary<WeaponType, Dictionary<ArmorType, Func<float, float>>> damageTypes = new Dictionary<WeaponType, Dictionary<ArmorType, Func<float, float>>>()
//{
//    {
//        WeaponType.Basic, new Dictionary<ArmorType, Func<float, float>>
//        {
//           ArmorType.Basic, ArmorType.Fortified => {
//        } 
//    }
//};

