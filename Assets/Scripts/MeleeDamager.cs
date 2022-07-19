using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamager : Damager
{
    public override void DamageTarget()
    {
        if(unit.currentTarget != null)
        {
            if (unit.currentTarget.gameObject.activeSelf)
            {
                float modifiedDamage = unit.currentTarget.gameObject.GetComponent<Health>().CalcDamage(attributes.attackDamage,
                    attributes.weaponType, unit.currentTarget.gameObject.GetComponent<Attributes>().armorType);

             //   Debug.Log(modifiedDamage);
                unit.currentTarget.gameObject.GetComponent<Health>().SubtractHealth(attributes.attackDamage);
          
            }
        }
  
        
    }
}
