using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [HideInInspector]
    public Unit unit;
    [HideInInspector]
    public Attributes attributes;


    private void Awake()
    {
        unit = GetComponent<Unit>();
        attributes = GetComponent<Attributes>();
  
    }

    public virtual void DamageTarget()
    {
        //unit.currentTarget.SubtractHealth(10);
    }

    
    public float DamageType()
    {
        // Debug.Log("Stuff: ");
        float finalModifiedDamage = 0;
        float modifiedDamage = 0;
        switch (attributes.attackType)
        {
            case AttackType.Physical:
                modifiedDamage = unit.currentTarget.gameObject.GetComponent<Health>().CalculateDamage(attributes.attackDamage,
                    attributes.weaponType, unit.currentTarget.gameObject.GetComponent<Attributes>().armorType);

                finalModifiedDamage = unit.currentTarget.gameObject.GetComponent<Health>().findDamage(attributes.attackType, modifiedDamage);
              //  Debug.Log("Physical: " + modifiedDamage);
                break;

            case AttackType.Magical:
              
                modifiedDamage = unit.currentTarget.gameObject.GetComponent<Health>().CalculateDamage(gameObject.GetComponent<Health>().
                       MagicResistance(attributes.magicAttack),
                     attributes.weaponType, unit.currentTarget.gameObject.GetComponent<Attributes>().armorType);

                finalModifiedDamage = unit.currentTarget.gameObject.GetComponent<Health>().findDamage(attributes.attackType, modifiedDamage);
              //  Debug.Log("Magical: " + modifiedDamage);
                break;
        }
        return finalModifiedDamage;
    }
}
