using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    Radiant, 
    Dire
}

public enum WeaponType
{
    Basic,
    Pierce,
    Siege, 
    Hero
}

public enum ArmorType
{
    Basic,
    Fortified,
    Hero
}

public enum AttackType
{
    Physical,
    Magical
}

[CreateAssetMenu(fileName = "Unit Stats", menuName = "New Stats")]
public class UnitStat : ScriptableObject
{

    public int startingLevel;
    public Sprite portraitImage;
    public Sprite iconImage;

    public float startingMaxHP;
    public float startingMaxMana;

    public int startingStrength;
    public int startingAgility;
    public int startingIntelligence;

    public int startingArmor;
    public int startingMagicResistance;


    public int startingAttackDamage;
    public float startingAttackSpeed;
    public float startingAttackRange;

    public float startingMovementSpeed;

    public float healthRegeneration;
    public float manaRegeneration;

    public WeaponType weaponType;
    public ArmorType armorType;
    public AttackType basicAttack;


}
