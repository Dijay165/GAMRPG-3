using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkillType
{
    Active,
    Passive
}
public class AbilityBase : MonoBehaviour
{
    // Start is called before the first frame update
    public int skillLevel;
    protected List<float> damage;

    protected List<float> coolDownDuration;
    protected List<float> effectDuration;

    protected List<float> manaCost;

    protected AttackType attackType;

    public bool isCooldown;
    public bool isInEffect;
    public bool canCast;
    public float castRange;

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

    public virtual void CastSkill(Unit target)
    {
        isCooldown = true;
        isInEffect = true;

    }

    public virtual IEnumerator CoolDownEnumerator()
    {
        Debug.Log("isCooldown");

        while (isCooldown)
        {
            Debug.Log(coolDownDuration[skillLevel]);
            yield return new WaitForSeconds(coolDownDuration[skillLevel]);
            canCast = true;
            isCooldown = false;
            //SkillManager.Instance.skillButtons[SkillManager.Instance.skillRef].interactable = true;
        }
        //Debug.Log("can cast again");
    }

    public virtual void CastCondition()
    {

    }
}
