using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MegaRangeCreep : Creep
{
    protected override void InitializeValues()
    {
        base.InitializeValues();
    }
    protected override void DeinitializeValues()
    {
        base.DeinitializeValues();
        Destroy(gameObject);
        //MegaRangeCreepPool.pool.Release(this);
    }
}
