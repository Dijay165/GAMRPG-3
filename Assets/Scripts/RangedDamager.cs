using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedDamager : Damager
{
    [SerializeField] private Transform firePoint;
   // [SerializeField] private HomingProjectile prefab;
    public override void DamageTarget()
    {
        float damage = 0;
        if (gameObject.activeSelf)
        {
            damage = attributes.attackDamage;
        }

        if(unit.currentTarget != null)
        {
            if (unit.currentTarget.gameObject.activeSelf)
            {
                HomingProjectile newInstance = ProjectilePool.pool.Get();// Instantiate(prefab);
                newInstance.transform.position = firePoint.position;
                newInstance.InitializeValues(unit.currentTarget,damage,250);

            }
        }
    

       
    }
}
