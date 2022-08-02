using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorModifier : AttributeModifier
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ApplyModification()
    {
        base.ApplyModification();
        attributes.totalArmor += modifierAmount;
    }

    public override void RemoveModification()
    {
        base.RemoveModification();
        attributes.totalArmor -= modifierAmount;
    }
}
