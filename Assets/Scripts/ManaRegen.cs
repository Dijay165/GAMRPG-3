using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRegen : Regeneration
{
    protected float regenValue;
    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        regenValue = mana.manaRegen;
        StartCoroutine(Regen());
    }

    public override void DoRegen()
    {
        base.DoRegen();
        mana.AddMana(regenValue);
    }
}
