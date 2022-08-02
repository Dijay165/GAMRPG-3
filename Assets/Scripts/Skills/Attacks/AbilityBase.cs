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

    private void Awake()
    {
    

    }
    private void Start()
    {
        
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

    

    public virtual void CastCondition()
    {

    }
}
