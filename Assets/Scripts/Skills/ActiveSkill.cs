using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Skills", menuName = "New Attack Skills")]
public class ActiveSkill : AbilityBase
{
    //public float castRange;
    public GameObject prefab;
  

    public virtual void OnActivate(Unit target)
    {
        if (canCast)
        {
          //  Debug.Log("OnActivate");
            isCooldown = true;
            isInEffect = true;
        }


    }
     
    public override AbilityBase data
    {
        get
        {
            return data;
        }

    }

    public virtual IEnumerator CoolDownEnumerator()
    {
        Debug.Log("isCooldown");

        while (isCooldown)
        {
            // Debug.Log(coolDownDuration[skillLevel]);
            yield return new WaitForSeconds(coolDownDuration[skillLevel]);
            canCast = true;
            isCooldown = false;
            //SkillManager.Instance.skillButtons[SkillManager.Instance.skillRef].interactable = true;
        }
        //Debug.Log("can cast again");
    }

}
