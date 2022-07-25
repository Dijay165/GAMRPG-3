using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamager : Damager
{
    float modifiedDamage;
    public override void DamageTarget()
    {
        if(unit.currentTarget != null)
        {
            if (unit.currentTarget.gameObject.activeSelf)
            {
                //if (attributes.attackType == AttackType.Physical)
                //{
                //    modifiedDamage = unit.currentTarget.gameObject.GetComponent<Health>().CalcDamage(attributes.attackDamage,
                //    attributes.weaponType, unit.currentTarget.gameObject.GetComponent<Attributes>().armorType);
                //}
                //else
                //{
                //    modifiedDamage = unit.currentTarget.gameObject.GetComponent<Health>().CalcDamage(gameObject.GetComponent<Health>().
                //        MagicResistance(attributes.magicAttack),
                //      attributes.weaponType, unit.currentTarget.gameObject.GetComponent<Attributes>().armorType);
                //}

                modifiedDamage = DamageType();
                Debug.Log(modifiedDamage);

                unit.currentTarget.gameObject.GetComponent<Health>().SubtractHealth(modifiedDamage);
          
            }
        }
  
        
    }
}
