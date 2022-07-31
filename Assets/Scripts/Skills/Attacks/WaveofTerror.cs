using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveofTerror : AbilityBase
{
    // Start is called before the first frame update
    TargetedDamager targetedDamager;
    Animator animator;
    public GameObject terrorPrefab;

    private void Awake()
    {
        Initialized();
        canCast = true;
    }
    void Start()
    {
        targetedDamager = GetComponent<TargetedDamager>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCooldown)
        {
            animator.SetTrigger("CastSkill");
            //     Debug.Log("In Range");
            //animator.SetTrigger("CastSkill");
        }
        //float distance = Vector3.Distance(targetedDamager.agent.transform.position, targetedDamager.targetHealth.playersParent.position);
        //if (distance < castRange)
        //{
            


        //}
    }

    public override void CastSkill(Unit target)
    {
        base.CastSkill(target);
        Debug.Log("WaveofTerror");
        GameObject obj = Instantiate(terrorPrefab);
    }


    public override IEnumerator CoolDownEnumerator()
    {
        return base.CoolDownEnumerator();
    }
}
