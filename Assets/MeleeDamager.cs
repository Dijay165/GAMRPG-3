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
                unit.currentTarget.gameObject.GetComponent<Health>().SubtractHealth(attributes.attackDamage);
                //Debug.Log("Damage");
            }
        }
        else
        {
            unit.currentTarget = null;
            Destroy(gameObject);
        }
        
    }
}
