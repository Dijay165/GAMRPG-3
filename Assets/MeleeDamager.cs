using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamager : Damager
{
    public override void DamageTarget()
    {
        if(unit.currentTarget != null)
        {
            //Debug.Log(gameObject.name + " MELEE DAMAGED " + unit.currentTarget.gameObject.name + " - "+ attributes.attackDamage);
            if (unit.currentTarget.gameObject.activeSelf)
            {
              //  Debug.Log("MELEE DAMAGED 2");
                unit.currentTarget.gameObject.GetComponent<Health>().SubtractHealth(attributes.attackDamage);
                //Debug.Log("Damage");
            }
        }
        //else
        //{
        //    unit.currentTarget = null;
        //    Destroy(gameObject);
        //}
        
    }
}
