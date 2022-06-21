using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public SpriteRenderer icon;
    protected int team;
    public Health currentTarget;
    public void AssignTeam()
    {
        TestStatsHolder testStatsHolder = GetComponent<TestStatsHolder>();
        team = (int)testStatsHolder.unitFaction;

        if(icon != null)
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
        InitializeValues();
    }

    protected virtual void InitializeValues()
    {
        AssignTeam();
        Health health = GetComponent<Health>();
        HealthOverheadUI healthOverheadUI = HealthOverheadUIPool.pool.Get();
        healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
        health.OnHealthModifyEvent.AddListener(healthOverheadUI.OnHealthChanged);
        health.OnDeathEvent.AddListener(healthOverheadUI.OnHealthDied);
    }
}
