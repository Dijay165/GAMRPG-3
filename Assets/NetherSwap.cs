using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetherSwap : ActiveSkill
{
    TargetedDamager targetedDamager;
    Animator animator;
  //  public GameObject swapPrefab;

    private void Awake()
    {
    //    targetedDamager = GetComponent<TargetedDamager>();
      //  animator = GetComponent<Animator>();
        Initialized();
        // float refe = skill.coolDownDuration[1];
        // coolDownDuration = skill.coolDownDuration;
        castRange = skill.effectDuration[0];
    }
    // Update is called once per frame
    void Update()
    {
        CastCondition();
    }

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
        // GameObject obj = Instantiate(swapPrefab);
        //ChannelSkill channelSkill = obj.GetComponent<ChannelSkill>();
        //channelSkill.transform.position = gameObject.transform.position;
        //channelSkill.InitializedValues(targetedDamager.targetHealth, damage[skillLevel], 1, attackType);
     //   StartCoroutine(Swap());


    }

    public IEnumerator Swap()
    {
        Debug.Log("Swap");
    //    Vector3 playerPosition = gameObject.transform.position;
        Vector3 targetPosition = targetedDamager.targetHealth.playersParent.transform.position;

    //    gameObject.transform.position = targetPosition;
      //  targetedDamager.targetHealth.playersParent.transform.position = playerPosition;
        yield return new WaitForSeconds(1f);
      
       
    }

    public override IEnumerator CoolDownEnumerator()
    {

      //  Debug.Log("Cooldown");
        return base.CoolDownEnumerator();
    }
}
