using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Attributes : MonoBehaviour
{
    public int skillPoints;

    public int strength;
    public int agility;
    public int intelligence;

    public int armor;
    public int magicResistance;

    public int attackDamage;
    public float attackSpeed;
    public float attackRange;

    public float movementSpeed;
    public float maxHp;
    public float currentHp;

    public Sprite portraitImage;
    
    public Action OnAttributesUpdated;

    public Health hp;

    // Start is called before the first frame update

    private void Awake()
    {
        hp = GetComponent<Health>();
    }

}
