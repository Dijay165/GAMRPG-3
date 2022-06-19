using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    protected Unit unit;
    protected Attributes attributes;

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
