using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifier : AttributeModifier
{
    // Start is called before the first frame update
    public float attackDamageModifier;
    public float attackRangeModifier;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ApplyModification()
    {
        base.ApplyModification();
      //  Debug.Log("Before attack Damage " + attributes.attackDamage + " Before attack Range " + attributes.attackRange + gameObject.name);
        attributes.attackDamage += attackDamageModifier;
        attributes.attackRange += attackRangeModifier;
       // Debug.Log("after attack Damage " + attributes.attackDamage + " after attack Range " + attributes.attackRange + gameObject.name);
    }

    public override void RemoveModification()
    {
        base.RemoveModification();
        attributes.attackDamage -= attackDamageModifier;
        attributes.attackRange -= attackRangeModifier;
    }

}
