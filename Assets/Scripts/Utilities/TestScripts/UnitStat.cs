using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    Radiant, 
    Dire
}

[CreateAssetMenu(fileName = "Unit Stats", menuName = "New Stats")]
public class UnitStat : ScriptableObject
{
    
    public Sprite portraitImage;
    public Sprite iconImage;

    public float startingMaxHP;

    public int startingStrength;
    public int startingAgility;
    public int startingIntelligence;

    public int startingArmor;
    public int startingMagicResistance;

    public int startingAttackDamage;
    public float startingAttackSpeed;
    public float startingAttackRange;

    public float startingMovementSpeed;

}
