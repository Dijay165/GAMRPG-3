using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeModifier : AttributeModifier
{
    // Start is called before the first frame update
    public override void ApplyModification()
    {
        base.ApplyModification();
        attributes.attackRange += modifierAmount;

    }

    public override void RemoveModification()
    {
        base.RemoveModification();
        attributes.attackRange -= modifierAmount;
    }
}
