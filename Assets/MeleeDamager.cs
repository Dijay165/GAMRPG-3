using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamager : Damager
{
    public override void DamageTarget()
    {
        int damage = 0;
        if (gameObject.activeSelf)
        {
            damage = attributes.attackDamage;
        }

        if (unit.currentTarget.gameObject.activeSelf)
        {
            unit.currentTarget.gameObject.GetComponent<Health>().SubtractHealth(damage);
            Debug.Log(unit.gameObject.name + " - " + damage + " - " + unit.currentTarget.gameObject.name);
        }
    }
}
