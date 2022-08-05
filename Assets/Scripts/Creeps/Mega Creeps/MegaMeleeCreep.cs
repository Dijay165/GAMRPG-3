using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MegaMeleeCreep : Creep
{
    protected override void InitializeValues()
    {
        base.InitializeValues();
     
    }
    protected override void DeinitializeValues()
    {
        base.DeinitializeValues();
        //Destroy(gameObject);
        SpawnManager.instance.Despawn(gameObject);
        //MegaMeleeCreepPool.pool.Release(this);
    }
}
