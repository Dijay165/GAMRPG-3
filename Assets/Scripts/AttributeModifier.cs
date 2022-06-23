using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeModifier : MonoBehaviour
{
    public int armorAmount = 0; //5 for tier 2 3 4
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ApplyModification()
    {
        gameObject.GetComponent<Attributes>().armor += armorAmount;
    }
}
