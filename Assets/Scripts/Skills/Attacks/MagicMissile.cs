using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "MagicMissile", menuName = "New MagicMissile")]

public class MagicMissile : AbilityBase
{
    public GameObject missilePrefab;
    TargetedDamager targetedDamager;
    Animator animator;
    private void Awake()
    {
        Initialized();
        targetedDamager = GetComponent<TargetedDamager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (targetedDamager.targetHealth != null)
        {
            float distance = Vector3.Distance(targetedDamager.agent.transform.position, targetedDamager.targetHealth.playersParent.position);
            if(distance < castRange)
            {
                canCast = true;
                if (!isCooldown)
                {
                    animator.SetTrigger("CastSkill");
               //     Debug.Log("In Range");
                    //animator.SetTrigger("CastSkill");
                }
               
                
            }
        }
    }
    public override void CastSkill(Unit target)
    {
        base.CastSkill(target);
       // Debug.Log("Magic Missile");
        GameObject obj = Instantiate(missilePrefab);
        HomingProjectile newInstance = obj.GetComponent<HomingProjectile>();
        
        newInstance.attackType = attackType;
        newInstance.transform.position = gameObject.transform.position;
        newInstance.InitializeValues(targetedDamager.targetHealth, damage[skillLevel], 900f);
      //  newInstance.gameObject.transform.SetParent(targetedDamager.targetHealth.playersParent);
      //  isInEffect = true;

    }
    public override IEnumerator CoolDownEnumerator()
    {
       // Debug.Log("Stuff");
        return base.CoolDownEnumerator();

    }
}
