using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Unit : MonoBehaviour
{
    protected bool isInUse;
    public SpriteRenderer icon;
    protected int team;
    public Health currentTarget;

    public Health health;
    private HealthOverheadUI healthOverheadUI;

   

    public void AssignTeam()
    {
        TestStatsHolder testStatsHolder = GetComponent<TestStatsHolder>();
        team = (int)testStatsHolder.unitFaction;

  
        if (icon != null)
        {
            icon.sprite = testStatsHolder.unitStat.iconImage;

            if (testStatsHolder.unitFaction == Faction.Radiant)
            {
                icon.color = new Color32(0, 255, 0, 255);
            }
            else
            {
                icon.color = new Color32(255, 0, 0, 255);
                
            }
        }
          
        
    }
    protected virtual void Start()
    {
        health = GetComponent<Health>();
        InitializeValues();
    }

    protected virtual void InitializeValues()
    {
        AssignTeam();
       
        healthOverheadUI = HealthOverheadUIPool.pool.Get();
        healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
        health.OnHealthModifyEvent.AddListener(healthOverheadUI.OnHealthChanged);
        health.OnDeathEvent.AddListener(healthOverheadUI.OnHealthDied);

       
        health.OnDeathEvent.AddListener(UnitDeath);
        isInUse = true;

    }

    protected virtual void DeinitializeValues()
    {

        health.OnHealthModifyEvent.RemoveListener(healthOverheadUI.OnHealthChanged);
        health.OnDeathEvent.RemoveListener(healthOverheadUI.OnHealthDied);
        //health.OnDeathEvent.RemoveAllListeners();

        isInUse = false;
    }

    public virtual void UnitDeath(Health objectHealth = null)
    {
        DeinitializeValues();
       
    }

  
}
