using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeModifier : MonoBehaviour
{
    public int armorAmount = 0; //5 for tier 2 3 4
 
    public void ApplyModification()
    {
        gameObject.GetComponent<Attributes>().armor += armorAmount;
    }
}
