using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicMissile", menuName = "New MagicMissile")]

public class MagicMissile : AttackSkills
{


    public override void Initialize(List<float> p_damage, List<float> p_coolDownDuration, List<float> p_effectDuration, List<float> p_manaCost, AttackType p_attackType, KeyCode p_pressButton)
    {
        base.Initialize(damage, coolDownDuration, effectDuration, manaCost, attackType, KeyCode.Q);
    }


    public override void CastSkill(Unit userUnit)
    {
        base.CastSkill(userUnit);
        
        Debug.Log("Stuff");

    }

}
