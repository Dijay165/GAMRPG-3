using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveofTerror : ActiveSkill
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
      //  targetedDamager = GetComponent<TargetedDamager>();
    //    animator = GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void Update()
    {
        CastCondition();
    }

    public override void OnActivate(Unit target)
    {
        //  animator.SetTrigger("CastSkill");
        
        base.OnActivate(target);
        //   Debug.Log("WaveofTerror");
        canCast = false;
        GameObject obj = Instantiate(terrorPrefab);
        StraightProjectile straightProjectile = obj.GetComponentInChildren<StraightProjectile>();
       // straightProjectile.transform.position = gameObject.transform.position;
        straightProjectile.Initialization(targetedDamager.targetHealth, skill.damage[skillLevel]);

    }


    public override IEnumerator CoolDownEnumerator()
    {
        return base.CoolDownEnumerator();
    }


    public override void CastCondition()
    {
        base.CastCondition();

      //  Debug.Log(canCast);
        if (canCast)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (!isCooldown)
                {
                    Debug.Log("Not cooldown");
                    animator.SetTrigger("CastSkill");
                }
                else
                {
                    Debug.Log("Cooldown");
                }
            }
        }
        else
        {
            animator.ResetTrigger("CastSkill");
        }
    }
}
