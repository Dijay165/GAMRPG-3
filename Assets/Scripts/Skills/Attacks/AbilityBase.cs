using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkillType
{
    Active,
    Passive
}
public abstract class AbilityBase : ScriptableObject
{
    // Start is called before the first frame update
    public int skillLevel;
    public List<float> damage;

    public List<float> coolDownDuration;
    public List<float> effectDuration;

    public List<float> manaCost;

    protected AttackType attackType;

    public bool isCooldown;
    public bool isInEffect;
    public bool canCast;
    public float castRange;
    public Sprite skillIcon;

    //[HideInInspector]
    public KeyCode keyCode;

    public Skill skill;

    public SkillType skillType;

    public int maxSkillLevel;
    public bool isMaxLevel;
    public bool isUnlock;

    private void Awake()
    {
        //maxSkillLevel = manaCost.Count;
        //skillLevel = 0;
    }
   


    
    protected void Initialized()
    {

        this.damage = skill.damage;
        this.coolDownDuration = skill.coolDownDuration;
        this.effectDuration = skill.effectDuration;
        this.manaCost = skill.manaCost;
        this.attackType = skill.attackType;
        this.keyCode = skill.pressButton;
        this.skillType = skill.SkillType;

    }


    //public virtual void OnActivate(Unit target)
    //{
    //    if (canCast)
    //    {
    //        //  Debug.Log("OnActivate");
    //        isCooldown = true;
    //        isInEffect = true;
    //    }


    //}

    public abstract AbilityBase data { get; }


    public bool CastCondition(Transform self, Transform target)
    {
        bool cond = false; 
        if (target != null)
        {
            float distance = Vector3.Distance(self.position, target.position);
            if (distance < castRange)
            {
                cond = true;
            }
            else
            {
                cond =  false;
            }
        }

        return cond;
        //  if(unit.transform.)
    }
}
