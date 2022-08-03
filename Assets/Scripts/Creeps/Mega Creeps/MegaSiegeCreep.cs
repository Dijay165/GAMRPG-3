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
        //Destroy(gameObject);
        MegaSiegeCreepPool.pool.Release(this);
    }
}
