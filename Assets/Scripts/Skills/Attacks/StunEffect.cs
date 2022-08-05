using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : StatusEffect
{
    // Start is called before the first frame update

    Unit unit;
    void Start()
    {
        unit = target.GetComponent<Unit>();
         StartCoroutine(Debuff());
    }

    // Update is called once per frame
    void Update()
    {
       // StartCoroutine(Debuff());
    }

    public override void InflictDebuff()
    {
     

        //Debug.Log(unit.isStun);
        unit.isStun = true;
        base.InflictDebuff();
        

    }

    private void OnDisable()
    {
        //  Debug.Log("OnDisable");
        unit.isStun = false;
    }

    public override IEnumerator Debuff()
    {
       // Debug.Log("Debuff");
        InflictDebuff();
        return base.Debuff();
    }

    private void OnDestroy()
    {
        
    }
}
