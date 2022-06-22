using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private HealthOverheadUI healthOverheadUI;

    private void OnEnable()
    {
        Debug.Log("OnStart");
        Health health = GetComponent<Health>();

        //PoolableObject healthOverheadUIObject = ObjectPoolsManager.GetPool(typeof(HealthOverheadUI)).pool.Get();

        HealthOverheadUI newHealthOverheadUI = HealthOverheadUIPool.pool.Get() as HealthOverheadUI;
        healthOverheadUI = newHealthOverheadUI;
        //healthOverheadUI = healthOverheadUIObject.GetComponent<HealthOverheadUI>();

        healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
        health.OnHealthModifyEvent.AddListener(healthOverheadUI.OnHealthChanged);
        health.OnDeathEvent.AddListener(healthOverheadUI.OnHealthDied);

        Debug.Log("OnEnd");
    }
}
