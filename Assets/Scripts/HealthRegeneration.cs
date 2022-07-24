using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegeneration : Regeneration
{
    float currentHP;
    public override void Awake()
    {
        base.Awake();

    }
    private void Start()
    {
        currentHP = health.GetHealth();
        regenValue = health.healthRegen;

        StartCoroutine(Regen());

    }

    public override void DoRegen()
    {
        base.DoRegen();
        health.AddHealth(regenValue);
    }


}
