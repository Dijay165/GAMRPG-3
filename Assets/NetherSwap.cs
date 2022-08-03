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
        GameObject obj = Instantiate(prefab, target.gameObject.transform.position, Quaternion.identity);
        ChannelSkill channelSkill = obj.GetComponent<ChannelSkill>();
        channelSkill.transform.position = target.gameObject.transform.position;
        channelSkill.targetTransform = targetedDamager.targetHealth.playersParent;
        channelSkill.InitializedValues(targetedDamager.targetHealth, 0, 1, attackType);
        channelSkill.gameObject.transform.SetParent(targetedDamager.targetHealth.playersParent);
        //StartCoroutine(Swap());


    }

    public IEnumerator Swap()
    {
        Debug.Log("Swap");
    //    Vector3 playerPosition = gameObject.transform.position;
        //Vector3 targetPosition = targetedDamager.targetHealth.playersParent.transform.position;

    //    gameObject.transform.position = targetPosition;
      //  targetedDamager.targetHealth.playersParent.transform.position = playerPosition;
        yield return new WaitForSeconds(1f);
      
       
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
