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
        
        foreach(AbilityBase skill in gameObject.GetComponents<AbilityBase>())
        {
            skills.Add(skill);
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
                //   Debug.Log("Pressed " + keyCodes[i]);

                anim.SetTrigger("CastSkill");
                // anim.SetBool("CastSkill", true);
                // targetedDamager.h();
                movement.HeroMove();
                Events.OnPlayerSkillIndex.Invoke(i);
                //ActivateSkill(i);
                
            }
          
        }
 
    }

    public void ActivateSkill(int index)
    {
        if (!skills[index].isCooldown && skills[index].canCast)
        {
            if(TryGetComponent<Unit>(out Unit unit))
            {
                // job.skills[index].CastSkill(unit);
                skills[index].CastSkill(unit);
                //job.skills[index].CastSkill(unit);
                StartCoroutine(skills[index].CoolDownEnumerator());
            }
            
        }
    }
}
