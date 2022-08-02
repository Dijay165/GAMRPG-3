using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    // Start is called before the first frame update

    public Job job;
    public List<KeyCode> keyCodes;

    public List<ActiveSkill> skills;
    Animator anim;
    MOBAMovement movement;
    [HideInInspector]
    public int skillIDIndex;
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
        
        foreach(ActiveSkill skill in job.skills)
        {
            skills.Add(skill);
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
                Debug.Log("Pressed " + keyCodes[i]);
       
                movement.HeroMove();
                Events.OnPlayerSkillIndex.Invoke(i);
                skillIDIndex = i;
              
                
                ActivateSkill();
                
            }
          
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
                // job.skills[index].CastSkill(unit);
                Debug.Log("Unit Found");
                skills[index].OnActivate(unit);
                //job.skills[index].CastSkill(unit);
                StartCoroutine(skills[index].CoolDownEnumerator());
            }
            else
            {
                Debug.Log("Unit Not Found");
            }
          
    }

    public void ActivateSkill()
    {
        /// Debug.Log("Activate Skill");
        //CastSkill(skillIDIndex);

       
        skills[skillIDIndex].OnActivate(gameObject.GetComponent<Unit>());
        StartCoroutine(skills[skillIDIndex].CoolDownEnumerator());
        //if (skills[skillIDIndex].skillType == SkillType.Active)
        //{
        //    if (!skills[skillIDIndex].isCooldown)
        //    {
            
        //    }
            
        //}
     
    }
}
