using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour
{
    // Start is called before the first frame update

    protected Health health;
    protected float regenValue;

    public virtual void Awake()
    {
        health = GetComponent<Health>();
        //health.healthRegen = regenValue;
    }

    public IEnumerator Regen()
    {
        while (health.isAlive)
        {
           //      health.AddHealth(regenValue);
            DoRegen();

            yield return new WaitForSeconds(0.1f);
        }
    }

    public virtual void DoRegen()
    {

    }

}
