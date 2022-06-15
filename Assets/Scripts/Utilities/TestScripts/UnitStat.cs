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
    public float attack;
    public float defense;
    public float moveSpeed;
    public float healthPoints;
    public Sprite portraitImage;
    [HideInInspector]
    public Faction faction;
    
}
