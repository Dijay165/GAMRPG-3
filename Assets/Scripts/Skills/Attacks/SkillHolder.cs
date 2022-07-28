using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    // Start is called before the first frame update

    public Job job;
    public List<KeyCode> keyCodes;
    void Start()
    {
        InitializeKeys();
    }

    // Update is called once per frame
    void Update()
    {
        KeyPressedCheckers();
    }


    public void InitializeKeys()
    {
        foreach(Skill skill in job.skills)
        {
            keyCodes.Add(skill.pressButton);
        }
    }

    public void KeyPressedCheckers()
    {
        for (int i = 0; i < keyCodes.Count; i++)
            if (Input.GetKeyDown(keyCodes[i])) Debug.Log("Pressed " + keyCodes[i]);
    }
}
