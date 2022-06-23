using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SiegeCreep : Creep
{
    protected override void InitializeValues()
    {
        base.InitializeValues();
   

    }
    protected override void DeinitializeValues()
    {
        base.DeinitializeValues();
       
        //SiegeCreepPool.pool.Release(this);
        Debug.Log("Destroy");
        //HealthOverheadUIPool.pool.Release(this);
        Destroy(gameObject);
    }

}
