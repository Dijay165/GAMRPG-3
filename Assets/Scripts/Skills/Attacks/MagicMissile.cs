using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "MagicMissile", menuName = "New MagicMissile")]

public class MagicMissile : AbilityBase
{
    public GameObject missilePrefab;
    private void Awake()
    {
        Initialized();
    }


    public override void CastSkill(Unit target)
    {
        base.CastSkill(target);
        Debug.Log("Magic Missile");
        GameObject obj = Instantiate(missilePrefab);
    }
}
