using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [HideInInspector]
    public Unit unit;
    [HideInInspector]
    public Attributes attributes;

    private void Start()
    {
        unit = GetComponent<Unit>();
        attributes = GetComponent<Attributes>();
    }
    public virtual void DamageTarget()
    {
        //unit.currentTarget.SubtractHealth(10);
    }
}
