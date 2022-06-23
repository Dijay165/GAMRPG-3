using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MegaSiegeCreep : Creep
{
    protected override void InitializeValues()
    {
        base.InitializeValues();


    }
    protected override void DeinitializeValues()
    {
        base.DeinitializeValues();
      
        //MegaSiegeCreepPool.pool.Release(this);
        Debug.Log("Destroy");
        //HealthOverheadUIPool.pool.Release(this);
        Destroy(gameObject);
    }

}
