using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Unit Stats", menuName = "New Stats")]
public class UnitStat : ScriptableObject
{
    public float attack;
    public float defense;
    public float moveSpeed;
    public float healthPoints;
    public Sprite portraitImage;
}
