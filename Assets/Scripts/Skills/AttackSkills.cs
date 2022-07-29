using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Skills", menuName = "New Attack Skills")]
public class AttackSkills : Skill
{
    public float castRange;

    public override void CastSkill(Unit userUnit)
    {
        base.CastSkill(userUnit);
       // Debug.Log("Stuff0");
    }
}
