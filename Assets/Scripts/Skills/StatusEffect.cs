using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    // Start is called before the first frame update

    protected Animator animator;
    public Animator targetAnimator;
    protected float duration;
    public Skill skill;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        duration = skill.effectDuration[skill.skillLevel];
    }

    public virtual void InflictDebuff(Animator target)
    {

    }

    public virtual void InflictBuff(Animator target)
    {

    }

    public virtual void ResetStats()
    {

    }

    public float durationGetter()
    {
        return duration;
    }

}
