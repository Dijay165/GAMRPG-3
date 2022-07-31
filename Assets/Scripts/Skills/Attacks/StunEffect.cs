using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : StatusEffect
{
    // Start is called before the first frame update
  
    void Start()
    {
        InflictDebuff();
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    public override void InflictDebuff()
    {
        base.InflictDebuff();
        Debug.Log("Debuff");
        Unit unit = target.GetComponent<Unit>();

        unit.isStun = true;
        Debug.Log(unit.isStun);

     //   target.Play("StatusEffect");
        
    }

    private void OnDisable()
    {
      //  Debug.Log("OnDisable");
    }
}
