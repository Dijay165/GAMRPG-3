using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{

    public string skillName;
    public float damage;
    public GameObject effects;
    public float coolDownDuration;
    public float effectDuration;
    
    public bool isCooldown;
    public bool isInEffect;
    public Vector2 skillLocation;

    public float animationTime;

    public Sprite skillIcon;
    public bool isLooping;

    

    public virtual void Initialize()
    {
        isCooldown = false;
        isInEffect = false;
    }
    //public virtual void Use(Unit userUnit)
    //{

    //    Debug.Log("ACTIVATEEEEEEEEEE" + skillName);
    //    isCooldown = true;

    //}

    public IEnumerator EffectsAnimation()
    {

        //  GameObject obj = Instantiate(SkillDatabase.effects, skillLocation, Quaternion.identity);

        GameObject obj = Instantiate(effects, skillLocation, Quaternion.identity);

        yield return new WaitForSeconds(animationTime);

        Destroy(obj);



    }

    public virtual IEnumerator CoolDownEnumerator()
    {

        while (isCooldown)
        {
            yield return new WaitForSeconds(this.coolDownDuration);
            isCooldown = false;
            //SkillManager.Instance.skillButtons[SkillManager.Instance.skillRef].interactable = true;
        }
    }

}
