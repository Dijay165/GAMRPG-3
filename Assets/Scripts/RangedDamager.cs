using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedDamager : Damager
{
    [SerializeField] private Transform firePoint;
   // [SerializeField] private HomingProjectile prefab;
    public override void DamageTarget()
    {
        if(unit.currentTarget != null)
        {
            float modifiedDamage = 0;
            if (gameObject.activeSelf)
            {

                //if(attributes.attackType == AttackType.Physical)
                //{
                //    modifiedDamage = unit.currentTarget.gameObject.GetComponent<Health>().CalcDamage(attributes.attackDamage,
                //      attributes.weaponType, unit.currentTarget.gameObject.GetComponent<Attributes>().armorType);
                //}
                //else
                //{
                //    modifiedDamage = unit.currentTarget.gameObject.GetComponent<Health>().CalcDamage(gameObject.GetComponent<Health>().
                //        MagicResistance(attributes.magicAttack),
                //      attributes.weaponType, unit.currentTarget.gameObject.GetComponent<Attributes>().armorType);
                //}


                modifiedDamage = DamageType();
                Debug.Log(modifiedDamage);
                // unit.currentTarget.gameObject.GetComponent<Health>().

                //  Debug.Log(gameObject.name + " Modified Damage " + modifiedDamage);
            }

            if (unit.currentTarget.gameObject.activeSelf)
            {
                HomingProjectile newInstance = ProjectilePool.pool.Get();// Instantiate(prefab);
                newInstance.weaponType = attributes.weaponType;
                newInstance.armorType = attributes.armorType;
                newInstance.transform.position = firePoint.position;

                newInstance.InitializeValues(unit.currentTarget,modifiedDamage,250);

            }
        }
    

       
    }
}
