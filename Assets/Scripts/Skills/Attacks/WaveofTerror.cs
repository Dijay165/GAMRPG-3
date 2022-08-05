using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveTerror", menuName = "WaveTerror")]

public class WaveofTerror : ActiveSkill
{
    // Start is called before the first frame update
    TargetedDamager targetedDamager;
    Animator animator;
   // public GameObject terrorPrefab;



    public override void OnActivate(Unit target)
    {
        //  animator.SetTrigger("CastSkill");
        
        base.OnActivate(target);

        targetedDamager = target.gameObject.GetComponent<TargetedDamager>();

        //   Debug.Log("WaveofTerror");
        canCast = false;
        GameObject obj = Instantiate(prefab, target.gameObject.transform.position, Quaternion.identity);
        StraightProjectile straightProjectile = obj.GetComponentInChildren<StraightProjectile>();
        // straightProjectile.transform.position = gameObject.transform.position;
        // straightProjectile.
      //  straightProjectile.transform.SetParent(targetedDamager.transform);
        straightProjectile.Initialization(targetedDamager.targetHealth, damage[skillLevel]);

    }


    public override IEnumerator CoolDownEnumerator()
    {
        return base.CoolDownEnumerator();
    }


    //public override void CastCondition()
    //{
    //    base.CastCondition();

    //  //  Debug.Log(canCast);
    //    if (canCast)
    //    {
    //        if (Input.GetKeyDown(KeyCode.W))
    //        {
    //            if (!isCooldown)
    //            {
    //                Debug.Log("Not cooldown");
    //                animator.SetTrigger("CastSkill");
    //            }
    //            else
    //            {
    //                Debug.Log("Cooldown");
    //            }
    //        }
    //    }
    //    else
    //    {
    //        animator.ResetTrigger("CastSkill");
    //    }
    //}
}
