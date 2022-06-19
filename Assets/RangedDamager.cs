using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedDamager : Damager
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private HomingProjectile prefab;
    public override void DamageTarget()
    {
        int damage = 0;
        if (gameObject.activeSelf)
        {
            damage = attributes.attackDamage;
        }

        if (unit.currentTarget.gameObject.activeSelf)
        {
            HomingProjectile newInstance = Instantiate(prefab);
            newInstance.transform.position = firePoint.position;
            newInstance.targetUnit = unit.currentTarget;
            newInstance.damage = damage;
            newInstance.target = unit.currentTarget.transform;
            newInstance.Homing();

            //unit.currentTarget.gameObject.GetComponent<Health>().SubtractHealth(damage);
            //Debug.Log(unit.gameObject.name + " - " + damage + " - " + unit.currentTarget.gameObject.name);
        }
    }
}
