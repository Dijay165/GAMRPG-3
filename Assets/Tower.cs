using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        Health health = GetComponent<Health>();
        HealthOverheadUI healthOverheadUI = HealthOverheadUIPool.pool.Get();
        healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
        health.OnHealthModifyEvent.AddListener(healthOverheadUI.OnHealthChanged);
        health.OnDeathEvent.AddListener(healthOverheadUI.OnHealthDied);
    }

    private void OnEnable()
    {
     
    }


}
