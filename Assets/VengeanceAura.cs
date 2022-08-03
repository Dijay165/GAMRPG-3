using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "VengeanceAura", menuName = "VengeanceAura")]

public class VengeanceAura : PassiveSkill
{


    public bool hasSkill;

    private void Awake()
    {
        //unit = GetComponent<Unit>();
    }
    private void Start()
    {

    }

    private void OnDisable()
    {
        hasSkill = false;
    }

    public override void OnApply(Unit unit)
    {
        base.OnApply(unit);
        // GameObject obj =     

        Debug.Log("OnApply");
        //  Debug.Log(unit.name);

        if (!hasSkill)
        {
            if (unit.GetComponent<VSAura>() == null)
            {
                unit.gameObject.AddComponent<VSAura>();


                unit.gameObject.GetComponent<VSAura>().auraRadius = this.castRange;
                unit.gameObject.GetComponent<VSAura>().damageBonus = this.damage;
                unit.gameObject.GetComponent<VSAura>().rangeBonus = this.effectDuration;
                hasSkill = true;
            }

        }
        


    }
}

