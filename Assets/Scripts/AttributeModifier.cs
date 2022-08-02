using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttributeModifier : MonoBehaviour
{
    protected Attributes attributes;
    public int modifierAmount = 0; //5 for tier 2 3 4
    public bool hasBuff;

    private void Awake()
    {
        attributes = gameObject.GetComponent<Attributes>();
    }
    public virtual void ApplyModification()
    {
        hasBuff = true;
        //  Debug.Log("Old Armor: " + gameObject.GetComponent<Attributes>().totalArmor + gameObject.name);

       // attributes.totalArmor += armorAmount;

       // Debug.Log(" New Armor: " + gameObject.GetComponent<Attributes>().totalArmor + gameObject.name);
    }

    public virtual void RemoveModification()
    {
        hasBuff = false;
   //     attributes.totalArmor -= armorAmount;
    }


}
