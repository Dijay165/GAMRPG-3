using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Skill", menuName = "New Skills")]
public abstract class Skill : ScriptableObject
{

    public string skillName;
    public GameObject effects;
    public List<float> damage;

    public List<float> coolDownDuration;
    public List<float> effectDuration;

    public List<float> manaCost;

    public bool isCooldown;
    public bool isInEffect;
     public Vector3 skillLocation;
  //  public Transform targetTransform;
    public float animationTime;

    public Sprite skillIcon;
  //  public bool isLooping;
    public KeyCode pressButton;
    public bool canDispel;
    public int skillLevel;
    public AttackType attackType;
    


   
    public virtual void Initialize(List<float> p_damage, List<float> p_coolDownDuration, 
        List<float> p_effectDuration, List<float> p_manaCost, AttackType p_attackType, KeyCode p_pressButton)
    {
        p_damage = this.damage;
        p_coolDownDuration = this.coolDownDuration;
        p_effectDuration = this.effectDuration;
        p_manaCost = this.manaCost;
        p_attackType = this.attackType;
        p_pressButton = this.pressButton;
        //this.damage = p_damage;
        //this.coolDownDuration = p_coolDownDuration;
        //this.effectDuration = p_effectDuration;
        //this.manaCost = p_manaCost;
        //this.attackType = p_attackType;
        //this.pressButton = p_pressButton;

        isCooldown = false;
        isInEffect = false;
    }
    public virtual void CastSkill(Unit userUnit)
    {
        if (!isCooldown)
        {
         //   Debug.Log("ACTIVATEEEEEEEEEE" + skillName);
            isCooldown = true;
            isInEffect = true;
        }
     

    }

    public IEnumerator EffectsAnimation()
    {

         GameObject obj = Instantiate(effects, skillLocation, Quaternion.identity);

    //    GameObject obj = Instantiate(effects, targetTransform.position, Quaternion.identity);

        yield return new WaitForSeconds(animationTime);

        Destroy(obj);

    }

    public virtual IEnumerator CoolDownEnumerator()
    {

        while (isCooldown)
        {
            Debug.Log("isCooldown");
            yield return new WaitForSeconds(this.coolDownDuration[skillLevel]);
            isCooldown = false;
            //SkillManager.Instance.skillButtons[SkillManager.Instance.skillRef].interactable = true;
        }
        Debug.Log("can cast again");
    }

}
