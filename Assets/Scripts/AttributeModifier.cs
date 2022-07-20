using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeModifier : MonoBehaviour
{
    public int armorAmount = 0; //5 for tier 2 3 4
    public bool hasBuff;
 
    public void ApplyModification()
    {
        hasBuff = true;
        Debug.Log("Old Armor: " + gameObject.GetComponent<Attributes>().bonusArmor + gameObject.name);

        gameObject.GetComponent<Attributes>().bonusArmor += armorAmount;

        Debug.Log(" New Armor: " + gameObject.GetComponent<Attributes>().bonusArmor + gameObject.name);
    }

    public void VoidModification()
    {
        hasBuff = false;
        gameObject.GetComponent<Attributes>().bonusArmor -= armorAmount;
    }
}
