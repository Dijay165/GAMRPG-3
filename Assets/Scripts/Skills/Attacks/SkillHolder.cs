using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    // Start is called before the first frame update

    public Job job;
    public List<KeyCode> keyCodes;

    public List<AbilityBase> skills;
    Animator anim;
    MOBAMovement movement;
    [HideInInspector]
    public int skillIDIndex;
    bool willCast;
    
    
    private void Awake()
    {
        // Events.OnPlayerSkillIndex.AddListener(KeyIndex);

        // Events.OnPlayerSkillIndex.AddListener(KeyIndex);
        anim = GetComponent<Animator>();
        movement = GetComponent<MOBAMovement>();
    }
    void Start()
    {
        //skills.Add(gameObject.GetComponents<Skill>());
        //skills = gameObject.GetComponents<Skill>();
        
        foreach(AbilityBase skill in job.skills)
        {
            skills.Add(skill);
            skill.isCooldown = false;
            skill.isInEffect = false;
            skill.canCast = true;
            skill.skillLevel = 0;
            skill.isMaxLevel = false;
            skill.maxSkillLevel = skill.manaCost.Count - 1;
            skill.isUnlock = false;
          //  Debug.Log(skill.name);
           // keyCodes.Add(skill.keyCode);
        }

        InitializeKeys();

    }

    private void OnEnable()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        KeyPressedCheckers();
        // PassiveSkill();
        //  anim.SetTrigger("CastSkill");
        ActivatePassiveSkill();
    }


    public void InitializeKeys()
    {

        for (int i = 0; i < skills.Count; i++)
        {
            keyCodes.Add(skills[i].keyCode);
        }

    }

    public void KeyPressedCheckers()
    {
        for (int i = 0; i < keyCodes.Count; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
              //  Debug.Log("Pressed " + keyCodes[i]);
       
                Events.OnPlayerSkillIndex.Invoke(i);
                skillIDIndex = i;
                movement.HeroMove();
                if (skills[skillIDIndex].canCast)
                {
                    if (skills[skillIDIndex].isUnlock)
                    {
                        willCast = true;

                    }
                }
            }
          
        }

        if(gameObject.GetComponent<TargetedDamager>().targetHealth != null)
        {
            bool inDistance = skills[skillIDIndex].CastCondition(gameObject.transform, gameObject.GetComponent<TargetedDamager>().targetHealth.playersParent.transform);
            if (inDistance)
            {
                //Debug.Log("distance");
                if (skills[skillIDIndex].canCast)
                {
                    if(gameObject.GetComponent<Mana>().currentMana > skills[skillIDIndex].manaCost[skills[skillIDIndex].skillLevel])
                    {
                        if (skills[skillIDIndex].isUnlock)
                        {
                            Debug.Log("Can cast, unlocked");

                            if (willCast)
                            {
                                anim.SetTrigger("CastSkill");
                            }
                            
                        }
                        else
                        {
                            Debug.Log("Can't cast, locked");
                            willCast = false;
                        }
                        
                    }
                    else
                    {
                        anim.SetTrigger("Turn");
                        willCast = false;

                    }



                }
               
            //    ActivateSkill();
            }
        }
        else
        {
          //  Debug.Log("isnull");
        }
       

    }

    public void Passive()
    {
       

        //foreach(PassiveSkill passiveSkill in skills)
        //{

        //}
    }

    public void CastSkill(int index)
    {
       
        
            if(TryGetComponent<Unit>(out Unit unit))
            {
                Debug.Log("Unit Found");
                
                //if(skills[index].GetType() == typeof(ActiveSkill))
                //{
                    if(skills[index] is ActiveSkill)
                    {
                        ActiveSkill activeSkill = (ActiveSkill)skills[index];
                        activeSkill.OnActivate(unit);
                        StartCoroutine(activeSkill.CoolDownEnumerator());
                    }
                 //   skills[index].OnActivate(unit);
                    
               //  }
            }
              
            else
            {
                Debug.Log("Unit Not Found");
            }
          
    }

    public void ActivatePassiveSkill()
    {

        for(int i = 0; i < skills.Count; i++)
        {
            if (skills[i] is PassiveSkill)
            {
                if (skills[i].isUnlock)
                {
                    PassiveSkill passiveSkill = (PassiveSkill)skills[i];
                    passiveSkill.OnApply(gameObject.GetComponent<Unit>());
                    Debug.Log("Passive is unlocked");

                }
                else
                {
                    Debug.Log("Passive is locked");
                }
              
            }
        }
 
    }



    public void ActivateSkill()
    {

        if (skills[skillIDIndex] is ActiveSkill)
        {
            ActiveSkill activeSkill = (ActiveSkill)skills[skillIDIndex];
            if (activeSkill.isUnlock)
            {
                if (activeSkill.canCast)
                {
                    //Debug.Log("Can cast");
                    Debug.Log("Active is unlocked");

                    activeSkill.OnActivate(gameObject.GetComponent<Unit>());
                    StartCoroutine(activeSkill.CoolDownEnumerator());


                    if (gameObject.GetComponent<Unit>().CompareTag("Player"))
                    {
                        StartCoroutine(SkillManager.instance.CountdownText(activeSkill.coolDownDuration[activeSkill.skillLevel], SkillManager.instance.text[skillIDIndex]));
                        // SkillManager.instance.ButtonChecker();
                    }

                }
                else
                {
                    //     Debug.Log("Cannot cast");
                }
            }
            else
            {
                Debug.Log("Active is locked");

            }


        }
    }

    public void StopCountdown()
    {
        Debug.Log("Stop");
  //      ActiveSkill activeSkill = (ActiveSkill)skills[skillIDIndex];

        StopAllCoroutines();
      //  StopCoroutine(SkillManager.instance.CountdownText(activeSkill.coolDownDuration[activeSkill.skillLevel], SkillManager.instance.text[skillIDIndex]));
    }
}
