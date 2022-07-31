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
    }

    public override void CastSkill(Unit target)
    {
        base.CastSkill(target);
        Debug.Log("WaveofTerror");
        GameObject obj = Instantiate(terrorPrefab);
        StraightProjectile straightProjectile = obj.GetComponent<StraightProjectile>();
        straightProjectile.transform.position = gameObject.transform.position;


    }


    public override IEnumerator CoolDownEnumerator()
    {
        return base.CoolDownEnumerator();
    }
}
