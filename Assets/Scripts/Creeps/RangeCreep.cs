using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RangeCreep : Creep
{
    protected override void InitializeValues()
    {
        base.InitializeValues();
    }
    protected override void DeinitializeValues()
    {
        base.DeinitializeValues();
        Destroy(gameObject);
        //RangeCreepPool.pool.Release(this);
    }
}
