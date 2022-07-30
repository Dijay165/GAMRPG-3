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
                Debug.Log("In Range");
                canCast = true;
                
            }
            else
            {
                Debug.Log("Not Range");
            }

        }
    }
    public override void CastSkill(Unit target)
    {
        base.CastSkill(target);
        Debug.Log("Magic Missile");
        GameObject obj = Instantiate(missilePrefab);
    }
}
