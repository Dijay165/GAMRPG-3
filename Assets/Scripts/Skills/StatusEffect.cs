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
    public Transform target;
    public bool isInEffect;
     

    private void Awake()
    {
        animator = GetComponent<Animator>();
        duration = skill.effectDuration[skill.skillLevel];
    }

    public void Initialized(float p_duration, Transform p_target)
    {
        duration = p_duration;
        target = p_target;

    }

    public virtual void InflictDebuff()
    {

    }

    public virtual void InflictBuff()
    {

    }

    public virtual void ResetStats()
    {

    }

    public float durationGetter()
    {
        return duration;
    }


    public virtual IEnumerator Debuff()
    {
        while (isInEffect)
        {
            Debug.Log(isInEffect + "On Cooldown");
            yield return new WaitForSeconds(this.duration);
            //     Destroy(gameObject);
            isInEffect = false;
        }

        Debug.Log("IsOver");
        
    }
}
