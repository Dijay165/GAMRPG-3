using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VengeanceAura : AbilityBase
{
    public float auraRadius;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, auraRadius);
    }
}
