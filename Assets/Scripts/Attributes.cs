using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
public class ModifyStatsEvent : UnityEvent { }
public class Attributes : MonoBehaviour
{

    public float defaultMana;
  

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

    public ModifyStatsEvent onModifyStatsEvent = new ModifyStatsEvent();

  
    public void ResetValues()
    {

        if (TryGetComponent<Unit>(out Unit unit))
        {
            

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
            

            //magicAttack = defaultAttackDamage;
            magicAttack = unit.unitStat.startingMagicAttack;
            attackType = unit.unitStat.attackType;

            baseAttackSpeed = unit.unitStat.baseAttackSpeed;

        }
        InitializeValues();
    }
    public void InitializeValues(int additional = 0)
    {
       
        strength = defaultStrength + additional;
        mana = defaultMana;
        manaRegen = defaultManaRegen ;
        healthRegen = defaultHealthRegen ;
        agility = defaultAgility + additional;
        intelligence = defaultIntelligence + additional;

        armor = defaultArmor ;
        magicResistance = defaultMagicResistance + additional;

        attackDamage = defaultAttackDamage ;
        totalAttackSpeed = (defaultAttackSpeed + bonusAttackSpeed) ;
        attackRange = defaultAttackRange ;

      

        movementSpeed = (defaultMovementSpeed);

        totalArmor = armor + bonusArmor;
        totalMagicResistance = magicResistance + bonusMagicResistance;
    }


   
    public void IncreaseStats()
    {
        totalArmor += 2;
        totalMagicResistance += 2;
        totalAttackSpeed += 2;
        Health health = GetComponent<Health>();
        Mana mana = GetComponent<Mana>();

        health.maxHealth += 50;
        mana.maxMana += 50;


        health.healthRegen += 0.2f;
        mana.manaRegen += 0.2f;


        //   bas
     //   Debug.Log("Increase Stats");
    }
}



