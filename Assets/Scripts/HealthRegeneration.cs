using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthRegeneration : Regeneration
{
    //   float currentHP;
    protected float regenValue;
    public TextMeshProUGUI text;
    public override void Awake()
    {
        base.Awake();

    }
    private void Start()
    {
   //     currentHP = health.GetHealth();
        regenValue = health.healthRegen;

        StartCoroutine(Regen());

    }

    private void Update()
    {
        //if (health.isAlive)
        //{
        //    // DoRegen();
        //    health.currentHealth += regenValue * Time.deltaTime;

        //    if (health.currentHealth > health.maxHealth)
        //    {
        //        health.currentHealth = health.maxHealth;
        //    }
        //    if (health.currentHealth < 0)
        //    {
        //        health.currentHealth = 0;
        //    }

        //    text.text = (int)health.currentHealth +"";

        //}
    }

    public override void DoRegen()
    {
        base.DoRegen();
        health.AddHealth(regenValue /** Time.deltaTime*/);
    }


}
