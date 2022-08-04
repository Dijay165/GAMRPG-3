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
        if (this.canCast)
        {
          //  Debug.Log("OnActivate");
            this.isCooldown = true;
            this.isInEffect = true;
            target.GetComponent<Mana>().SubtractMana(manaCost[skillLevel]);
          //  CoroutineSetup.instance.StartCoroutine(CoolDownEnumerator());
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
      

        while (isCooldown)
        {
           //  Debug.Log(coolDownDuration[skillLevel]);
            yield return new WaitForSeconds(this.coolDownDuration[skillLevel]);
            this.canCast = true;
            this.isCooldown = false;
            Debug.Log("can cast again");
            //SkillManager.Instance.skillButtons[SkillManager.Instance.skillRef].interactable = true;
        }

    }


    private void OnDisable()
    {
        this.canCast = true;
        this.isCooldown = false;
        this.isInEffect = false;
    }
}
