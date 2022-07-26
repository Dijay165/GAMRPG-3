using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Attributes : MonoBehaviour
{
    public int level;
    public float defaultMana;
    public int skillPoints;

    private int defaultStrength;
    private int defaultAgility;
    private int defaultIntelligence;

    private int defaultArmor;
    private int defaultMagicResistance;

    private int defaultAttackDamage;
    private float defaultAttackSpeed;
    public float baseAttackSpeed; 
    private float defaultAttackRange;

    private float defaultMovementSpeed;

    public float agiFlatModifiers; 



    public int strength;
    public int agility;
    public int intelligence;

    public float armor;
    public int magicResistance;
    public float magicAttack;

    public float attackDamage;
    public float totalAttackSpeed;
    public float attackRange;
    public float bonusAttackSpeed;

    public float mana;

    public float movementSpeed;
    public WeaponType weaponType;
    public ArmorType armorType;
    public AttackType attackType;


    public Action OnAttributesUpdated;

    public float bonusArmor;

    public float totalArmor;
    public float bonusMagicResistance;
    public float totalMagicResistance;

    public float defaultHealthRegen;
    public float defaultManaRegen;

    public float healthRegen;
    public float manaRegen;

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

            defaultManaRegen = unit.unitStat.manaRegeneration;
            defaultHealthRegen = unit.unitStat.healthRegeneration;
            defaultMana = unit.unitStat.startingMaxMana;
            level = unit.unitStat.startingLevel;

            //magicAttack = defaultAttackDamage;
            magicAttack = unit.unitStat.startingMagicAttack;
            attackType = unit.unitStat.attackType;

            baseAttackSpeed = unit.unitStat.baseAttackSpeed;

        }
        InitializeValues();
    }
    public void InitializeValues()
    {
       
        strength = defaultStrength;
        mana = defaultMana;
        manaRegen = defaultManaRegen;
        healthRegen = defaultHealthRegen;
        agility = defaultAgility;
        intelligence = defaultIntelligence;

        armor = defaultArmor;
        magicResistance = defaultMagicResistance;

        attackDamage = defaultAttackDamage;
        totalAttackSpeed = defaultAttackSpeed + bonusAttackSpeed;
        attackRange = defaultAttackRange;

      

        movementSpeed = defaultMovementSpeed;

        totalArmor = armor + bonusArmor;
        totalMagicResistance = magicResistance + bonusMagicResistance;
    }


   
    };



