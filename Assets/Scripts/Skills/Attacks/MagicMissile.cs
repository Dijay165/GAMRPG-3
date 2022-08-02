using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicMissile", menuName = "New MagicMissile")]

public class MagicMissile : ActiveSkill
{
    

     TargetedDamager targetedDamager;
     Animator animator;


    public override void OnActivate(Unit target)
    {
        base.OnActivate(target);


        Debug.Log("Magic Missile");

        targetedDamager = target.gameObject.GetComponent<TargetedDamager>();

        GameObject obj = Instantiate(prefab, target.gameObject.transform.position, Quaternion.identity);
        HomingProjectile newInstance = obj.GetComponent<HomingProjectile>();

        newInstance.attackType = attackType;
        newInstance.transform.position = target.gameObject.transform.position;
        newInstance.InitializeValues(targetedDamager.targetHealth, damage[skillLevel], 900f);
        newInstance.gameObject.transform.SetParent(targetedDamager.targetHealth.playersParent);
        //  isInEffect = true;
    }
    public override IEnumerator CoolDownEnumerator()
    {
       // Debug.Log("Stuff");
        return base.CoolDownEnumerator();

    }


    public override void CastCondition()
    {
        base.CastCondition();
        if (targetedDamager.targetHealth != null)
        {
            float distance = Vector3.Distance(targetedDamager.agent.transform.position, targetedDamager.targetHealth.playersParent.position);
            if (distance < castRange)
            {
           //     canCast = true;
                if (!isCooldown)
                {
                    
                    animator.SetTrigger("CastSkill");
                    //     Debug.Log("In Range");
                    //animator.SetTrigger("CastSkill");
                }


            }
        }
    }
}
