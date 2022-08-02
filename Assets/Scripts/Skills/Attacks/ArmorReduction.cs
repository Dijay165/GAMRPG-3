using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorReduction : StatusEffect
{
    // Start is called before the first frame update
    Unit unit;
    public List<float> armorReductionModifier;
    private float baseArmor;

   // public int skillIndex;
    void Start()
    {
        unit = target.GetComponent<Unit>();
        StartCoroutine(Debuff());

        baseArmor = unit.GetComponent<Attributes>().totalArmor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InflictDebuff()
    {
        base.InflictDebuff();
        Debug.Log("Before Debuff" + unit.GetComponent<Attributes>().totalArmor);
        unit.GetComponent<Attributes>().totalArmor -= armorReductionModifier[skill.skillLevel];
        Debug.Log("After Debuff" + unit.GetComponent<Attributes>().totalArmor);
    }

    public override IEnumerator Debuff()
    {
        InflictDebuff();
        return base.Debuff();
    }

    private void OnDestroy()
    {
        unit.GetComponent<Attributes>().totalArmor += baseArmor;
    }

}
