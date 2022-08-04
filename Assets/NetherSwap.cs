using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NetherSwap", menuName = "NetherSwap")]

public class NetherSwap : ActiveSkill
{
    TargetedDamager targetedDamager;
    Animator animator;
  //  public GameObject swapPrefab;

   
    public override void CastCondition()
    {
        base.CastCondition();
        if (targetedDamager.targetHealth != null)
        {
            float distance = Vector3.Distance(targetedDamager.agent.transform.position, targetedDamager.targetHealth.playersParent.position);
            if (distance < castRange)
            {
               // canCast = true;
                if (!isCooldown)
                {

                    animator.SetTrigger("CastSkill");
                    //     Debug.Log("In Range");
                    //animator.SetTrigger("CastSkill");
                }


            }
        }
    }

    public override void OnActivate(Unit target)
    {
        base.OnActivate(target);
        canCast = false;

        //Skill(target);

        targetedDamager = target.gameObject.GetComponent<TargetedDamager>();

        Vector3 playerPosition = target.transform.position;
        Vector3 targetPosition = targetedDamager.targetHealth.playersParent.transform.position;

        target.transform.position = targetPosition;
        targetedDamager.targetHealth.playersParent.transform.position = playerPosition;


        // CoroutineSetup.instance.StartCoroutine(Swap(target));
    }

    public IEnumerator Swap(Unit unit)
    {
    
        yield return new WaitForSeconds(1f);
        Debug.Log("Swap");
        targetedDamager = unit.gameObject.GetComponent<TargetedDamager>();

        Vector3 playerPosition = unit.transform.position;
        Vector3 targetPosition = targetedDamager.targetHealth.playersParent.transform.position;

        unit.transform.position = targetPosition;
        targetedDamager.targetHealth.playersParent.transform.position = playerPosition;


     //   CoroutineSetup.instance.StartCoroutine(CoolDownEnumerator());
    }

    //public void Skill(Unit unit)
    //{
    //    Vector3 playerPosition = unit.transform.position;
    //    Vector3 targetPosition = targetedDamager.targetHealth.transform.position;

    //    unit.transform.position = targetPosition;
    //    targetedDamager.targetHealth.playersParent.transform.position = playerPosition;
    //}

    public override IEnumerator CoolDownEnumerator()
    {

      //  Debug.Log("Cooldown");
        return base.CoolDownEnumerator();
    }
}
