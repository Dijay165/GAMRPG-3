using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [HideInInspector]
    public Unit unit;
    [HideInInspector]
    public Attributes attributes;
    [HideInInspector]
    public TestStatsHolder statsHolder;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        attributes = GetComponent<Attributes>();
        statsHolder = gameObject.GetComponent<TestStatsHolder>();
    }

    public virtual void DamageTarget()
    {
        //unit.currentTarget.SubtractHealth(10);
    }

   
}
