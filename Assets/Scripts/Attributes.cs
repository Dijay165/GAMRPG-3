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

    public int armor;
    public int magicResistance;

    public int attackDamage;
    public float attackSpeed;
    public float attackRange;

    public float movementSpeed;

    public Action OnAttributesUpdated;

   
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
}
