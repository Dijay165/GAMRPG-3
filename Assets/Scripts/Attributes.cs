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
    
    public Action OnAttributesUpdated;
    // Start is called before the first frame update
    void Start()
    {
        //450 + (20 * Strength)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
