using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    // Start is called before the first frame update

    public Job job;
    [HideInInspector]
    public List<KeyCode> keyCodes;

    public List<AbilityBase> skills;
    private void Awake()
    {
        // Events.OnPlayerSkillIndex.AddListener(KeyIndex);

       // Events.OnPlayerSkillIndex.AddListener(KeyIndex);
    }
    void Start()
    {
        //skills.Add(gameObject.GetComponents<Skill>());
        //skills = gameObject.GetComponents<Skill>();
        
        foreach(AbilityBase skill in gameObject.GetComponents<AbilityBase>())
        {
            skills.Add(skill);
        }
        InitializeKeys();

        
    }

    // Update is called once per frame
    void Update()
    {
        KeyPressedCheckers();
    }


    public void InitializeKeys()
    {
        foreach(AbilityBase skill in skills)
        {
            keyCodes.Add(skill.keyCode);
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
                ActivateSkill(i);

            }
          
        }
 
    }

    public void ActivateSkill(int index)
    {
        if (!skills[index].isCooldown)
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
