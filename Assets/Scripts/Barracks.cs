using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Structures
{


    public override void Death(Health objectHealth = null)
    {
        SpawnManager.UpdateBarracksState(this);
        //base.Death(objectHealth);
    }
}
