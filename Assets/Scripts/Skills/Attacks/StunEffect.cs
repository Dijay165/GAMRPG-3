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
           
    }

    public override void InflictDebuff()
    {
        base.InflictDebuff();
       
        

        unit.isStun = true;
        

     //   target.Play("StatusEffect");
        
    }

    private void OnDisable()
    {
      //  Debug.Log("OnDisable");
    }

    public override IEnumerator Debuff()
    {
        Debug.Log("Debuff");
        InflictDebuff();
        unit.isStun = false;
        return base.Debuff();
        
    }
}
